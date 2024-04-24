using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Products.DeleteSeveralProducts
{
    public record DeleteSeveralProductsRequest(List<Guid> Ids);

    public record DeleteSeveralProductsResponse(bool IsSucces);

    public class DeleteSeveralProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products",
                async ([FromBody] DeleteSeveralProductsRequest request, [FromServices] ISender sender) =>
            {
                var command = request.Adapt<DeleteSeveralProductsCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteSeveralProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteSeveralProducts")
            .Produces<DeleteSeveralProductsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete several products")
            .WithDescription("Delete several products");
        }
    }
}