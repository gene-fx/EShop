using OrderingDomain.Enums;

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
                                                          //! /\ It means that an Order have only one custumer,
                                                          //! but a Customer can be realeted to multiple Orders

            builder.HasMany<OrderItem>()
                .WithOne()
                .HasForeignKey(orderItem => orderItem.OrderId);//! Order 1 - n OrderItem
                                                               //! it represents the relation between one Order with many OrderItems

            builder.ComplexProperty(order => order.OrderName, orderName => //! It defines complexes properties like
            {                                                              //! OrderName that has its won properties
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

                orderShippingAddress.Property(orderShippingAddress => orderShippingAddress.EmailAddress)
                                    .HasMaxLength(100)
                                    .IsRequired();

                orderShippingAddress.Property(orderShippingAddress => orderShippingAddress.AddressLine)
                                    .HasMaxLength(200)
                                    .IsRequired();

                orderShippingAddress.Property(orderShippingAddress => orderShippingAddress.ZipCode)
                                    .IsRequired();

                orderShippingAddress.Property(orderShippingAddress => orderShippingAddress.Country)
                                    .IsRequired();

                orderShippingAddress.Property(orderShippingAddress => orderShippingAddress.State)
                                    .IsRequired();

            });

            builder.ComplexProperty(order => order.BillingAdress, orderBillingAdress =>
            {
                orderBillingAdress.Property(prop => prop.FirstName)
                                .HasMaxLength(50)
                                .IsRequired();

                orderBillingAdress.Property(prop => prop.LastName)
                                .HasMaxLength(50)
                                .IsRequired();

                orderBillingAdress.Property(prop => prop.EmailAddress)
                                .HasMaxLength(50)
                                .IsRequired();

                orderBillingAdress.Property(prop => prop.AddressLine)
                                .HasMaxLength(180)
                                .IsRequired();

                orderBillingAdress.Property(prop => prop.Country)
                                .HasMaxLength(50)
                                .IsRequired();

                orderBillingAdress.Property(prop => prop.State)
                                .HasMaxLength(50)
                                .IsRequired();

                orderBillingAdress.Property(prop => prop.ZipCode)
                                .HasMaxLength(5)
                                .IsRequired();
            });

            builder.ComplexProperty(order => order.Payment, payment =>
            {
                payment.Property(p => p.CardName)
                        .HasMaxLength(50);

                payment.Property(p => p.CardNumber)
                        .HasMaxLength(24)
                        .IsRequired();

                payment.Property(p => p.Expiration)
                        .HasMaxLength(10);

                payment.Property(p => p.CVV)
                        .HasMaxLength(3)
                        .IsRequired();

                payment.Property(p => p.PaymentMethod)
                        .IsRequired();
            });

            builder.Property(order => order.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(status => status.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(order => order.TotalPrice);
        }
    }
}
