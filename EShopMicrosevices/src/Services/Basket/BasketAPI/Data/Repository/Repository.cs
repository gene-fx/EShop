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

        public void Delete(string Id)
        {
             session.Delete<T>(Id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            var query = await session.Query<T>().Where(filter).ToListAsync();

            return query[0];
        }

        public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {   
            if (filter is not null)
            {
                var query = await session.Query<T>().Where(filter).ToListAsync();

                return query.ToList();
            }

            var all = await session.Query<T>().ToListAsync();

            return all.ToList();
        }
    }
}