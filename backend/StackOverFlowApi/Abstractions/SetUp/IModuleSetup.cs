using Microsoft.AspNetCore.Builder;

namespace Abstractions.Setup;

public interface IModuleSetup
{
    public void Setup(WebApplicationBuilder builder);
}
