using GestionTareas.Api.MiddleWare;
using GestionTareas.Application;
using GestionTareas.Infrastructure.DataBase;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues[key: "controller"]}_{e.ActionDescriptor.RouteValues["action"]}");
});

//Infrastructure
builder.Services.AddMongoContext();

//Core
builder.Services.AddUseCases();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();


Console.WriteLine($"........................................");
Console.WriteLine($"INICIADO...");
Console.WriteLine($"........................................");
app.Run();