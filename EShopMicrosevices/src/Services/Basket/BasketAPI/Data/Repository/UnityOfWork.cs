using BasketAPI.Data.Repository.IRepository;

namespace BasketAPI.Data.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly IDocumentSession _session;

        public UnityOfWork(IDocumentSession session)
        {
            _session = session;
            BasketRepository = new BasketRepository(session);
        }

        public IBasketRepository BasketRepository {get;set;}

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _session.SaveChangesAsync(cancellationToken);
        }
    }
}