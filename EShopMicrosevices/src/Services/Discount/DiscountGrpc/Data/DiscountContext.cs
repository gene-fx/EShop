using DiscountGrpc.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Data;

public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions<DiscountContext> db) : base(db)
    {

    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "One Plus 21", Description = "OP from te future", Amount = 1 },
            new Coupon { Id = 2, ProductName = "MOTOBOSTA", Description = "QUALQUER MOTOBOMBA", Amount = 99 }
            );
    }
}
