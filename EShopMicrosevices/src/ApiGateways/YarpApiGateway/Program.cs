var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var app = builder.Build();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//Configure the HTTP request pipeline.
app.MapReverseProxy();

app.Run();
