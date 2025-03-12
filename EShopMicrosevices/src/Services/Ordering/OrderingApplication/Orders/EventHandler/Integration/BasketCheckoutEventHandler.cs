using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;
using OrderingApplication.Orders.Commands.CreateOrder;

namespace OrderingApplication.Orders.EventHandler.Integration;
public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("BasketCheckoutIntegrationEvent consumed: {BasketCheckoutEvent}", context.Message.GetType().Name);

        CreateOrderCommand createOrderCommand = MapToOrderCommand(context.Message);

        await sender.Send(createOrderCommand);

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

        foreach (var item in basketCheckoutEvent.Items)
        {
            OrderItemDto orderItem = item.Adapt<OrderItemDto>();
            orderItems.Add(orderItem);
        }

        var orderDto = new OrderDto(
            OrderId: orderId,
            CustomerId: basketCheckoutEvent.CustomerId,
            OrderName: basketCheckoutEvent.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: OrderingDomain.Enums.OrderStatus.Pending,
            OrderItems: orderItems.AsReadOnly()
            );

        return new CreateOrderCommand(orderDto);
    }
}
