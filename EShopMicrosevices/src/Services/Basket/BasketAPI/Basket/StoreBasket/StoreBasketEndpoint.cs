using FastEndpoints;

namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketRequet(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint(ISender sender)
    : Endpoint<StoreBasketRequet, StoreBasketResponse>
{
    public override void Configure()
    {
        Post("/basket");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Store Basket";
            s.Description = "Add or Update basket";
            s.Response<StoreBasketResponse>(201, "Created");
            s.Response(400, "Bad Request");
        });
    }

    public override async Task HandleAsync(StoreBasketRequet req, CancellationToken ct)
    {
        var command = req.Adapt<StoreBasketCommand>();
        var result = await sender.Send(command, ct);
        var response = result.Adapt<StoreBasketResponse>();
        await Send.ResponseAsync(response);
    }
    //public void AddRoutes(IEndpointRouteBuilder app)
    //{
    //    Console.WriteLine("Mapping Store Basket Endpoint1");

    //    app.MapPost("/basket", async (StoreBasketRequet request, ISender sender) =>
    //    {
    //        Console.WriteLine("Mapping Store Basket Endpoint2");

    //        var command = request.Adapt<StoreBasketCommand>();

    //        var result = await sender.Send(command);

    //        return Results.Created($"/basket/{result.UserName}", result.Adapt<StoreBasketResponse>());
    //    })
    //    .WithName("StoreBasket")
    //    .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
    //    .ProducesProblem(StatusCodes.Status400BadRequest)
    //    .WithSummary("Store Basket")
    //    .WithDescription("Add or Update basket");
    //}
}