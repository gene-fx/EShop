using Microsoft.EntityFrameworkCore;
using OrderingApplication.Data;
using OrderingApplication.Dtos;
using OrderingApplication.Exceptions;
using OrderingApplication.Orders.Commands.UpdateOrder;
using OrderingDomain.Enums;
using OrderingDomain.Models;
using OrderingDomain.ValueObjects;

namespace Ordering.Tests.Orders;

public class UpdateOrderHandlerTests
{
    private static AddressDto Addr() => new("John", "Doe", "j@t.com", "123 Main", "US", "CA", "90001");
    private static PaymentDto Pay() => new("John Doe", "4111111111111111", "12/26", "123", 1);

    private static Order CreateTestOrder(Guid orderId, Guid customerId)
    {
        var addr = Address.Of("John", "Doe", "j@t.com", "123 Main", "US", "CA", "90001");
        var pay = Payment.Of("John Doe", "4111111111111111", "12/26", "123", 1);
        var order = Order.Create(OrderId.Of(orderId), CustomerId.Of(customerId),
            OrderName.Of("ORD-U01"), addr, addr, pay);
        order.Add(ProductId.Of(Guid.NewGuid()), 1, 100m);
        return order;
    }

    [Fact]
    public async Task Handle_ExistingOrder_UpdatesAndReturnsSuccess()
    {
        var orderId = Guid.NewGuid();
        var existingOrder = CreateTestOrder(orderId, Guid.NewGuid());

        var mockSet = new Mock<DbSet<Order>>();
        mockSet.Setup(s => s.FindAsync(It.IsAny<object?[]?>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(existingOrder);
        mockSet.Setup(s => s.Update(It.IsAny<Order>()));

        var ctx = new Mock<IApplicationDbContext>();
        ctx.Setup(c => c.Orders).Returns(mockSet.Object);
        ctx.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new UpdateOrderHandler(ctx.Object);
        var dto = new OrderDto(orderId, existingOrder.CustomerId.Value, "ORD-U01-UPD",
            Addr(), Addr(), Pay(), OrderStatus.Completed,
            [new(Guid.Empty, Guid.NewGuid(), 1, 100m)]);

        var result = await handler.Handle(new UpdateOrderCommand(dto), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        mockSet.Verify(s => s.Update(existingOrder), Times.Once);
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

        var handler = new UpdateOrderHandler(ctx.Object);
        var dto = new OrderDto(Guid.NewGuid(), Guid.NewGuid(), "ORD-GHOST",
            Addr(), Addr(), Pay(), OrderStatus.Pending,
            [new(Guid.Empty, Guid.NewGuid(), 1, 10m)]);

        var act = () => handler.Handle(new UpdateOrderCommand(dto), CancellationToken.None);

        await act.Should().ThrowAsync<OrderNotFoundException>();
        mockSet.Verify(s => s.Update(It.IsAny<Order>()), Times.Never);
    }
}
