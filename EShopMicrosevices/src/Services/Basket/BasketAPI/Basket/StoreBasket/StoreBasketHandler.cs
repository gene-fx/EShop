using Discount.Grpc;
using static Discount.Grpc.DiscountProtoService;

namespace BasketAPI.Basket.StoreBasket;

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
    (IBasketRepository basketRepository,
    DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await VerifyDiscount(command.Cart);

        var result = await basketRepository.Store(command.Cart, cancellationToken);

        await basketRepository.Commit(cancellationToken);

        return new StoreBasketResult(result.UserName);
    }

    private async Task VerifyDiscount(ShoppingCart Cart)
    {
        foreach (var item in Cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });

            if (string.IsNullOrEmpty(coupon.ProductName))
                continue;

            if (coupon.Over > 0 && item.Quantity >= coupon.Over)
                item.Price -= coupon.OverAmount;
            else
                item.Price -= coupon.Amount;
        }
    }
}