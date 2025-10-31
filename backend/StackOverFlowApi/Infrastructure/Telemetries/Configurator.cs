using Hangfire;
using Infrastructure.Telemetries.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Data.Common;
using System.Diagnostics;

namespace Infrastructure.Telemetries;

public static class Configurator
{
    public static void AddOpenTelemetryTelemetry(this WebApplicationBuilder builder, string serviceName, string serviceVersion)
    {
            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName, serviceVersion: serviceVersion))
                .WithTracing(tracing =>
                {
                    tracing.AddAspNetCoreInstrumentation(options =>
                    {
                        options.Filter = (httpContext) =>
                        {
                            var path = httpContext.Request.Path.Value?.ToLowerInvariant() ?? "";
                            return !path.StartsWith("/dashboard") && !path.StartsWith("/health") && !path.StartsWith("/metrics");
                         
                        };
                    });
                    tracing.AddHttpClientInstrumentation();
                    tracing.AddSource("HangfireJobs");
                    tracing.AddRabbitMQInstrumentation();
                    tracing.AddEntityFrameworkCoreInstrumentation(options =>
                    {
                        options.Filter = (providerName, command) =>
                        {
                            var text = command.CommandText?.ToLowerInvariant() ?? "";
                            return !text.Contains("hangfire") && !text.Contains("outbox") && !text.Contains("inbox");
                        };
                    });


                    tracing.AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri(builder.Configuration.GetValue<string>("OTEL:Endpoint")!);
                    });
                })
                .WithMetrics(metrics =>
                {
                    metrics.AddAspNetCoreInstrumentation();
                    metrics.AddHttpClientInstrumentation();

                    metrics.AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri(builder.Configuration.GetValue<string>("OTEL:Endpoint")!);
                    });

                });
        }
    public static void AddFilters(this WebApplication app)
    {
        GlobalJobFilters.Filters.Add(new OpenTelemetryHangfireFilter());
    }
}

