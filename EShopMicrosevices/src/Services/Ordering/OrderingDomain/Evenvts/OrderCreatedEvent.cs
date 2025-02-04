namespace OrderingDomain.Evenvts;

public record OrderCreatedEvent(Order order) : IDomainEvent;
