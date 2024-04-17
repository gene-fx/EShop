namespace CatalogAPI.Products.GetProduct
{
    public record GetProductsQuery() : IQuery<GetProductResult>;

    public record GetProductResult(IEnumerable<Product> Products);

    public class GetProductQueryHandler
        (IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetProductsQueryHandler.Handle called with {query}");

            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductResult(products);
        }
    }
}
