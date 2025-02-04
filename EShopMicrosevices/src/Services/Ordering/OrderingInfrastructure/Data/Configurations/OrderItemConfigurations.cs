namespace OrderingInfrastructure.Data.Configurations;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.Id)
            .HasConversion(orderItemId => orderItemId.Value,
                           dbId => OrderItemId.Of(dbId));//! HasConversion converts the value from and of database to ValueObject,
                                                         //! in this case, it converts the key (GUID) from db to OrderItemId and vice-versa.

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId);//! OrderItem  n - 1 Product
                                                             // meaning that each OrderItem is associated with onde Product,
                                                             // but a single Product can be associated with multiple OrderItems.

        builder.Property(orderItem => orderItem.Price).IsRequired();

        builder.Property(orderItem => orderItem.Quantity).IsRequired();

        builder.Property(orderItem => orderItem.ProductId).IsRequired();
    }
}
