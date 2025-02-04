namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id)
    : ICommand<DeleteProductResponse>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(model => model.Id).NotEmpty().WithMessage("Id is required");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductRequest, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductRequest command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        session.Delete<Product>(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResponse(true);
    }
}