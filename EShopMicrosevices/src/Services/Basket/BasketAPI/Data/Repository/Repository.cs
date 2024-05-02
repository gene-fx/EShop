namespace BasketAPI.Data.Repository
{
    public class Repository<T>
        (IDocumentSession session)
        : IRepository<T> where T : class
    {
        public void Add(T entity)
        {
            session.Insert<T>(entity);
        }

        public void Delete(T entity)
        {
             session.Delete<T>(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            var query = await session.Query<T>().Where(filter).ToListAsync();

            return query.Any() ? query[0] : throw new EntityNotFoundException("No entity was found under the requested name.");
        }

        public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {   
            if (filter is not null)
            {
                var query = await session.Query<T>().Where(filter).ToListAsync();
              
                return query.ToList().Any() ? await session.Query<T>().Where(filter).ToListAsync() : throw new EntityNotFoundException(filter.Body.ToString());
            }

            var all = await session.Query<T>().ToListAsync();

            return all.Any() ? all : throw new EntityNotFoundException("No entity was found under the requested name.");
        }
    }
}