using Refit;
using ShoppingWeb.Models.Ordering;

namespace ShoppingWeb.Services;

public interface IOrderingService
{
    [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}")]
    Task<GetOrdersResponse> GetOrders(int? pageIndex = 1, int? pageSize = 10, CancellationToken cancellationToken = default);

    [Get("/ordering-service/orders/user/{name}")]
    Task<GetOrdersByNameResponse> GetOPrdersByName(string name, CancellationToken cancellationToken = default);

    [Get("/ordering-service/orders/customer/{customerId}")]
    Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(string customerId, CancellationToken cancellationToken = default);
}
