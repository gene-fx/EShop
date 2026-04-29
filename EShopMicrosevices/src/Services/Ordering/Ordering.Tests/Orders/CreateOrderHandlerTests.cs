using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using OrderingApplication.Data;
using OrderingApplication.Dtos;
using OrderingApplication.Orders.Commands.CreateOrder;
using OrderingDomain.Enums;
using OrderingDomain.Models;
using OrderingInfrastructure.Data;

namespace Ordering.Tests.Orders;

public class CreateOrderHandlerTests
{
    private static AddressDto Addr() => new("John", "Doe", "j@t.com", "123 Main", "US", "CA", "90001");
    private static PaymentDto Pay() => new("John Doe", "4111111111111111", "12/26", "123", 1);

    private static (Mock<IApplicationDbContext> ctx, List<Order> orders) BuildMock()
    {
        var orders = new List<Order>();
        var mockSet = new Mock<DbSet<Order>>();
        mockSet.Setup(s => s.Add(It.IsAny<Order>())).Callback<Order>(o => orders.Add(o));

        var ctx = new Mock<IApplicationDbContext>();
        ctx.Setup(c => c.Orders).Returns(mockSet.Object);
        ctx.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        return (ctx, orders);
    }

    [Fact]
    public async Task Handle_ValidOrder_ReturnsSuccessWithId()
    {
        var (ctx, orders) = BuildMock();
        var handler = new CreateOrderHandler(ctx.Object, NullLogger<CreateOrderHandler>.Instance);

        var dto = new OrderDto(Guid.Empty, Guid.NewGuid(), "ORD-001",
            Addr(), Addr(), Pay(), OrderStatus.Pending,
            [new(Guid.Empty, Guid.NewGuid(), 2, 50m)]);

        var result = await handler.Handle(new CreateOrderCommand(dto), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Id.Should().NotBeNull().And.NotBe(Guid.Empty);
        orders.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_ValidOrder_SavesAllItems()
    {
        var (ctx, orders) = BuildMock();
        var handler = new CreateOrderHandler(ctx.Object, NullLogger<CreateOrderHandler>.Instance);

        var dto = new OrderDto(Guid.Empty, Guid.NewGuid(), "ORD-002",
            Addr(), Addr(), Pay(), OrderStatus.Pending,
            [new(Guid.Empty, Guid.NewGuid(), 2, 25m), new(Guid.Empty, Guid.NewGuid(), 1, 50m)]);

        await handler.Handle(new CreateOrderCommand(dto), CancellationToken.None);

        orders.Should().HaveCount(1);
        orders[0].OrderItems.Should().HaveCount(2);
        orders[0].TotalPrice.Should().Be(100m);
    }

    [Fact]
    public async Task Handle_ValidOrder_CallsSaveChanges()
    {
        var (ctx, _) = BuildMock();
        var handler = new CreateOrderHandler(ctx.Object, NullLogger<CreateOrderHandler>.Instance);

        var dto = new OrderDto(Guid.Empty, Guid.NewGuid(), "ORD-003",
            Addr(), Addr(), Pay(), OrderStatus.Pending,
            [new(Guid.Empty, Guid.NewGuid(), 1, 10m)]);

        await handler.Handle(new CreateOrderCommand(dto), CancellationToken.None);

        ctx.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
