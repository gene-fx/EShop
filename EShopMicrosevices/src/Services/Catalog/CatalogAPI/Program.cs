using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.

builder.Services.AddMediatR(_ =>
{
    _.RegisterServicesFromAssembly(typeof(Program).Assembly);
    _.AddOpenBehavior(typeof(ValidationBehavior<,>));
    _.AddOpenBehavior(typeof(LogginBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly((typeof(Program).Assembly));

builder.Services.AddCarter();

builder.Services.AddMarten(_ =>
{
    _.Connection(builder.Configuration.GetConnectionString("Postgresql")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Postgresql")!);

var app = builder.Build();
//Configure HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
 