namespace CatalogAPI.Products.GetProductsByName
{
    public record GetProductByNameRequest(string? Name, int? PageNumber = 1, int? PageSize = 10, bool? AsIdList = false);

    public record GetProductByNameResponse(IEnumerable<Product> Products);

    public class GetProductByNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{name}", 
                async (ISender sender,
                string name,
                [AsParameters] GetProductByNameRequest request) =>
                {
                    request = new GetProductByNameRequest(name, request.PageNumber, request.PageSize, request.AsIdList);

                    var result = await sender.Send(request.Adapt<GetProductByNameQuery>());

                    GetProductByNameResponse response = result.Adapt<GetProductByNameResponse>();

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
                .WithName("GetProductByName")
                .Produces<GetProductByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Name")
                .WithDescription("Get Product By Name");
        }
    }
}
