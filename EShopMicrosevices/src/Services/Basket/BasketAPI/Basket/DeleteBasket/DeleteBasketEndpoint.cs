using FastEndpoints;

namespace BasketAPI.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint(ISender sender)
    : Endpoint<DeleteBasketRequest, DeleteBasketResponse>
{
    public override void Configure()
    {
        Delete("/basket/{userName}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Delete Basket";
            s.Description = "Delete Basket";
            s.Response<DeleteBasketResponse>(200, "Success");
            s.Response(404, "Basket Not Found");
        });
    }

    public override async Task HandleAsync(DeleteBasketRequest req, CancellationToken ct)
    {
        var command = req.Adapt<DeleteBasketCommand>();
        var result = await sender.Send(command, ct);
        var response = result.Adapt<DeleteBasketResponse>();
        await Send.ResponseAsync(response, cancellation: ct);
    }

    //public void AddRoutes(IEndpointRouteBuilder app)
    //{
    //    app.MapDelete("/basket/{userName}", async (string userName) =>
    //    {
    //        var request = new DeleteBasketRequest(userName);

    //        var command = request.Adapt<DeleteBasketCommand>();

    //        var response = await sender.Send(command);

    //        return Results.Ok(response.Adapt<DeleteBasketResponse>());
    //    })
    //    .WithName("DeleteBasket")
    //    .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
    //    .ProducesProblem(StatusCodes.Status400BadRequest)
    //    .WithSummary("Delete Basket")
    //    .WithDescription("Delete Basket");
    //}
}