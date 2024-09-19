using OrderingAPI;
using OrderingApplication;
using OrderingInfrastructure;

var builder = WebApplication.CreateBuilder(args);

//TODO Add services to the container.

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

//TODO Configure the HTTP request pipeline.

app.MapGet("/", () => "Hello World!");

app.Run();
