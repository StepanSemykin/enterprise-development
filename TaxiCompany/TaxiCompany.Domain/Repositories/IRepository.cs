namespace TaxiCompany.Domain.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAsync();
    Task<T?> GetAsync(int id);
    Task PostAsync(T value);
    Task<bool> PutAsync(int id, T value);
    Task<bool> DeleteAsync(int id);
}