using Abstractions.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Presentation.Startup;

public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfigurationBuilder configuration)
    {
    }
}
