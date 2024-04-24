namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductByCategoryQuery
        (string? Category, int? PageNumber = 1, int? PageSize = 10, bool? AsIdList = false) 
        : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryQueryValidator : AbstractValidator<GetProductByCategoryQuery>
    {
        public GetProductByCategoryQueryValidator()
        {
            RuleFor(model => model.Category).NotEmpty().WithMessage("Category is required");
        }
    }

    internal class GetProductByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var response = await session.Query<Product>()
                .Where(_ => _.Category.Contains(query.Category!)).ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10);

            return new GetProductByCategoryResult(response);
        }
    }
}
