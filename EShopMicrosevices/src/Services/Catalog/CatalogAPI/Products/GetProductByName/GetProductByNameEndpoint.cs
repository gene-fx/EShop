﻿
namespace CatalogAPI.Products.GetProductByName
{
    //public record GetProductByNameRequest();

    public record GetProductByNameResponse(IEnumerable<Product> Products);

    public class GetProductByNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{name}", async (ISender sender, string name) =>
            {
                var result = await sender.Send(new GetProductByNameQuery(name));

                GetProductByNameResponse response = result.Adapt<GetProductByNameResponse>();

                if (response is null || response.Products.IsEmpty())
                {
                    throw new ProductNotFoundException();
                }

                return Results.Ok(response);
            });
        }
    }
}
