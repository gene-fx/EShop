
namespace BasketAPI.Data.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        public Task Commit(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
