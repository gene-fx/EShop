namespace ShoppingWeb.Models.Ordering;

public record OrderModel
(
    Guid OrderId,
    Guid CustomerId,
    string OrderName,
    AddressModel ShippingAddress,
    AddressModel BillingAddress,
    PaymentModel Payment,
    OrderStatus Status,
    IReadOnlyList<OrderItemModel> OrderItems
);

public record AddressModel
(
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode
);

public record OrderItemModel
(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price
);

public record PaymentModel
(
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv,
    int PaymentMethod
);

public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Completed = 3,
    Cancelled = 4
}