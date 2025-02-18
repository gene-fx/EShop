namespace OrderingAPI.Endpoints;

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Adapt<CreateOrderCommand>());

            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/oreders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Accepts<CreateOrderRequest>("CreateOrderRequest object - Order")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order");
    }
}
