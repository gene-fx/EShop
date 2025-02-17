namespace OrderingApplication.Extensions;
public static class OrderExtions
{
    public static IReadOnlyCollection<OrderDto> ProjectToOrderDto(this IReadOnlyCollection<Order> orders)
    {
        return orders.Select(order => new OrderDto(
                Id: Guid.Parse(order.Id.ToString()),
                CustomerID: Guid.Parse(order.CustomerId.ToString()),
                OrderName: order.OrderName.ToString(),
                ShippingAddress: new AddressDto(
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode),
                BillingAddress: new AddressDto(
                    order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode),
                Payment: new PaymentDto(
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi =>
                    new OrderItemDto(Guid.Parse(oi.OrderId.ToString()), Guid.Parse(oi.ProductId.ToString()), oi.Quantity, oi.Price)).ToList().AsReadOnly()
            )).ToList().AsReadOnly();
    }
}
