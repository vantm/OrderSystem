using Carter;

using SharedLib.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.AddServiceInstallers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.MapCarter();

app.Run();