
namespace OrderingInfrastructure.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id)
                .HasConversion(productId => productId.Value,
                                dbId => ProductId.Of(dbId));//! HasConversion converts the value from and of database to ValueObject,
                                                            //! in this case, it converts the key (GUID) from db to ProductId and vice-versa.

            builder.Property(product => product.Name).HasMaxLength(100).IsRequired();

            builder.Property(product => product.Price).IsRequired();
        }
    }
}
