var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();
