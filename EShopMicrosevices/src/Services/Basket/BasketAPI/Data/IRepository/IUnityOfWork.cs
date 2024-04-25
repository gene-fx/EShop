namespace BasketAPI.Data.IRepository
{
    public interface IUnityOfWork
    {
        Task Commit(CancellationToken cancellationToken);
    }
}
