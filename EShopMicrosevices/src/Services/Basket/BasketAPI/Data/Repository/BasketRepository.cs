namespace BasketAPI.Data.Repository
{
    public class BasketRepository
        : Repository<ShoppingCart>, IBasketRepository
    {
        private readonly IDocumentSession _session;
            
        public BasketRepository(IDocumentSession session) : base(session)
        {    
            _session = session;
        }

        public async Task<ShoppingCart> Store(ShoppingCart basket)
        {
             _session.Store(basket);
            
            return basket;
        }
    }
}
