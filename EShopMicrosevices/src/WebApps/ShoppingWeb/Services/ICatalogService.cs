using Refit;
using ShoppingWeb.Models.Catalog;

namespace ShoppingWeb.Services;

public interface ICatalogService
{
    [Get("/catalog-service/products?pageNumber={pageNumber }&pageSize={pageSize}")]
    Task<GetProductResponse> GetProducts(int? pageNumber = 1, int? pageSize = 10, CancellationToken cancellationToken = default);

    [Get("/catalog-service/products/{id}")]
    Task<GetProductByIdResponse> GetProductById(Guid id, CancellationToken cancellationToken = default);

    [Get("/catalog-service/products/{category}")]
    Task<GetProductCategorydResponse> GetProductCategoryd(string category, CancellationToken cancellationToken = default);
}
