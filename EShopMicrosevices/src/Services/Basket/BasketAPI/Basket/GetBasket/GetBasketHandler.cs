namespace BasketAPI.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);

    internal class GetBasketQueryHandler
        (IUnityOfWork unityOfWork)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var result = await unityOfWork.BasketRepository.Get(x => x.UserName == query.UserName);

            return new GetBasketResult(result);
        }
    }
}