namespace OrderingApplication.Dtos;
public record OrderDto
(
    Guid OrderId,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    IReadOnlyList<OrderItemDto> OrderItems
);
