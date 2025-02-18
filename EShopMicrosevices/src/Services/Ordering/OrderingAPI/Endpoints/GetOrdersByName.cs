using OrderingApplication.Orders.Queries.GetOrdersByName;

namespace OrderingAPI.Endpoints;

//public record GetOrdersByMameRequest(string Name);

public record GetOrdersByMameResponse(IReadOnlyCollection<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/{name}", async (string name, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByNameQuery(name));

            return Results.Ok(result.Adapt<GetOrdersByMameResponse>());
        })
        .WithName("GetOredersByName")
        .Accepts<string>("Accepts the Order Name")
        .Produces<GetOrdersByMameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("GetOrderByName");
    }
}
