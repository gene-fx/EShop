using BuildingBlocks.Repository.IRepository;

namespace BuildingBlocks.Repository;

internal class Repository<T> : IRepository<T> where T : class
{
    public T Add(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public T Get(string id)
    {
        throw new NotImplementedException();
    }

    public Span<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T Update(T entity)
    {
        throw new NotImplementedException();
    }
}
