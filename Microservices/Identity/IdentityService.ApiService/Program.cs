using System.Reflection;

using Carter;

using SharedLib.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.AddServiceInstallers();

var app = builder.Build();

app.UseHttpLogging();

app.MapCarter();

app.Run();