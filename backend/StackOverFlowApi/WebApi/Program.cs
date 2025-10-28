using Configuration.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.SetupWebApi();


var app = builder.Build();

app.StartupWebApi(builder.Configuration);

app.Run();

public partial class Program { }