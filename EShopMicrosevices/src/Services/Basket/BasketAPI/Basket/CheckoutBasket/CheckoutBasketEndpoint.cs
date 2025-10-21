namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        Console.WriteLine("Mapping Checkout Basket Endpoint1");

        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            Console.WriteLine("Mapping Checkout Basket Endpoint2");

            var command = request.Adapt<CheckoutBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CheckoutBasketResponse>();

            return Results.Ok(response);
        });
        //.WithName("CheckoutBasket")
        //.Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
        //.ProducesProblem(StatusCodes.Status400BadRequest)
        //.WithSummary("Checkout Basket")
        //.WithDescription("Checkout Basket");
    }
}
