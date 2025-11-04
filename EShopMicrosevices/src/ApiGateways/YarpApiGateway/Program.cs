using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", config =>
    {
        config.PermitLimit = 5;
        config.Window = TimeSpan.FromSeconds(10);
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        config.QueueLimit = 0;
    });

    options.RejectionStatusCode = 429;


    options.OnRejected = async (context, token) =>
    {
        var requestingIp = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unkown";
        context.HttpContext.Response.StatusCode = 429;
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.Headers.RetryAfter = "10";
        await context.HttpContext.Response.WriteAsync($"Too many requests from ip {requestingIp}. Please try again later.", token);
    };
});

var app = builder.Build();
//Configure the HTTP request pipeline.

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
