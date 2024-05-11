using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Data
{
    public static class Extensions
    {
        public static async Task<IApplicationBuilder> UseMigration(this IApplicationBuilder app) 
        {
            using var scope = app.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();

            await dbContext.Database.MigrateAsync();

            return app;
        }
    }
}
