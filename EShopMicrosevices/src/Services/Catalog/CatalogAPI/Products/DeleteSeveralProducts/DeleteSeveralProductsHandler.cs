namespace CatalogAPI.Products.DeleteSeveralProducts;

public record DeleteSeveralProductsCommand(List<Guid> Ids) : ICommand<DeleteSeveralProductsResult>;

public record DeleteSeveralProductsResult(bool IsSucces);

public class DeleteSeveralProductsValitador : AbstractValidator<DeleteSeveralProductsCommand>
{
    public DeleteSeveralProductsValitador()
    {
        RuleFor(model => model.Ids).NotEmpty().WithMessage("Ids should not be empty");
    }
}

internal class DeleteSeveralProductsCommandHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteSeveralProductsCommand, DeleteSeveralProductsResult>
{
    public async Task<DeleteSeveralProductsResult> Handle(DeleteSeveralProductsCommand command, CancellationToken cancellationToken)
    {
        var products = new List<Product>();

        foreach (Guid id in command.Ids)
        {
            var product = await session.LoadAsync<Product>(id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(command.Ids, id);
            }

            products.Add(product);
        }

        session.DeleteObjects(products);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteSeveralProductsResult(true);
    }
}