using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace OrderingApplication.Orders.EventHandler.Integration;
public class BasketCheckoutEventHandler
    : IConsumer<BasketCheckoutEvent>
{
    public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {


        throw new NotImplementedException();
    }
}
