using FastEndpoints;

namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckout);

public record CheckoutBasketEndpointResponse(bool IsSuccess, Guid? OrederId, string? Error = null);

public class CheckoutBasketEndpoint(ISender sender)
    : Endpoint<CheckoutBasketRequest, CheckoutBasketEndpointResponse>
{
    public override void Configure()
    {
        Post("/basket/checkout");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Checkout Basket";
            s.Description = "Checkout Basket";
            s.Response<CheckoutBasketEndpointResponse>(201, "Success");
            s.Response<CheckoutBasketEndpointResponse>(500, "Internal Server Error");
        });
    }

    public override async Task HandleAsync(CheckoutBasketRequest req, CancellationToken ct)
    {
        CheckoutBasketCommand command = new CheckoutBasketCommand(req.BasketCheckout);

        CheckoutBasketResult result = await sender.Send(command, ct);

        CheckoutBasketEndpointResponse endpointResponse = result.Adapt<CheckoutBasketEndpointResponse>();

        if (result.IsSuccess == false)
            await Send.ResultAsync(TypedResults.InternalServerError(endpointResponse));
        else
            await Send.ResultAsync(TypedResults.Created(endpointResponse.OrederId.ToString(), endpointResponse));
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
