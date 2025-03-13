using BuildingBlocks.Messaging.Events;
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

        var orderItems = new List<OrderItemDto>();

        foreach (var item in basketCheckoutEvent.Items)
        {
            Guid productId = (Guid)item.GetType().GetProperty("ProductId")!.GetValue(item, null)!;
            Guid orderId = (Guid)item.GetType().GetProperty("OrderId")!.GetValue(item, null)!;
            decimal price = (decimal)item.GetType().GetProperty("Price")!.GetValue(item, null)!;
            int quantity = (int)item.GetType().GetProperty("Quantity")!.GetValue(item, null)!;

            orderItems.Add(new OrderItemDto(orderId, productId, quantity, price));
        }

        var orderDto = new OrderDto(
            OrderId: Guid.NewGuid(),
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
