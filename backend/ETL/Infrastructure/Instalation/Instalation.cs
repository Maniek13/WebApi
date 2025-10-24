using Abstractions.DbContexts;
using Application.Api;
using Hangfire;
using Infrastructure.Api;
using Infrastructure.Api.Options;
using Infrastructure.Filters;
using Infrastructure.Jobs;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Instalation;

public static class Instalation
{
    public static void InfrastructoreSetup(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<StackOverFlowOptions>(
            builder.Configuration.GetSection("ExternalApies:StackOverFlow"));

        builder.Services.AddHttpClient<IStackOverFlowApiClient, StackOverFlowApiClient>();
        builder.Services.AddScoped<ISOFGrpcClient, SOFGrpcClient>();

        builder.Services.AddHangfire(c =>
        {
            c.UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"));
        });

        builder.Services.AddHangfireServer();


        builder.Services.AddMassTransit(cfg =>
        {
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
            });
        });
    }

    public static void InfrastructoreStartup(this WebApplication app)
    {
        app.UseHangfireDashboard("/dashbord", new DashboardOptions
        {
            Authorization = new[] { new AuthorizationFilter() }
        });

        ConfigureJobs.SetRecurngJobs();
    }
}
