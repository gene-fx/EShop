using BuildingBlocks.Exceptions;

namespace OrderingApplication.Exceptions;
public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base("Order", id)
    {
    }
}
