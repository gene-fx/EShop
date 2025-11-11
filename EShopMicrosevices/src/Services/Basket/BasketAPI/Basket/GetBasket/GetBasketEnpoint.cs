using FastEndpoints;

namespace BasketAPI.Basket.GetBasket;

public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEnpoint(ISender sender)
    : Endpoint<GetBasketRequest, GetBasketResponse>
{
    public override void Configure()
    {
        Get("/basket/{userName}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get Basket";
            s.Description = "Get Basket";
            s.Response<GetBasketResponse>(200, "Success");
            s.Response(400, "Bad Request");
        });
    }

    public override async Task HandleAsync(GetBasketRequest userName, CancellationToken ct)
    {
        var query = userName.Adapt<GetBasketQuery>();
        var result = await sender.Send(query, ct);
        var response = result.Adapt<GetBasketResponse>();
        await Send.ResponseAsync(response, cancellation: ct);
    }
    //public void AddRoutes(IEndpointRouteBuilder app)
    //{
    //    app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
    //    {
    //        var query = new GetBasketQuery(userName);

    //        var result = await sender.Send(query);

    //        var response = result.Adapt<GetBasketResponse>();

    //        return Results.Ok(response);
    //    })
    //    .WithName("GetBasket")
    //    .Produces<GetBasketResponse>(StatusCodes.Status200OK)
    //    .ProducesProblem(StatusCodes.Status400BadRequest)
    //    .WithSummary("Get Basket")
    //    .WithDescription("Get Basket");
    //}
}