namespace OrderingApplication.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.CustomerId)
            .Where(order => order.CustomerId == CustomerId.Of(query.CustomerId))
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ContinueWith(result => result.Result as IReadOnlyCollection<Order>);

        return new GetOrdersByCustomerResult(orders.ProjectToOrderDto());
    }
}
