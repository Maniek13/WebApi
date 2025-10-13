using Abstractions.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace WebSockets.Startup;

public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfigurationBuilder configuration)
    {
        var webSocketOptions = new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromMinutes(1)
        };

        application.UseWebSockets();
    }
}
