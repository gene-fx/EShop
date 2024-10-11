//TODO: End the Order complex properties entity configuration


namespace OrderingInfrastructure.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);

            builder.Property(order => order.Id)
                .HasConversion(orderId => orderId.Value,
                                dbId => OrderId.Of(dbId));//! HasConversion converts the value from and of database to ValueObject,
                                                          //! in this case, it converts the key (GUID) from db to OrderId and vice-versa.

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(order => order.CustomerId)
                .IsRequired();                            //! Order  n - 1 Cusotmer
                                                          //! /\ It means that a Order have only one custume,
                                                          //! but a Customer can be realeted to multiple Orders

            builder.HasMany<OrderItem>()
                .WithOne()
                .HasForeignKey(orderItem => orderItem.OrderId);//! Order 1 - n OrderItem
                                                               //! it represents the relation between one Order with many OrderItems

            builder.ComplexProperty(order => order.OrderName, orderName =>
            {
                orderName.Property(orderName => orderName.Value)
                        .HasColumnName(nameof(OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
            });

            builder.ComplexProperty(order => order.ShippingAddress, orderShippingAddress =>
            {
                orderShippingAddress.Property(orderShippingAddress => orderShippingAddress.FirstName)
                                    .HasMaxLength(50)
                                    .IsRequired();

                orderShippingAddress.Property(orderShippingAddress => orderShippingAddress.LastName)
                                    .HasMaxLength(50)
                                    .IsRequired();

            });
        }
    }
}
