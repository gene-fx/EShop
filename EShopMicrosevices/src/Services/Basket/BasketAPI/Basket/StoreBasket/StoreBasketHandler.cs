namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) 
        : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(model => model.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(model => model.Cart.UserName).NotEmpty().WithMessage("User Name is required");
        }
    }

    public class StoreBasketCommandHandler
        (IUnityOfWork unityOfWork)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var result = await unityOfWork.BasketRepository.Store(command.Cart);

            await unityOfWork.Commit(cancellationToken);

            return new StoreBasketResult(result.UserName);
        }
    }
}