using Abstractions.Setup;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Extensions.Configuration;

public static class WebApplicationBuilderExtensions
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
}
