namespace OrderingApplication.Orders.Queries.GetOrdersByName;
public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName.Value)
            .AsNoTracking()
            .ToListAsync();

        return new GetOrdersByNameResult(orders!.ProjectToOrderDto());
    }

    //private IReadOnlyList<OrderDto> ProjectToOrderDto(IReadOnlyList<OrderDto> orders)
    //{
    //    var result = new List<OrderDto>();

    //    foreach (var order in orders)
    //    {
    //        var orderDto = new OrderDto(
    //            Id: order.Id,
    //            CustomerID: order.CustomerID,
    //            OrderName: order.OrderName,
    //            ShippingAddress: new AddressDto(
    //                order.ShippingAddress.FirstName,
    //                order.ShippingAddress.LastName,
    //                order.ShippingAddress.EmailAddress,
    //                order.ShippingAddress.AddressLine,
    //                order.ShippingAddress.Country,
    //                order.ShippingAddress.State,
    //                order.ShippingAddress.ZipCode),
    //            BillingAddress: new AddressDto(
    //                order.BillingAddress.FirstName,
    //                order.BillingAddress.LastName,
    //                order.BillingAddress.EmailAddress,
    //                order.BillingAddress.AddressLine,
    //                order.BillingAddress.Country,
    //                order.BillingAddress.State,
    //                order.BillingAddress.ZipCode),
    //            Payment: new PaymentDto(
    //                order.Payment.CardName,
    //                order.Payment.CardNumber,
    //                order.Payment.Expiration,
    //                order.Payment.Cvv,
    //                order.Payment.PaymentMethod),
    //            Status: order.Status,
    //            OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList().AsReadOnly());

    //        result.Add(order);
    //    }

    //    return result.AsReadOnly();
    //}
}
