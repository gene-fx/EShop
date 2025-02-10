namespace OrderingInfrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
        new List<Customer>()
        {
            Customer.Create(CustomerId.Of(Guid.Parse("d3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Alice Marie Smith", "alice.smith@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("e4b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Bob James Johnson", "bob.johnson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("f5b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Charlie David Williams", "charlie.williams@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("a6b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Dave Michael Brown", "dave.brown@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("b7b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Eve Elizabeth Jones", "eve.jones@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("c8b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Frank Thomas Garcia", "frank.garcia@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("d9b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Grace Anne Martinez", "grace.martinez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("eab07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Heidi Louise Rodriguez", "heidi.rodriguez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("fbb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Ivan Alexander Martinez", "ivan.martinez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("acb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Judy Patricia Hernandez", "judy.hernandez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("bdb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Mallory Sophia Lopez", "mallory.lopez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("ceb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Nathan Christopher Gonzalez", "nathan.gonzalez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("dfb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Olivia Grace Wilson", "olivia.wilson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("e0b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Peggy Rose Anderson", "peggy.anderson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("f1b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Quinn Daniel Thomas", "quinn.thomas@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("a2b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Rachel Emily Taylor", "rachel.taylor@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("b3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Sam Henry Moore", "sam.moore@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("c4b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Trudy Jane Jackson", "trudy.jackson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("d5b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Ursula Marie White", "ursula.white@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("e6b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Victor James Harris", "victor.harris@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("f7b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Walter John Martin", "walter.martin@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("a8b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Xander Michael Thompson", "xander.thompson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("b9b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Yvonne Elizabeth Martinez", "yvonne.martinez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("cab07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Zach David Garcia", "zach.garcia@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("dbb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Amy Louise Martinez", "amy.martinez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("ecb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Brian James Hernandez", "brian.hernandez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("fdb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Carol Marie Lopez", "carol.lopez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("aeb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Dan Michael Gonzalez", "dan.gonzalez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("bfb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Ella Grace Wilson", "ella.wilson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("d0b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Fred Thomas Anderson", "fred.anderson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("e1b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Gina Elizabeth Thomas", "gina.thomas@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("f2b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Harry James Taylor", "harry.taylor@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("a3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Isabel Marie Moore", "isabel.moore@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("b4b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Jack Michael Jackson", "jack.jackson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("c5b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Karen Louise Martinez", "karen.martinez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("d6b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Leo James Hernandez", "leo.hernandez@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("e7b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Mia Elizabeth Lopez", "mia.lopez@example.com")
        };

    public static IEnumerable<Product> Products =>
        new List<Product>()
        {
            Product.Create(ProductId.Of(Guid.Parse("d3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product1", decimal.Parse("100.50")),
            Product.Create(ProductId.Of(Guid.Parse("e4b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product2", decimal.Parse("200.75")),
            Product.Create(ProductId.Of(Guid.Parse("f5b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product3", decimal.Parse("300.40")),
            Product.Create(ProductId.Of(Guid.Parse("a6b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product4", decimal.Parse("400.20")),
            Product.Create(ProductId.Of(Guid.Parse("b7b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product5", decimal.Parse("500.60")),
            Product.Create(ProductId.Of(Guid.Parse("c8b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product6", decimal.Parse("600.80")),
            Product.Create(ProductId.Of(Guid.Parse("d9b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product7", decimal.Parse("700.90")),
            Product.Create(ProductId.Of(Guid.Parse("eab07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product8", decimal.Parse("800.30")),
            Product.Create(ProductId.Of(Guid.Parse("fbb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product9", decimal.Parse("900.10")),
            Product.Create(ProductId.Of(Guid.Parse("acb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product10", decimal.Parse("1000.50")),
            Product.Create(ProductId.Of(Guid.Parse("bdb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product11", decimal.Parse("1100.75")),
            Product.Create(ProductId.Of(Guid.Parse("ceb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product12", decimal.Parse("1200.40")),
            Product.Create(ProductId.Of(Guid.Parse("dfb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product13", decimal.Parse("1300.20")),
            Product.Create(ProductId.Of(Guid.Parse("e0b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product14", decimal.Parse("1400.60")),
            Product.Create(ProductId.Of(Guid.Parse("f1b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product15", decimal.Parse("1500.80")),
            Product.Create(ProductId.Of(Guid.Parse("a2b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product16", decimal.Parse("1600.90")),
            Product.Create(ProductId.Of(Guid.Parse("b3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product17", decimal.Parse("1700.30")),
            Product.Create(ProductId.Of(Guid.Parse("c4b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product18", decimal.Parse("1800.10")),
            Product.Create(ProductId.Of(Guid.Parse("d5b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product19", decimal.Parse("1900.50")),
            Product.Create(ProductId.Of(Guid.Parse("e6b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product20", decimal.Parse("2000.75")),
            Product.Create(ProductId.Of(Guid.Parse("f7b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product21", decimal.Parse("2100.40")),
            Product.Create(ProductId.Of(Guid.Parse("a8b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product22", decimal.Parse("2200.20")),
            Product.Create(ProductId.Of(Guid.Parse("b9b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product23", decimal.Parse("2300.60")),
            Product.Create(ProductId.Of(Guid.Parse("cab07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product24", decimal.Parse("2400.80")),
            Product.Create(ProductId.Of(Guid.Parse("dbb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product25", decimal.Parse("2500.90")),
            Product.Create(ProductId.Of(Guid.Parse("ecb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product26", decimal.Parse("2600.30")),
            Product.Create(ProductId.Of(Guid.Parse("fdb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product27", decimal.Parse("2700.10")),
            Product.Create(ProductId.Of(Guid.Parse("aeb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product28", decimal.Parse("2800.50")),
            Product.Create(ProductId.Of(Guid.Parse("bfb07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product29", decimal.Parse("2900.75")),
            Product.Create(ProductId.Of(Guid.Parse("d0b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product30", decimal.Parse("3000.40")),
            Product.Create(ProductId.Of(Guid.Parse("e1b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), "Product31", decimal.Parse("3100.20"))
        };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            Address address1 = Address.Of("Alice", "Marie Smith", "alice.smith@example.com", "123 Main St", "USA", "CA", "90210");

            Payment payment1 = Payment.Of("Alice Marie Smith", "1234 5678 9012 3456", "12/25", "123", 1);

            Order order1 = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.Parse("d3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")),
                orderName: OrderName.Of(payment1.CardName!), shippingAddress: address1, billingAddress: address1, payment: payment1);

            order1.Add(ProductId.Of(Guid.Parse("d3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), 5, decimal.Parse("100.50"));
            order1.Add(ProductId.Of(Guid.Parse("e4b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), 3, decimal.Parse("200.75"));

            Address address2 = Address.Of("John", "Doe", "john.doe@example.com", "456 Elm St", "USA", "NY", "10001");

            Payment payment2 = Payment.Of("John Doe", "9876 5432 1098 7654", "11/24", "456", 2);

            Order order2 = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.Parse("e4b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")),
                orderName: OrderName.Of(payment2.CardName!), shippingAddress: address2, billingAddress: address2, payment: payment2);

            order2.Add(ProductId.Of(Guid.Parse("f5b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), 2, decimal.Parse("300.40"));
            order2.Add(ProductId.Of(Guid.Parse("d3b07384-d9a1-4c9e-8f6d-1a2b3c4d5e6f")), 3, decimal.Parse("100.50"));

            return new List<Order> { order1, order2 };
        }
    }
}
