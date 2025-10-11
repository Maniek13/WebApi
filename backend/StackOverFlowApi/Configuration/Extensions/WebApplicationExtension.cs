using Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Configuration.Extensions;

public static class WebApplicationExtension
{
    public static void StartupWebApi(this WebApplication app, IConfigurationBuilder configuration)
    {
        app.StartupModules(configuration,
            typeof(Infrastructure.Startup.ModuleStartup).Assembly,
            typeof(Presentation.Startup.ModuleStartup).Assembly
            );
    }
}
