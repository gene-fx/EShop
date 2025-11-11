using Refit;
using ShoppingWeb.Models.Basket;

namespace ShoppingWeb.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasket(string userName, CancellationToken cancellationToken = default);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest req, CancellationToken cancellationToken = default);

    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasket(string userName, CancellationToken cancellationToken = default);

    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest req, CancellationToken cancellationToken = default);
}
