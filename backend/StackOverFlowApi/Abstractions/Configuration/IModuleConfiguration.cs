using Microsoft.AspNetCore.Builder;

namespace Abstractions.Configuration;

public interface IModuleConfiguration
{
    public void SetUp(WebApplicationBuilder builder);
}
