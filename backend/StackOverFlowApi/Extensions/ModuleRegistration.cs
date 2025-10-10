using Abstractions.Configuration;
using Abstractions.SetUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Extensions;

public static class ModuleRegistration
{
    public static void ConfigureModules(this WebApplicationBuilder builder, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var cfgs = assembly
                .GetTypes()
                .Where(el => typeof(IModuleConfiguration).IsAssignableFrom(el) && !el.IsAbstract && !el.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IModuleConfiguration>();

            foreach(var cfg in cfgs)
            {
                cfg.SetUp(builder);
            }
        }
    }
    public static void SetUpModules(this WebApplication application, IConfigurationBuilder configuration, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var cfgs = assembly
                .GetTypes()
                .Where(el => typeof(IModuleSetup).IsAssignableFrom(el) && !el.IsAbstract && !el.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IModuleSetup>();

            foreach (var cfg in cfgs)
            {
                cfg.SetUp(application, configuration);
            }
        }
    }
}
