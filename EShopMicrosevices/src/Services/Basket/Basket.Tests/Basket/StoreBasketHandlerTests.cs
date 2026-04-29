using BasketAPI.Basket.StoreBasket;
using BasketAPI.Data.Repository.IRepository;
using BasketAPI.Models;
using Discount.Grpc;
using FluentAssertions;
using Grpc.Core;
using Moq;
using static Discount.Grpc.DiscountProtoService;

namespace Basket.Tests.Basket;

public class StoreBasketHandlerTests
{
    private readonly Mock<IBasketRepository> _repository;
    private readonly Mock<DiscountProtoServiceClient> _discountClient;
    private readonly StoreBasketCommandHandler _handler;

    public StoreBasketHandlerTests()
    {
        _repository = new Mock<IBasketRepository>();
        _discountClient = new Mock<DiscountProtoServiceClient>();
        _handler = new StoreBasketCommandHandler(_repository.Object, _discountClient.Object);

        _repository.Setup(r => r.Store(It.IsAny<ShoppingCart>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync((ShoppingCart cart, CancellationToken _) => cart);
        _repository.Setup(r => r.Commit(It.IsAny<CancellationToken>()))
                   .Returns(Task.CompletedTask);

        // Return an empty coupon (no discount) by default
        _discountClient.Setup(c => c.GetDiscountAsync(
                It.IsAny<GetDiscountRequest>(),
                It.IsAny<Metadata>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .Returns(CreateUnaryCall(new CouponModel { ProductName = string.Empty, Amount = 0 }));
    }

    [Fact]
    public async Task Handle_ValidCart_StoresAndReturnsUserName()
    {
        var cart = new ShoppingCart("alice")
        {
            Items = [new ShoppingCartItem { ProductId = Guid.NewGuid(), ProductName = "Book", Price = 15m, Quantity = 1 }]
        };
        var command = new StoreBasketCommand(cart);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.UserName.Should().Be("alice");
        _repository.Verify(r => r.Store(It.IsAny<ShoppingCart>(), It.IsAny<CancellationToken>()), Times.Once);
        _repository.Verify(r => r.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CartWithDiscount_AppliesAmountDiscount()
    {
        var productId = Guid.NewGuid();
        var cart = new ShoppingCart("bob")
        {
            Items = [new ShoppingCartItem { ProductId = productId, ProductName = "Gadget", Price = 100m, Quantity = 1 }]
        };

        _discountClient.Setup(c => c.GetDiscountAsync(
                It.IsAny<GetDiscountRequest>(),
                It.IsAny<Metadata>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .Returns(CreateUnaryCall(new CouponModel { ProductName = "Gadget", Amount = 10, Over = 0 }));

        var command = new StoreBasketCommand(cart);

        await _handler.Handle(command, CancellationToken.None);

        cart.Items[0].Price.Should().Be(90m);
    }

    [Fact]
    public async Task Handle_CartWithTieredDiscount_AppliesOverAmountWhenQuantityMet()
    {
        var cart = new ShoppingCart("carol")
        {
            Items = [new ShoppingCartItem { ProductId = Guid.NewGuid(), ProductName = "Chair", Price = 200m, Quantity = 3 }]
        };

        _discountClient.Setup(c => c.GetDiscountAsync(
                It.IsAny<GetDiscountRequest>(),
                It.IsAny<Metadata>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .Returns(CreateUnaryCall(new CouponModel { ProductName = "Chair", Amount = 5, Over = 2, OverAmount = 30 }));

        await _handler.Handle(new StoreBasketCommand(cart), CancellationToken.None);

        // quantity (3) >= Over (2), so OverAmount discount (30) is applied
        cart.Items[0].Price.Should().Be(170m);
    }

    private static AsyncUnaryCall<T> CreateUnaryCall<T>(T response)
        => new(Task.FromResult(response), Task.FromResult(new Metadata()),
               () => Status.DefaultSuccess, () => new Metadata(), () => { });
}
