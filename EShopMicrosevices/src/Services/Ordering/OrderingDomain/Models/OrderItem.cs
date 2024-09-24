﻿namespace OrderingDomain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price) 
        {
            OrderId = OrderItemId.Of(Guid.NewGuid());
            ProductId = productId;
            Quantity = quantity;
            Price = price;    
        }

        public OrderId OrderId { get; private set; } = default!;

        public ProductId ProductId { get; private set; } = default!;

        public int Quantity { get; private set; } = default!;

        public decimal Price { get; private set; } = default!;
    }
}