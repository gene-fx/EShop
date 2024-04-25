namespace BasketAPI.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Task<T> Add(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(Expression<Func<T, bool>>? filter = null, bool? asNoTrack = true)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
