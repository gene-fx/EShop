using Microsoft.AspNetCore.Builder;

namespace OrderingInfrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedCustomerAsync(context);
        }

        private static async Task SeedCustomerAsync(ApplicationDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                context.Customers.AddRange(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }
    }
}
