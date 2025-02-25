using BuildingBlocks.Exceptions.Handler;

namespace OrderingAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication webApplication)
    {
        webApplication.MapCarter();

        webApplication.UseExceptionHandler(options => {/*emapty*/ });

        webApplication.UseHealthChecks("/health");

        return webApplication;
    }
}
