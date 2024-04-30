namespace BasketAPI.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public void Add(T entity);

        public void Delete(T entity);

        public Task<T> Get(Expression<Func<T, bool>> filter);

        public Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}