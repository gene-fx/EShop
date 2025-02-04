namespace BuildingBlocks.Repository.IRepository;

internal interface IRepository<T> where T : class
{
    T Get(string id);

    Span<T> GetAll();

    T Add(T entity);

    T Update(T entity);

    bool Delete(T entity);
}
