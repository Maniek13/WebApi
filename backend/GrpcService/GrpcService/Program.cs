using Application.Instalation;
using Infrastructure.Instalation;
using Persistence.Instalation;

var builder = WebApplication.CreateBuilder(args);

builder.InfrastructoreSetup();
builder.ApplicationSetup();
builder.PersistenceSetup();

var app = builder.Build();

app.InfrastructoreStartup();

app.Run();
