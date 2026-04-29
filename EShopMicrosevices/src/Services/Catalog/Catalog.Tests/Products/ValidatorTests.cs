using CatalogAPI.Products.CreateProduct;
using CatalogAPI.Products.DeleteProduct;
using CatalogAPI.Products.GetProductById;
using CatalogAPI.Products.UpdateProduct;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace Catalog.Tests.Products;

public class CreateProductValidatorTests
{
    private readonly CreateProductCommandValidator _validator = new();

    [Fact]
    public void Validate_ValidCommand_NoErrors()
    {
        var command = new CreateProductCommand("Name", ["Cat"], "Desc", "img.jpg", 9.99m);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_EmptyName_HasError(string? name)
    {
        var command = new CreateProductCommand(name!, ["Cat"], "Desc", "img.jpg", 9.99m);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Validate_ZeroPrice_HasError()
    {
        var command = new CreateProductCommand("Name", ["Cat"], "Desc", "img.jpg", 0m);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Price);
    }
}

public class GetProductByIdValidatorTests
{
    private readonly GetProductByIdQueryValidator _validator = new();

    [Fact]
    public void Validate_ValidId_NoErrors()
    {
        var query = new GetProductByIdQuery(Guid.NewGuid());
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_EmptyId_HasError()
    {
        var query = new GetProductByIdQuery(Guid.Empty);
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(q => q.Id);
    }
}

public class UpdateProductValidatorTests
{
    private readonly UpdateProductCommandValidator _validator = new();

    [Fact]
    public void Validate_ValidCommand_NoErrors()
    {
        var command = new UpdateProductCommand(Guid.NewGuid(), "Name", ["Cat"], "Desc", "img.jpg", 9.99m);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_EmptyId_HasError()
    {
        var command = new UpdateProductCommand(Guid.Empty, "Name", ["Cat"], "Desc", "img.jpg", 9.99m);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}

public class DeleteProductValidatorTests
{
    private readonly DeleteProductRequestValidator _validator = new();

    [Fact]
    public void Validate_ValidRequest_NoErrors()
    {
        var request = new DeleteProductRequest(Guid.NewGuid());
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_EmptyId_HasError()
    {
        var request = new DeleteProductRequest(Guid.Empty);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Id);
    }
}
