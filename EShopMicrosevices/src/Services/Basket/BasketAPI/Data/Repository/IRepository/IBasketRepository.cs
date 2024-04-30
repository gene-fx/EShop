namespace BasketAPI.Data.Repository.IRepository
{
    public interface IBasketRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart> Store(ShoppingCart cart);
    }
}
