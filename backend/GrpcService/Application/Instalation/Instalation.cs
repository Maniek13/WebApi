using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Instalation;

public static class Instalation
{
    public static void ApplicationSetup(this WebApplicationBuilder builder)
    {
        builder.Services.AddMapster();
        TypeAdapterConfig.GlobalSettings.Scan(ModuleAssembly.GetExecutionAssembly);

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(ModuleAssembly.GetExecutionAssembly));
    }
}
