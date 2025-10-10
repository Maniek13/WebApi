using Abstractions.SetUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Presentation.SetUp;


public class ModuleSetUp : IModuleSetup
{
    public void SetUp(WebApplication application, IConfigurationBuilder configuration)
    {
    }
}
