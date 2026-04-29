using BasketAPI.Basket.GetBasket;
using BasketAPI.Data.Repository.IRepository;
using BasketAPI.Models;
using BuildingBlocks.Exceptions;
using FluentAssertions;
using Moq;

namespace Basket.Tests.Basket;

public class GetBasketHandlerTests
{
    private readonly Mock<IBasketRepository> _repository;
    private readonly GetBasketQueryHandler _handler;

    public GetBasketHandlerTests()
    {
        _repository = new Mock<IBasketRepository>();
        _handler = new GetBasketQueryHandler(_repository.Object);
    }

    [Fact]
    public async Task Handle_ExistingBasket_ReturnsCart()
    {
        var cart = new ShoppingCart("john_doe")
        {
            Items =
            [
                new ShoppingCartItem { ProductId = Guid.NewGuid(), ProductName = "Widget", Price = 9.99m, Quantity = 2 }
            ]
        };
        _repository.Setup(r => r.Get("john_doe", It.IsAny<CancellationToken>()))
                   .ReturnsAsync(cart);

        var result = await _handler.Handle(new GetBasketQuery("john_doe"), CancellationToken.None);

        result.Should().NotBeNull();
        result.Cart.UserName.Should().Be("john_doe");
        result.Cart.Items.Should().HaveCount(1);
        result.Cart.TotalPrice.Should().Be(19.98m);
    }

    [Fact]
    public async Task Handle_NonExistentBasket_ThrowsBasketNotFoundException()
    {
        _repository.Setup(r => r.Get("unknown", It.IsAny<CancellationToken>()))
                   .ReturnsAsync((ShoppingCart?)null);

        var act = () => _handler.Handle(new GetBasketQuery("unknown"), CancellationToken.None);

        await act.Should().ThrowAsync<BasketNotFoundException>();
    }

    [Fact]
    public async Task Handle_EmptyCart_ReturnZeroTotalPrice()
    {
        var cart = new ShoppingCart("jane_doe");
        _repository.Setup(r => r.Get("jane_doe", It.IsAny<CancellationToken>()))
                   .ReturnsAsync(cart);

        var result = await _handler.Handle(new GetBasketQuery("jane_doe"), CancellationToken.None);

        result.Cart.TotalPrice.Should().Be(0m);
        result.Cart.Items.Should().BeEmpty();
    }
}
