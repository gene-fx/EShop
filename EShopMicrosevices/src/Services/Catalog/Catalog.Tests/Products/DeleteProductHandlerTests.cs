using CatalogAPI.Exceptions;
using CatalogAPI.Models;
using CatalogAPI.Products.DeleteProduct;
using FluentAssertions;
using Marten;
using Moq;

namespace Catalog.Tests.Products;

public class DeleteProductHandlerTests
{
    private readonly Mock<IDocumentSession> _session;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductHandlerTests()
    {
        _session = new Mock<IDocumentSession>();
        _session.Setup(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        _handler = new DeleteProductCommandHandler(_session.Object);
    }

    [Fact]
    public async Task Handle_ExistingProduct_DeletesAndReturnsSuccess()
    {
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Product" };
        _session.Setup(s => s.LoadAsync<Product>(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

        var result = await _handler.Handle(new DeleteProductRequest(productId), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _session.Verify(s => s.Delete(It.Is<Product>(p => p.Id == productId)), Times.Once);
        _session.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentProduct_ThrowsProductNotFoundException()
    {
        var productId = Guid.NewGuid();
        _session.Setup(s => s.LoadAsync<Product>(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

        var act = () => _handler.Handle(new DeleteProductRequest(productId), CancellationToken.None);

        await act.Should().ThrowAsync<ProductNotFoundException>();
        _session.Verify(s => s.Delete(It.IsAny<Product>()), Times.Never);
    }
}
