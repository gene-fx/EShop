namespace BuildingBlocks.Messaging.Events.Responses;
public record BasketCheckoutResponse
{
    public Guid Id { get; set; }

    public DateTime OccurredOn => DateTime.UtcNow;

    public string EventType => GetType().AssemblyQualifiedName!;

    public bool IsSuccess { get; set; }

    public Guid OrderId { get; set; }

    public string? ErrorMessage { get; set; }
}
