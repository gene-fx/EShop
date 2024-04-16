var builder = WebApplication.CreateBuilder(args);
//Add services to the container.

builder.Services.AddCarter();

builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(_ =>
{
    _.Connection(builder.Configuration.GetConnectionString("Postgres")!);
}).UseLightweightSessions();


var app = builder.Build();
//Configure HTTP request pipeline

app.MapCarter();

app.Run();
