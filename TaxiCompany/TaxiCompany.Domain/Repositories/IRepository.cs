namespace TaxiCompany.Domain.Repositories;

public interface IRepository<T>
{
    public IEnumerable<T> Get();
    public T? Get(int id);
    public void Post(T value);
    public bool Put(int id, T value);
    public bool Delete(int id);
}
