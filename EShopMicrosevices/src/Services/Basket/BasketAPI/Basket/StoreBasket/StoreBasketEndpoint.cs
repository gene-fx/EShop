namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketRequet(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequet request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command);

            return Results.Created($"/basket/{result.UserName}", result.Adapt<StoreBasketResponse>());
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Basket")
        .WithDescription("Add or Update basket");
    }
}