namespace BasketAPI.Data.IRepository
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Add(T entity, CancellationToken cancellationToken);

        public void Delete(T entity);

        public Task<T> Get(Expression<Func<T, bool>>? filter = null, bool? asNoTrack = true);

        public Task<T> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}