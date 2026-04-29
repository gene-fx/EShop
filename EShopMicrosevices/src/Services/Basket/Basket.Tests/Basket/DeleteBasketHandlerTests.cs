using BasketAPI.Basket.DeleteBasket;
using BasketAPI.Data.Repository.IRepository;
using BasketAPI.Models;
using BuildingBlocks.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Basket.Tests.Basket;

public class DeleteBasketHandlerTests
{
    private readonly Mock<IBasketRepository> _repository;
    private readonly Mock<IServiceScopeFactory> _scopeFactory;
    private readonly DeleteBasketCommandHandler _handler;

    public DeleteBasketHandlerTests()
    {
        _repository = new Mock<IBasketRepository>();

        var mockProvider = new Mock<IServiceProvider>();
        mockProvider.Setup(p => p.GetService(typeof(IBasketRepository))).Returns(_repository.Object);

        var mockScope = new Mock<IServiceScope>();
        mockScope.Setup(s => s.ServiceProvider).Returns(mockProvider.Object);

        _scopeFactory = new Mock<IServiceScopeFactory>();
        _scopeFactory.Setup(f => f.CreateScope()).Returns(mockScope.Object);

        _handler = new DeleteBasketCommandHandler(_scopeFactory.Object);
    }

    [Fact]
    public async Task Handle_ExistingBasket_DeletesAndReturnsSuccess()
    {
        var cart = new ShoppingCart("user1");
        _repository.Setup(r => r.Get("user1", It.IsAny<CancellationToken>())).ReturnsAsync(cart);
        _repository.Setup(r => r.Delete("user1", It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _repository.Setup(r => r.Commit(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _handler.Handle(new DeleteBasketCommand("user1"), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _repository.Verify(r => r.Delete("user1", It.IsAny<CancellationToken>()), Times.Once);
        _repository.Verify(r => r.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentBasket_ThrowsBasketNotFoundException()
    {
        _repository.Setup(r => r.Get("ghost", It.IsAny<CancellationToken>())).ReturnsAsync((ShoppingCart?)null);

        var act = () => _handler.Handle(new DeleteBasketCommand("ghost"), CancellationToken.None);

        await act.Should().ThrowAsync<BasketNotFoundException>();
        _repository.Verify(r => r.Delete(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
