using MediatR;
using Microsoft.Extensions.Logging;
using OrderingDomain.Evenvts;

namespace OrderingApplication.Orders.EventHandler;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {^DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
