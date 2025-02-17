
namespace OrderingApplication.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Where(order => order.CustomerId == CustomerId.Of(query.CustomerId))
            .ToListAsync(cancellationToken).ContinueWith(result => result.Result as IReadOnlyCollection<Order>);

        throw new NotImplementedException();
    }
}
