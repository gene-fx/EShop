namespace OrderingDomain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLenght = 5;

        public string Value { get; }

        private OrderName(string value) => Value = value;

        private static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            ArgumentOutOfRangeException.ThrowIfLessThan(value.Length, DefaultLenght);

            return new OrderName(value);
        }
    }
}
