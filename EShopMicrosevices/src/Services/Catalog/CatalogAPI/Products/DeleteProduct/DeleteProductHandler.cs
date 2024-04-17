﻿
namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id) 
        : ICommand<DeleteProductResponse>;

    public record DeleteProductResult(bool IsSuccess);

    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductRequest, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductRequest command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"DeleteProductCommandHandler.Handle called with {command}");

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null) 
            {
                throw new ProductNotFoundException();
            }

            session.Delete<Product>(product);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResponse(true);
        }
    }
}