using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderingInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(x =>
            //{
            //    x.UseSqlServer(configuration.GetConnectionString("Database")!);
            //});

            //serices.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
