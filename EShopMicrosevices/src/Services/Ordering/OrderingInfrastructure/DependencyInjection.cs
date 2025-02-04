using OrderingInfrastructure.Data.Interceptors;

namespace OrderingInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.AddInterceptors(new AuditableEntityInterceptor());
                x.UseSqlServer(configuration.GetConnectionString("Database")!);
            });

            services.AddScoped<ApplicationDbContext>();

            return services;
        }
    }
}
