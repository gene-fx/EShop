namespace BasketAPI.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
    {
        public GetBasketQueryValidator()
        {
            RuleFor(model => model.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserName is required");
        }
    }

    internal class GetBasketQueryHandler
        (IUnityOfWork unityOfWork)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var result = await unityOfWork.BasketRepository.Get(x => x.UserName == query.UserName);

            if(result is null)
            {
                throw new BasketNotFoundException(query.UserName);
            }

            return new GetBasketResult(result);
        }
    }
}