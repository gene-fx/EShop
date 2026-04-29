using Microsoft.EntityFrameworkCore;
using OrderingApplication.Data;
using OrderingApplication.Exceptions;
using OrderingApplication.Orders.Commands.DeleteOrder;
using OrderingDomain.Models;
using OrderingDomain.ValueObjects;

namespace Ordering.Tests.Orders;

public class DeleteOrderHandlerTests
{
    private static Order CreateTestOrder(Guid orderId)
    {
        var addr = Address.Of("John", "Doe", "j@t.com", "123 Main", "US", "CA", "90001");
        var pay = Payment.Of("John Doe", "4111111111111111", "12/26", "123", 1);
        var order = Order.Create(OrderId.Of(orderId), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("ORD-D01"), addr, addr, pay);
        order.Add(ProductId.Of(Guid.NewGuid()), 1, 50m);
        return order;
    }

    [Fact]
    public async Task Handle_ExistingOrder_DeletesAndReturnsSuccess()
    {
        var orderId = Guid.NewGuid();
        var existingOrder = CreateTestOrder(orderId);

        var mockSet = new Mock<DbSet<Order>>();
        mockSet.Setup(s => s.FindAsync(It.IsAny<object?[]?>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(existingOrder);
        mockSet.Setup(s => s.Remove(existingOrder));

        var ctx = new Mock<IApplicationDbContext>();
        ctx.Setup(c => c.Orders).Returns(mockSet.Object);
        ctx.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeleteOrderCommandHandler(ctx.Object);

        var result = await handler.Handle(new DeleteOrderCommand(orderId), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        mockSet.Verify(s => s.Remove(existingOrder), Times.Once);
        ctx.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentOrder_ThrowsOrderNotFoundException()
    {
        var mockSet = new Mock<DbSet<Order>>();
        mockSet.Setup(s => s.FindAsync(It.IsAny<object?[]?>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Order?)null);

        var ctx = new Mock<IApplicationDbContext>();
        ctx.Setup(c => c.Orders).Returns(mockSet.Object);

        var handler = new DeleteOrderCommandHandler(ctx.Object);

        var act = () => handler.Handle(new DeleteOrderCommand(Guid.NewGuid()), CancellationToken.None);

        await act.Should().ThrowAsync<OrderNotFoundException>();
        mockSet.Verify(s => s.Remove(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ExistingOrder_DoesNotSaveWhenRemoveFails()
    {
        var orderId = Guid.NewGuid();
        var existingOrder = CreateTestOrder(orderId);

        var mockSet = new Mock<DbSet<Order>>();
        mockSet.Setup(s => s.FindAsync(It.IsAny<object?[]?>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(existingOrder);
        mockSet.Setup(s => s.Remove(existingOrder)).Throws(new InvalidOperationException("Remove failed"));

        var ctx = new Mock<IApplicationDbContext>();
        ctx.Setup(c => c.Orders).Returns(mockSet.Object);

        var handler = new DeleteOrderCommandHandler(ctx.Object);

        var act = () => handler.Handle(new DeleteOrderCommand(orderId), CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>();
        ctx.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
