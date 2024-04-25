//#BASKET API

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssembly(typeof(Program).Assembly);
});


builder.Services.AddCarter();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();