//#BASKET API

var builder = WebApplication.CreateBuilder(args);

//!Add services to the container

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogginBehavior<,>));

builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssembly(typeof(Program).Assembly);
    _.AddOpenBehavior(typeof(ValidationBehavior<,>));
    _.AddOpenBehavior(typeof(LogginBehavior<,>));
});

builder.Services.AddMarten(_ =>
{
    _.Connection(builder.Configuration.GetConnectionString("Database")!);
    _.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly((typeof(Program).Assembly));

builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

builder.Services.AddCarter();

var app = builder.Build();

//!Configure HTTP request pipeline
app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();