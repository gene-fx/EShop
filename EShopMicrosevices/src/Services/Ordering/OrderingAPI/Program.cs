using OrderingAPI;
using OrderingAPI.FileWatcher;
using OrderingApplication;
using OrderingInfrastructure;
using OrderingInfrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//TODO Add services to the container.

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddHostedService<OrderingFileWatcher>();

var app = builder.Build();

//TODO Configure the HTTP request pipeline.

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
