using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Abstractions.SetUp;

public interface IModuleSetup
{
    public void SetUp(WebApplication application, IConfigurationBuilder configuration);
}
