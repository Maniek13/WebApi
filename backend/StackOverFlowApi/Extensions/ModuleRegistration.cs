using Abstractions.Setup;
using Abstractions.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Extensions;

public static class ModuleRegistration
{
    public static void SetupModules(this WebApplicationBuilder builder, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var cfgs = assembly
                .GetTypes()
                .Where(el => typeof(IModuleSetup).IsAssignableFrom(el) && !el.IsAbstract && !el.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IModuleSetup>();

            foreach(var cfg in cfgs)
            {
                cfg.Setup(builder);
            }
        }
    }
    public static void StartupModules(this WebApplication application, IConfigurationBuilder configuration, params Assembly[] assemblies)
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
