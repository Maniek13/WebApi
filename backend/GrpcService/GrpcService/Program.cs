using Application.Instalation;
using Infrastructure.Instalation;

var builder = WebApplication.CreateBuilder(args);

builder.InfrastructoreSetup();
builder.ApplicationSetup();

var app = builder.Build();

app.InfrastructoreStartup();

app.Run();
