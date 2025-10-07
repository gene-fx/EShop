using BuildingBlocks.Messaging.Events;
using MassTransit;
using OrderingApplication.Orders.Commands.CreateOrder;
using System.Text.Json;

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

        Guid orderId = Guid.NewGuid();

        foreach (JsonElement item in basketCheckoutEvent.Items)
        {
            Guid productId = item.GetProperty("productId").GetGuid();
            string productName = item.GetProperty("productName").GetString()!;
            int quantity = item.GetProperty("quantity").GetInt32();
            decimal price = Decimal.Parse(item.GetProperty("price").ToString());
            orderItems.Add(new OrderItemDto(orderId, productId, quantity, price));
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

