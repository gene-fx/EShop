using CatalogAPI.Exceptions;
using CatalogAPI.Models;
using CatalogAPI.Products.UpdateProduct;
using FluentAssertions;
using Marten;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Catalog.Tests.Products;

public class UpdateProductHandlerTests
{
    private readonly Mock<IDocumentSession> _session;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductHandlerTests()
    {
        _session = new Mock<IDocumentSession>();
        _session.Setup(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        _handler = new UpdateProductCommandHandler(_session.Object,
            NullLogger<UpdateProductCommandHandler>.Instance);
    }

    [Fact]
    public async Task Handle_ExistingProduct_UpdatesAndReturnsSuccess()
    {
        var productId = Guid.NewGuid();
        var existing = new Product
        {
            Id = productId,
            Name = "Old Name",
            Category = ["Old Cat"],
            Description = "Old Desc",
            ImageFile = "old.jpg",
            Price = 10m
        };
        _session.Setup(s => s.LoadAsync<Product>(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existing);

        var command = new UpdateProductCommand(productId, "New Name", ["New Cat"], "New Desc", "new.jpg", 20m);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        existing.Name.Should().Be("New Name");
        existing.Price.Should().Be(20m);
        _session.Verify(s => s.Update(It.IsAny<Product>()), Times.Once);
        _session.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistentProduct_ThrowsProductNotFoundException()
    {
        var productId = Guid.NewGuid();
        _session.Setup(s => s.LoadAsync<Product>(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

        var command = new UpdateProductCommand(productId, "Name", ["Cat"], "Desc", "img.jpg", 10m);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ProductNotFoundException>();
        _session.Verify(s => s.Update(It.IsAny<Product>()), Times.Never);
    }
}
