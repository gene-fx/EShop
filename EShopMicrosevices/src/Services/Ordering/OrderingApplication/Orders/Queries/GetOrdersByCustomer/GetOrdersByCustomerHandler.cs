namespace OrderingApplication.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(order => order.CustomerId == CustomerId.Of(query.CustomerId))
            .OrderBy(o => o.OrderName.Value)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.ProjectToOrderDto());
    }
}
