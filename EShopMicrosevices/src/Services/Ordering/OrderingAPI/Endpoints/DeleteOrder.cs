using OrderingApplication.Orders.Commands.DeleteOrder;

namespace OrderingAPI.Endpoints;

//public record DeleteOrderRequest(Guid Id);

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders/{id}", (Guid id, ISender sender) =>
        {
            var result = sender.Send(new DeleteOrderCommand(id));

            return Results.Ok(result.Adapt<DeleteOrderResponse>());
        })
        .WithName("DeleteOrder")
        .Accepts<Guid>("Accepts the Order ID")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("DeleteOrder");
    }
}
