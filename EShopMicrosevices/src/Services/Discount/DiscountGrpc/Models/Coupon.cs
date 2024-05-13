namespace DiscountGrpc.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int Amount { get; set; }

        public int Over { get; set; }

        public int OverAmount { get; set; }
    }
}
