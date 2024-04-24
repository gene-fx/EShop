namespace CatalogAPI.Products.GetProductsByName
{
    public record GetProductByNameQuery
        (string? Name, int? PageNumber = 1, int? PageSize = 10, bool? AsIdList = false) 
        : IQuery<GetProductByNameResult>;

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
                .Where(_ => _.Name.ToLower() == query.Name.ToString().ToLower()).ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            if(!result.Any()) 
            {
                throw new ProductNotFoundException($"No product was found under the name {query.Name}");
            }

            return new GetProductByNameResult(result);
        }
    }
}
