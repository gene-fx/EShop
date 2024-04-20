using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.

builder.Services.AddCarter();

builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssembly(typeof(Program).Assembly);
    _.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddMarten(_ =>
{
    _.Connection(builder.Configuration.GetConnectionString("Postgres")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly((typeof(Program).Assembly));

var app = builder.Build();
//Configure HTTP request pipeline

app.MapCarter();

app.Run();
 