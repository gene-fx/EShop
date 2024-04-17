using Marten.Linq.QueryHandlers;
using System.Linq;

namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetProductByNameQueryHandler.Handle called with {query}");

            var response = await session.Query<Product>().Where(_ => _.Category.Contains(query.Category)).ToListAsync();

            return new GetProductByCategoryResult(response);
        }
    }
}
