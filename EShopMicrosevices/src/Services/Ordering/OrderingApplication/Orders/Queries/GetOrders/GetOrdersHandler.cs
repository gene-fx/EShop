
namespace OrderingApplication.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(query.PaginationRequest.PageSize * query.PaginationRequest.PageIndex)
            .ToListAsync()
            .ContinueWith(o => o as IReadOnlyCollection<Order>);

        return new GetOrdersResult(
            new BuildingBlocks.Pgination.PaginatedResult<OrderDto>(
                query.PaginationRequest.PageIndex,
                query.PaginationRequest.PageSize,
                totalCount,
                orders!.ProjectToOrderDto()));
    }
}
