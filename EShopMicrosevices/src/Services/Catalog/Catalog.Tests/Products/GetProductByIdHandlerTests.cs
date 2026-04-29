using CatalogAPI.Exceptions;
using CatalogAPI.Models;
using CatalogAPI.Products.GetProductById;
using FluentAssertions;
using Marten;
using Moq;

namespace Catalog.Tests.Products;

public class GetProductByIdHandlerTests
{
    private readonly Mock<IDocumentSession> _session;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdHandlerTests()
    {
        _session = new Mock<IDocumentSession>();
        _handler = new GetProductByIdQueryHandler(_session.Object);
    }

    [Fact]
    public async Task Handle_ExistingProduct_ReturnsProduct()
    {
        var productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Name = "Test Product",
            Category = ["Electronics"],
            Description = "Description",
            ImageFile = "img.jpg",
            Price = 49.99m
        };
        _session.Setup(s => s.LoadAsync<Product>(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

        var result = await _handler.Handle(new GetProductByIdQuery(productId), CancellationToken.None);

        result.Should().NotBeNull();
        result.Product.Id.Should().Be(productId);
        result.Product.Name.Should().Be("Test Product");
    }

    [Fact]
    public async Task Handle_NonExistentProduct_ThrowsProductNotFoundException()
    {
        var productId = Guid.NewGuid();
        _session.Setup(s => s.LoadAsync<Product>(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

        var act = () => _handler.Handle(new GetProductByIdQuery(productId), CancellationToken.None);

        await act.Should().ThrowAsync<ProductNotFoundException>();
    }
}
