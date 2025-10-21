using Application.Api;
using Infrastructure.Api;
using Infrastructure.Api.Options;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Infrastructure.Instalation;

public static class Instalation
{
    public static void InfrastructoreSetup(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<StackOverFlowOptions>(
            builder.Configuration.GetSection("ExternalApies:StackOverFlow"));

        builder.Services.AddHttpClient<IStackOverFlowApiClient, StackOverFlowApiClient>();

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 5201, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        builder.Services.AddGrpc();
    }

    public static void InfrastructoreStartup(this WebApplication app)
    {
        app.MapGrpcService<UsersService>();
    }
}
