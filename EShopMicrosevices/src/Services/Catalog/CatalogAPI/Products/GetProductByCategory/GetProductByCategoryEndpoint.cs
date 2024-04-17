
using CatalogAPI.Products.GetProductByName;
using Mapster;

namespace CatalogAPI.Products.GetProductByCategory
{
    //public record GetProductByCategoryRequest();

    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{category}", async (ISender sender, string category) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));

                GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();

                if(response is null || response.Products.IsEmpty())
                {
                    throw new ProductNotFoundException();
                }

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
        }
    }
}
