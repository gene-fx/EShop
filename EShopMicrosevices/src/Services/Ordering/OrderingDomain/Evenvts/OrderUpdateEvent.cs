namespace OrderingDomain.Evenvts;

public record OrderUpdateEvent(Order order) : IDomainEvent;
