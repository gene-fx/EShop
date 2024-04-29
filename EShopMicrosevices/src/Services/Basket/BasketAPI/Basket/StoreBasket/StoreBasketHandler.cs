namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

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
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO: store ShoppingCart into Db

            return new StoreBasketResult(command.Cart.UserName);
        }
    }
}