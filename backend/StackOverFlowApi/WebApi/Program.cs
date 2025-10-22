using Configuration.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.SetupWebApi();


var app = builder.Build();

app.StartupWebApi(builder.Configuration);

app.Run();

public partial class Program { }