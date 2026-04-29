using FluentAssertions;
using OrderingDomain.Enums;
using OrderingDomain.Evenvts;
using OrderingDomain.Models;
using OrderingDomain.ValueObjects;

namespace Ordering.Tests.Domain;

public class OrderAggregateTests
{
    private static Address TestAddress() =>
        Address.Of("John", "Doe", "john@test.com", "123 Main St", "US", "CA", "90001");

    private static Payment TestPayment() =>
        Payment.Of("John Doe", "4111111111111111", "12/26", "123", 1);

    [Fact]
    public void Create_ValidParameters_CreatesOrderInPendingStatus()
    {
        var orderId = OrderId.Of(Guid.NewGuid());
        var customerId = CustomerId.Of(Guid.NewGuid());

        var order = Order.Create(orderId, customerId, OrderName.Of("Order-001"),
            TestAddress(), TestAddress(), TestPayment());

        order.Should().NotBeNull();
        order.Id.Should().Be(orderId);
        order.Status.Should().Be(OrderStatus.Pending);
        order.OrderItems.Should().BeEmpty();
    }

    [Fact]
    public void Create_ValidParameters_RaisesOrderCreatedEvent()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-002"), TestAddress(), TestAddress(), TestPayment());

        order.DomainEvents.Should().ContainSingle(e => e is OrderCreatedEvent);
    }

    [Fact]
    public void Add_ValidItem_AppendsToOrderItems()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-003"), TestAddress(), TestAddress(), TestPayment());
        var productId = ProductId.Of(Guid.NewGuid());

        order.Add(productId, 2, 49.99m);

        order.OrderItems.Should().HaveCount(1);
        order.OrderItems[0].Quantity.Should().Be(2);
        order.OrderItems[0].Price.Should().Be(49.99m);
    }

    [Fact]
    public void TotalPrice_MultipleItems_CalculatesCorrectly()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-004"), TestAddress(), TestAddress(), TestPayment());

        order.Add(ProductId.Of(Guid.NewGuid()), 2, 10m);
        order.Add(ProductId.Of(Guid.NewGuid()), 3, 5m);

        order.TotalPrice.Should().Be(35m);
    }

    [Fact]
    public void Add_ZeroQuantity_ThrowsArgumentOutOfRangeException()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-005"), TestAddress(), TestAddress(), TestPayment());

        var act = () => order.Add(ProductId.Of(Guid.NewGuid()), 0, 10m);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Add_ZeroPrice_ThrowsArgumentOutOfRangeException()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-006"), TestAddress(), TestAddress(), TestPayment());

        var act = () => order.Add(ProductId.Of(Guid.NewGuid()), 1, 0m);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Remove_ExistingItem_RemovesFromOrderItems()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-007"), TestAddress(), TestAddress(), TestPayment());
        var productId = ProductId.Of(Guid.NewGuid());
        order.Add(productId, 1, 25m);

        order.Remove(productId);

        order.OrderItems.Should().BeEmpty();
    }

    [Fact]
    public void Update_ValidValues_UpdatesFieldsAndRaisesEvent()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-008"), TestAddress(), TestAddress(), TestPayment());
        order.ClearDomainEvents();

        var newAddress = Address.Of("Jane", "Smith", "jane@test.com", "456 Oak Ave", "US", "NY", "10001");
        order.Update(OrderName.Of("Order-008-Updated"), newAddress, newAddress, TestPayment(), OrderStatus.Completed);

        order.OrderName.Value.Should().Be("Order-008-Updated");
        order.Status.Should().Be(OrderStatus.Completed);
        order.DomainEvents.Should().ContainSingle(e => e is OrderUpdateEvent);
    }

    [Fact]
    public void ClearDomainEvents_RemovesAllEvents()
    {
        var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(Guid.NewGuid()),
            OrderName.Of("Order-009"), TestAddress(), TestAddress(), TestPayment());

        var cleared = order.ClearDomainEvents();

        cleared.Should().HaveCount(1);
        order.DomainEvents.Should().BeEmpty();
    }
}
