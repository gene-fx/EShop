using OrderingDomain.Abstractions;

namespace OrderingDomain.Models
{
    public class Order : Aggregate<Guid>
    {
        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Guid CustomerID { get; private set; } = default!;

        public string OrderName { get; private set; } = default!;

        public Adress ShippingAdress { get; private set; } = default!;

        public Adress BillingAdress { get; private set; } = default!;

        public Payment Payment { get; private set; } = default!;

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.TotalPrice * x.Quantity);
            private set { }
        }
    }
}
