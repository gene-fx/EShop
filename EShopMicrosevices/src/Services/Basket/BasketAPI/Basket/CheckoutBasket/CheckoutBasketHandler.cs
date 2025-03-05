using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout)
    : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

//Validation
public class CheckoutBasketCommandValidator
    : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckout).NotNull().NotEmpty()
            .WithMessage("BasketCheckout object can't be null or empty");
        RuleFor(x => x.BasketCheckout.UserName).NotEmpty()
            .WithMessage("BasketCheckout UserName is required");
    }
}

public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.Get(command.BasketCheckout.UserName, cancellationToken);

        if (basket == null) return new CheckoutBasketResult(false);

        var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutEvent>();

        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.Delete(command.BasketCheckout.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
