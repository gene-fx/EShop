using BasketAPI.Models;
using BuildingBlocks.CQRS;

namespace BasketAPI.Basket.GetBasket
{
    public record GetBasketQuety(string Name) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);

    internal class GetBasketQueryHandler : IQueryHandler<GetBasketQuety, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuety request, CancellationToken cancellationToken)
        {

            //TODO: get basket from DB
            //var basket = await _repository.Get();                                                                                                                                      
            return new GetBasketResult(new ShoppingCart("coco"));
        }
    }
}
