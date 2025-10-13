using Configuration.Extensions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.SetupWebApi();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHangfireDashboard();

app.StartupWebApi(builder.Configuration);

app.UseHttpsRedirection();

app.Run();

public partial class Program { }