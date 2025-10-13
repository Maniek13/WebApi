using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Abstractions.Startup;

public interface IModuleStartup
{
    public void Startup(WebApplication application, IConfiguration configuration);
}
