using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

var app = builder.Build();

app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();
