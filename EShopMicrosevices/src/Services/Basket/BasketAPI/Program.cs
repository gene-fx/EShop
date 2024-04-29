//#BASKET API

var builder = WebApplication.CreateBuilder(args);

//!Add services to the container
builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssembly(typeof(Program).Assembly);
    _.AddOpenBehavior(typeof(ValidationBehavior<,>));
    _.AddOpenBehavior(typeof(LogginBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly((typeof(Program).Assembly));

builder.Services.AddCarter();

var app = builder.Build();

//!Configure HTTP request pipeline
app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();