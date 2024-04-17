﻿namespace CatalogAPI.Products.GetProductByName
{
    public record GetProductByNameQuery(string Name) : IQuery<GetProductByNameResult>;

    public record GetProductByNameResult(IEnumerable<Product> Products);

    internal class GetProductByNameQueryHandler(IDocumentSession session, ILogger<GetProductByNameQueryHandler> logger)
        : IQueryHandler<GetProductByNameQuery, GetProductByNameResult>
    {
        public async Task<GetProductByNameResult> Handle(GetProductByNameQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetProductByNameQueryHandler.Handle called with {query}");

            var result = await session.Query<Product>()
                .Where(_ => _.Name.ToLower() == query.Name.ToString().ToLower()).ToListAsync();

            return new GetProductByNameResult(result);
        }
    }
}