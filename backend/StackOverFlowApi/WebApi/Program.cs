using Configuration.Extensions;
using Hangfire;
var builder = WebApplication.CreateBuilder(args);

builder.SetupWebApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolice",
        policy => policy
            .AllowAnyOrigin() 
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("CorsPolice");

app.StartupWebApi(builder.Configuration);

app.Run();

public partial class Program { }