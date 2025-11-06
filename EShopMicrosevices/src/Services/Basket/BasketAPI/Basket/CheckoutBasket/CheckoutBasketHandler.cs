using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.Responses;
using MassTransit;

namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout)
    : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess, Guid? OrderId, string? Error = null);

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

public class CheckoutBasketHandler(IBasketRepository repository, IRequestClient<BasketCheckoutEvent> client)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.Get(command.BasketCheckout.UserName, cancellationToken);

        if (basket == null) return new CheckoutBasketResult(false, null, "Basket not found");

        var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutEvent>();

        eventMessage.Items = command.BasketCheckout.Items.Select(item => item.Adapt<ShoppingCartItem>()).ToList();

        eventMessage.TotalPrice = basket.TotalPrice;

        var response = await client.GetResponse<BasketCheckoutResponse>(eventMessage, cancellationToken, TimeSpan.FromMinutes(2));

        if (response.Message.IsSuccess == false)
        {
            return new CheckoutBasketResult(false, null, response.Message.ErrorMessage);
        }

        await repository.Delete(command.BasketCheckout.UserName, cancellationToken);

        return new CheckoutBasketResult(true, response.Message.OrderId);
    }
}
