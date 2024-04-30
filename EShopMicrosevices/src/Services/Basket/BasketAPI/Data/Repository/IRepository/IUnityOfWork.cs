namespace BasketAPI.Data.Repository.IRepository
{
    public interface IUnityOfWork
    {
        IBasketRepository BasketRepository { get; }

        Task Commit(CancellationToken cancellationToken);
    }
}