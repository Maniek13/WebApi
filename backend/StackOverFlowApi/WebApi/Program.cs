using Extensions;
using FastEndpoints;
using WebApi.StartUpTasks;

var builder = WebApplication.CreateBuilder(args);


builder.ConfigureModules(typeof(Persistence.Configuration.ModuleConfiguration).Assembly,
    typeof(Presentation.Configuration.ModuleConfiguration).Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    MigrationInitializer.ApplyMigrations(app.Services);

app.UseFastEndpoints()
    .UseSwagger();

app.SetUpModules(builder.Configuration, typeof(Presentation.SetUp.ModuleSetUp).Assembly);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();
