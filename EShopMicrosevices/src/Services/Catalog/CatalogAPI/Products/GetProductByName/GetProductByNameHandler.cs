using CatalogAPI.Products.GetProductById;

namespace CatalogAPI.Products.GetProductByName
{
    public record GetProductByNameQuery(string Name) : IQuery<GetProductByNameResult>;

    public record GetProductByNameResult(IEnumerable<Product> Products);

    public class GetProductByNameQueryValidator : AbstractValidator<GetProductByNameQuery>
    {
        public GetProductByNameQueryValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required");
        }
    }

    internal class GetProductByNameQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByNameQuery, GetProductByNameResult>
    {
        public async Task<GetProductByNameResult> Handle(GetProductByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>()
                .Where(_ => _.Name.ToLower() == query.Name.ToString().ToLower()).ToListAsync();

            if(result.Count <= 0) 
            {
                throw new ProductNotFoundException($"No product was found with the name {query.Name}");
            }

            return new GetProductByNameResult(result);
        }
    }
}
