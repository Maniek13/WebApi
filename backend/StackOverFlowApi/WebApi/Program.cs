using Configuration.Extensions;

using WebApi.StartupTasks;

var builder = WebApplication.CreateBuilder(args);

builder.SetupWebApi();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    MigrationInitializer.ApplyMigrations(app.Services);

app.StartupWebApi(builder.Configuration);

app.UseHttpsRedirection();

app.Run();

public partial class Program { }