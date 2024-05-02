namespace BasketAPI.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string UserName);

    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoint(ISender sender)
        : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName) =>
            {
                var request = new DeleteBasketRequest(userName);

                var command = request.Adapt<DeleteBasketCommand>();

                var response = await sender.Send(command);

                return Results.Ok(response.Adapt<DeleteBasketResponse>());
            });
        }
    }
}