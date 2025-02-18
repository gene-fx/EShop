using BuildingBlocks.Pgination;
using OrderingApplication.Orders.Queries.GetOrders;

namespace OrderingAPI.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = sender.Send(request.Adapt<GetOrdersQuery>());

            return Results.Ok(result.Adapt<GetOrdersResponse>());
        })
        .WithName("GetOrders")
        .Accepts<PaginationRequest>("Accepst the pagination variables as a parameter")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders");
    }
}
