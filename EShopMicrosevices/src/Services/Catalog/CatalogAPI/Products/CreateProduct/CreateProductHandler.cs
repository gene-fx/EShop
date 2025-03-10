﻿namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(model => model.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(model => model.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(model => model.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(model => model.Price).NotEmpty().WithMessage("Price is required");
    }
}

internal class CreateProductHandler(IDocumentSession _session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        _session.Store(product);
        await _session.SaveChangesAsync();

        return new CreateProductResult(product.Id);
    }
}