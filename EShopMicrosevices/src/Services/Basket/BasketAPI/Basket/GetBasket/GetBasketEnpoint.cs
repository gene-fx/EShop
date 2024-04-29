namespace BasketAPI.Basket.GetBasket
{
    //public record GetBasketRequest(string UserName);

    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEnpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var query = new GetBasketQuety(userName);

                var result = await sender.Send(query);

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            });
        }
    }
}