using BasketAPI.Data.Repository;
using BasketAPI.Data.Repository.IRepository;
using BasketAPI.Models;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using System.Text.Json;

namespace Basket.Tests.Data;

public class CachedBasketRepositoryTests
{
    private readonly Mock<IBasketRepository> _innerRepository;
    private readonly Mock<IDistributedCache> _cache;
    private readonly CachedBasketRepository _repository;

    public CachedBasketRepositoryTests()
    {
        _innerRepository = new Mock<IBasketRepository>();
        _cache = new Mock<IDistributedCache>();
        _repository = new CachedBasketRepository(_innerRepository.Object, _cache.Object);
    }

    [Fact]
    public async Task Get_CacheHit_ReturnsCachedBasket()
    {
        var cart = new ShoppingCart("user1")
        {
            Items = [new ShoppingCartItem { ProductName = "Widget", Price = 5m, Quantity = 1 }]
        };
        var json = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cart));

        _cache.Setup(c => c.GetAsync("user1", It.IsAny<CancellationToken>()))
              .ReturnsAsync(json);

        var result = await _repository.Get("user1", CancellationToken.None);

        result.Should().NotBeNull();
        result!.UserName.Should().Be("user1");
        _innerRepository.Verify(r => r.Get(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Get_CacheMiss_FetchesFromInnerAndPopulatesCache()
    {
        var cart = new ShoppingCart("user2");
        _cache.Setup(c => c.GetAsync("user2", It.IsAny<CancellationToken>()))
              .ReturnsAsync((byte[]?)null);
        _innerRepository.Setup(r => r.Get("user2", It.IsAny<CancellationToken>()))
                        .ReturnsAsync(cart);

        var result = await _repository.Get("user2", CancellationToken.None);

        result.Should().NotBeNull();
        _innerRepository.Verify(r => r.Get("user2", It.IsAny<CancellationToken>()), Times.Once);
        _cache.Verify(c => c.SetAsync("user2", It.IsAny<byte[]>(),
            It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Store_UpdatesCacheAndDelegatesToInner()
    {
        var cart = new ShoppingCart("user3");
        _innerRepository.Setup(r => r.Store(cart, It.IsAny<CancellationToken>())).ReturnsAsync(cart);

        var result = await _repository.Store(cart, CancellationToken.None);

        result.Should().Be(cart);
        _cache.Verify(c => c.SetAsync("user3", It.IsAny<byte[]>(),
            It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        _innerRepository.Verify(r => r.Store(cart, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_RemovesFromCacheAndDelegatesToInner()
    {
        var json = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new ShoppingCart("user4")));
        _cache.Setup(c => c.GetAsync("user4", It.IsAny<CancellationToken>())).ReturnsAsync(json);
        _innerRepository.Setup(r => r.Delete("user4", It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _repository.Delete("user4", CancellationToken.None);

        result.Should().BeTrue();
        _cache.Verify(c => c.RemoveAsync("user4", It.IsAny<CancellationToken>()), Times.Once);
        _innerRepository.Verify(r => r.Delete("user4", It.IsAny<CancellationToken>()), Times.Once);
    }
}
