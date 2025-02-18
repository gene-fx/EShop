using OrderingApplication.Orders.Commands.UpdateOrder;

namespace OrderingAPI.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Adapt<UpdateOrderCommand>());

            return Results.Ok(result.Adapt<UpdateOrderResponse>());
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("UpdateOrder");
    }
}
