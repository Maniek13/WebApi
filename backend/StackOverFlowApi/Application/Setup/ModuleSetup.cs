using Abstractions.Services;
using Abstractions.Setup;
using Application.Services;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddMapster();
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ModuleAssembly).Assembly);

        builder.Services.AddScoped<ITagService, TagService>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ModuleAssembly).Assembly));
    }
}
