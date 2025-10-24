using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.DbContexts;
using Abstractions.Setup;
using Application.Consumers;
using Application.Interfaces.StackOverFlow;
using Hangfire;
using Infrastructure.Adapters;
using Infrastructure.Adapters.Types;
using Infrastructure.Api;
using Infrastructure.Api.Options;
using Infrastructure.Middleware;
using Infrastructure.Security.Identity;
using Infrastructure.Services.CacheServices;
using Infrastructure.Services.DataServices;
using Infrastructure.Services.HostedServices;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(builder.Configuration)
            .CreateLogger();

        builder.Services.Configure<StackOverFlowOptions>(
            builder.Configuration.GetSection("ExternalApies:StackOverFlow"));

        builder.Services.AddSingleton<ICacheVersionService, CacheVersionService>();
        builder.Services.AddHttpClient<IStackOverFlowApiClient, StackOverFlowApiClient>();
        builder.Services.AddScoped<IStackOverFlowDataService, StackOverFlowDataService>();
        
        builder.Services.AddHostedService<StartupSyncHostedService>();
        builder.Services.AddMemoryCache();

        builder.Host.UseSerilog();
        builder.Services.AddSignalR();

        builder.Services.AddIdentityWithJwt(builder.Configuration);

        builder.Services.AddScoped<Query>();

        builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddType<UserType>()
            .AddType<QuestionType>()
            .AddType<TagType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .ModifyCostOptions(opt =>
            {
                opt.EnforceCostLimits = false;
            });


        var connectionString = builder.Configuration.GetConnectionString("Default");

        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        builder.Services.AddHangfire(c =>
        {
            c.UseSqlServerStorage(connectionString);
        });

        builder.Services.AddHangfireServer();

        builder.Services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<QuestionsConsumer>();
            cfg.AddConsumer<UsersConsumer>();
            cfg.AddConsumer<TestConsumer>();

            cfg.AddEntityFrameworkOutbox<AbstractAppDbContext>(o =>
            {
                o.UseSqlServer();
                o.UseBusOutbox();
            });


            cfg.UsingRabbitMq((context, c) =>
            {
                c.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                c.ReceiveEndpoint("Test", e =>
                {
                    e.ConfigureConsumer<TestConsumer>(context);
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(10)));
                });

                c.ReceiveEndpoint("Questions", e =>
                {
                    e.ConfigureConsumer<QuestionsConsumer>(context);
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(10)));
                });

                c.ReceiveEndpoint("Users", e =>
                {
                    e.ConfigureConsumer<UsersConsumer>(context);
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(10)));
                });
            });
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolice",
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
    }
}
