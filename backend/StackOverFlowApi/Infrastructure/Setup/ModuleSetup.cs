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
using Infrastructure.Security.Identity;
using Infrastructure.Services.CacheServices;
using Infrastructure.Services.DataServices;
using Infrastructure.Services.HostedServices;
using MassTransit;
using Microsoft.AspNetCore.Builder;
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
            .AddSorting();

        builder.Services.AddHangfire(c =>
        {
            c.UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"));
        });

        builder.Services.AddHangfireServer();

        builder.Services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<QuestionsConsumer>();
            cfg.AddConsumer<UsersConsumer>();

            cfg.AddEntityFrameworkOutbox<AbstractSOFDbContext>(o =>
            {
                o.UseSqlServer();
                o.QueryDelay = TimeSpan.FromSeconds(5);
                o.DisableInboxCleanupService();
            });

            cfg.UsingRabbitMq((context, c) =>
            {
                c.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });


                c.ReceiveEndpoint("Questions", e =>
                {
                    e.ConfigureConsumer<QuestionsConsumer>(context);
                });

                c.ReceiveEndpoint("Users", e =>
                {
                    e.ConfigureConsumer<UsersConsumer>(context);
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
