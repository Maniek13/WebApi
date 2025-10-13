using Abstractions.Startup;
using Infrastructure.Midlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Startup;

public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfiguration configuration)
    {

        application.UseMiddleware<ErrorLoggingMiddleware>();
    }
}
