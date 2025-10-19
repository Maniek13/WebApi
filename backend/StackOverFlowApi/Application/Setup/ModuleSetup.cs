using Abstractions.Setup;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Setup;

public class ModuleSetup : IModuleSetup
{
        public void Setup(WebApplicationBuilder builder)
        {
            builder.Services.AddMapster();
            TypeAdapterConfig.GlobalSettings.Scan(ModuleAssembly.GetExecutionAssembly);

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(ModuleAssembly.GetExecutionAssembly));

        }
}
