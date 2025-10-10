using Abstractions.Caches;
using Abstractions.Setup;
using Infrastructure.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICacheVersionService, CacheVersionService>();
        builder.Services.AddMemoryCache();
    }
}
