
using Mapster;

namespace BasketAPI.Basket.DeleteBasket
{
    public record DeleteBaskettRequest(string UserName);

    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoint(ISender sender)
        : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName) =>
            {
                //ISender sends a Request adapted as Command
                return Results.Ok(await sender.Send(new DeleteBaskettRequest(userName).Adapt<DeleteBasketCommand>()));
            });

        }
    }
}
