using Abstractions.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Extensions.Configuration;

public static class WebApplicationExtensions
{
    public static void StartupModules(this WebApplication application, IConfiguration configuration, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var cfgs = assembly
                .GetTypes()
                .Where(el => typeof(IModuleStartup).IsAssignableFrom(el) && !el.IsAbstract && !el.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IModuleStartup>();

            foreach (var cfg in cfgs)
            {
                cfg.Startup(application, configuration);
            }
        }
    }
}
