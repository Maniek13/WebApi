using Configuration.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;

using WebApi.StartupTasks;

var builder = WebApplication.CreateBuilder(args);

builder.SetupWebApi();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    MigrationInitializer.ApplyMigrations(app.Services);

app.UseFastEndpoints();

app.StartupWebApi(builder.Configuration);


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

public partial class Program { }