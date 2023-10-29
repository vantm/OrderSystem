using Carter;

using SharedLib.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.AddServiceInstallers();

var app = builder.Build();

app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();