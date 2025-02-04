namespace OrderingInfrastructure.Data.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
            .HasConversion(customerId => customerId.Value,
                            dbId => CustomerId.Of(dbId));//! HasConversion converts the value from and of database to ValueObject,
                                                         //! in this case, it converts the key (GUID) from db to CustomerId and vice-versa.

        builder.Property(customer => customer.Name).HasMaxLength(150).IsRequired();

        builder.Property(customer => customer.Email).HasMaxLength(255).IsRequired();

        builder.HasIndex(customer => customer.Email).IsUnique();
    }
}
