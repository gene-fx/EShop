namespace CatalogAPI.Products.GetProductsByCategory;

public record GetProductByCategoryRequest(string? Category, int? PageNumber = 1, int? PageSize = 10, bool? AsIdList = false);

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (ISender sender,
            string category,
            [AsParameters] GetProductByCategoryRequest request) =>
        {
            request = new GetProductByCategoryRequest(category, request.PageNumber, request.PageSize, request.AsIdList);

            var result = await sender.Send(request.Adapt<GetProductByCategoryQuery>());

            GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();

            if (request.AsIdList ?? false)
            {
                List<Guid> ids = new List<Guid>();

                foreach (Product product in response.Products)
                {
                    ids.Add(product.Id);
                }

                return Results.Ok(ids);
            }

            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}