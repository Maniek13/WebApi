using Abstractions.Startup;
using Infrastructure.Midleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Startup;

public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfigurationBuilder configuration)
    {
        application.UseMiddleware<ErrorLoggingMiddleware>();
    }
}
