namespace OrderingApplication.Orders.EventHandler.Domain;
public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {^DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
