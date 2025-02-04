namespace BasketAPI.Data.Repository;

public class CachedBasketRepository
    (IBasketRepository basketRepository,
    IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> Get(string userName)
    {
        if (await cache.GetAsync(userName) is not null)
        {
            return JsonSerializer.Deserialize<ShoppingCart>(await cache.GetAsync(userName))!;
        }

        var basket = await basketRepository.Get(userName);

        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket));

        return basket;
    }

    public async Task<ShoppingCart> Store(ShoppingCart cart, CancellationToken cancellationToken)
    {
        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);

        return await basketRepository.Store(cart, cancellationToken);
    }

    public async Task<bool> Delete(string userName, CancellationToken cancellationToken)
    {
        if (await cache.GetAsync(userName) is not null)
        {
            await cache.RemoveAsync(userName);
        }

        return await basketRepository.Delete(userName, cancellationToken);
    }

    public Task Commit(CancellationToken cancellationToken)
    {
        return basketRepository.Commit(cancellationToken);
    }
}
