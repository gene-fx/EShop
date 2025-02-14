namespace OrderingApplication.Dtos;
public record OrderDto
(
    Guid Id,
    Guid CustomerID,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    IReadOnlyList<OrderItemDto> OrderItems
);
