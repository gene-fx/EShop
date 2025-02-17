using BuildingBlocks.Pgination;

namespace OrderingApplication.Orders.Queries.GetOrders;
public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto>);