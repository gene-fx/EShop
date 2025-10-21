using FastEndpoints;

namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint(ISender sender)
    : Endpoint<CheckoutBasketRequest, CheckoutBasketResponse>
{
    public override void Configure()
    {
        Post("/basket/checkout");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Checkout Basket";
            s.Description = "Checkout Basket";
            s.Response<CheckoutBasketResponse>(201, "Success");
            s.Response(400, "Bad Request");
        });
    }

    public override async Task HandleAsync(CheckoutBasketRequest req, CancellationToken ct)
    {
        CheckoutBasketCommand command = new CheckoutBasketCommand(req.BasketCheckoutDto);

        var result = await sender.Send(command, ct);

        var response = result.Adapt<CheckoutBasketResponse>();

        await Send.CreatedAtAsync(req.BasketCheckoutDto.UserName, response);
    }

    //public void AddRoutes(IEndpointRouteBuilder app)
    //{

    //    Console.WriteLine("Mapping Checkout Basket Endpoint1");

    //    app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
    //    {
    //        Console.WriteLine("Mapping Checkout Basket Endpoint2");

    //        var command = request.Adapt<CheckoutBasketCommand>();

    //        var result = await sender.Send(command);

    //        var response = result.Adapt<CheckoutBasketResponse>();

    //        return Results.Ok(response);
    //    })
    //    .WithName("CheckoutBasket")
    //    .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
    //    .ProducesProblem(StatusCodes.Status400BadRequest)
    //    .WithSummary("Checkout Basket")
    //    .WithDescription("Checkout Basket");
    //}
}
