namespace BasketAPI.Data.Repository.IRepository;

public interface IBasketRepository
{
    Task<ShoppingCart> Store(ShoppingCart cart, CancellationToken cancellationToken);

    Task<ShoppingCart> Get(string userName, CancellationToken? cancellationToken);

    Task<bool> Delete(string userName, CancellationToken cancellationToken);

    Task Commit(CancellationToken cancellationToken);
}
