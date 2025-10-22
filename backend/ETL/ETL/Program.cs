using Application.Instalation;
using Infrastructure.Instalation;
using Persistence.Instalation;

var builder = WebApplication.CreateBuilder(args);

builder.PersistenceSetup();
builder.InfrastructoreSetup();
builder.ApplicationSetup();

var app = builder.Build();

app.UseHttpsRedirection();

app.InfrastructoreStartup();

app.Run();
