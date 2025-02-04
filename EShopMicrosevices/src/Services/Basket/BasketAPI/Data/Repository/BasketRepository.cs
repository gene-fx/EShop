
namespace BasketAPI.Data.Repository;

public class BasketRepository
    : IBasketRepository
{
    private readonly IDocumentSession _session;

    public BasketRepository(IDocumentSession session)
    {
        _session = session;
    }
    public async Task<ShoppingCart?> Get(string userName)
    {
        return await _session.LoadAsync<ShoppingCart>(userName);
    }
    public async Task<ShoppingCart> Store(ShoppingCart basket, CancellationToken cancellationToken)
    {
        _session.Store(basket);

        return basket;
    }

    public async Task<bool> Delete(string userName, CancellationToken cancellationToken)
    {
        _session.Delete<ShoppingCart>(userName);

        return true;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _session.SaveChangesAsync(cancellationToken);
    }
}
