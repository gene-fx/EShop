namespace OrderingApplication.Orders.Commands.DeleteOrder;
public class DeleteOrderCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FindAsync([OrderId.Of(command.Id)], cancellationToken);

        if (order is null) throw new OrderNotFoundException(command.Id);

        dbContext.Orders.Remove(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }

}
