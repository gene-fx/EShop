using BuildingBlocks.Pagination;
using OrderingApplication.Orders.Queries.GetOrders;

namespace OrderingAPI.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var query = new GetOrdersQuery(new PaginationRequest(request.PageIndex, request.PageSize));

            var result = await sender.Send(query);

            return Results.Ok(new GetOrdersResponse(result.PaginatedResult.Adapt<PaginatedResult<OrderDto>>()));
        })
        .WithName("GetOrders")
        .Accepts<PaginationRequest>("Accepst the pagination variables as a parameter")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders");
    }
}
