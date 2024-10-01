namespace OrderingDomain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }

        private OrderItemId(Guid value) => Value = value;

        private static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if(value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty");
            }

            return new OrderItemId(value);
        }
    }
}
