using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;

namespace OrderingApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            x.AddOpenBehavior(typeof(ValidationBehavior<,>));
            x.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
