using CatalogAPI.Models;
using CatalogAPI.Products.CreateProduct;
using FluentAssertions;
using Marten;
using Moq;

namespace Catalog.Tests.Products;

public class CreateProductHandlerTests
{
    private readonly Mock<IDocumentSession> _session;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _session = new Mock<IDocumentSession>();
        _session.Setup(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        _handler = new CreateProductHandler(_session.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_StoresProductAndReturnsResult()
    {
        var command = new CreateProductCommand(
            "Test Product", ["Electronics"], "A test product", "test.jpg", 29.99m);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        _session.Verify(s => s.Store(It.Is<Product>(p =>
            p.Name == "Test Product" &&
            p.Category.Contains("Electronics") &&
            p.Price == 29.99m)), Times.Once);
        _session.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ValidCommand_MapsAllFieldsCorrectly()
    {
        var command = new CreateProductCommand(
            "Laptop", ["Electronics", "Computers"], "A laptop", "laptop.jpg", 999.00m);

        Product? storedProduct = null;
        _session.Setup(s => s.Store(It.IsAny<Product[]>()))
                .Callback<Product[]>(p => storedProduct = p.FirstOrDefault());

        await _handler.Handle(command, CancellationToken.None);

        storedProduct.Should().NotBeNull();
        storedProduct!.Name.Should().Be("Laptop");
        storedProduct.Category.Should().Contain("Electronics");
        storedProduct.Category.Should().Contain("Computers");
        storedProduct.Description.Should().Be("A laptop");
        storedProduct.ImageFile.Should().Be("laptop.jpg");
        storedProduct.Price.Should().Be(999.00m);
    }
}
