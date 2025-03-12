using BuildingBlocks.Messaging.Events;
using MassTransit;
using OrderingApplication.Orders.Commands.CreateOrder;

namespace OrderingApplication.Orders.EventHandler.Integration;
public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("BasketCheckoutIntegrationEvent consumed: {BasketCheckoutEvent}", context.Message.GetType().Name);



        throw new NotImplementedException();
    }

    private CreateOrderCommand MapToOrderCommand(BasketCheckoutEvent basketCheckoutEvent)
    {
        var addressDto = new AddressDto(basketCheckoutEvent.FirstName,
            basketCheckoutEvent.LastName, basketCheckoutEvent.EmailAddress,
            basketCheckoutEvent.AddressLine, basketCheckoutEvent.Country,
            basketCheckoutEvent.State, basketCheckoutEvent.ZipCode);

        var paymentDto = new PaymentDto(basketCheckoutEvent.CardName,
            basketCheckoutEvent.CardNumber, basketCheckoutEvent.Expiration,
            basketCheckoutEvent.CVV, basketCheckoutEvent.PaymentMethod);

        var orderId = Guid.NewGuid();

        var orderItems = new List<OrderItemDto>();

        var orderDto = new OrderDto(orderId, basketCheckoutEvent.CustomerId,
            basketCheckoutEvent.TotalPrice, addressDto, paymentDto, OrderingDomain.Enums.OrderStatus.Pending,
            []);

        foreach (var item in basketCheckoutEvent.)
        {

        }


        return new CreateOrderCommand();
    }
}
