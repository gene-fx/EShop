using OrderingApplication.Orders.Queries.GetOrdersByCustomer;


namespace OrderingAPI.Endpoints;

//public record GetOrdersByCustomerNameResquest(Guid Customer);

public record GetOrdersByCustomerResponse(IReadOnlyCollection<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var query = new GetOrdersByCustomerQuery(customerId);

            var result = await sender.Send(query);

            return Results.Ok(result.Adapt<GetOrdersByCustomerResponse>());
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders By Customer ID");
    }
}
