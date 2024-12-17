namespace OrderingDomain.Models
{
    public class Customer : Entity<CustomerId>
    {
        public string Name { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public static Customer Create(CustomerId customerId, string name, string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            var customer = new Customer
            {
                Id = customerId,
                Name = name,
                Email = email
            };

            NameNormalizer(customer);

            return customer;
        }

        internal static Customer NameNormalizer(Customer customer)
        {
            string[] names = customer.Name.Split(' ');

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = char.ToUpper(names[i][0]) + names[i].Substring(1);
            }

            return customer;
        }
    }
}
