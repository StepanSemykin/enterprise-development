using Microsoft.EntityFrameworkCore;
using TaxiCompany.Domain.Context;
using TaxiCompany.Domain.Entities;

namespace TaxiCompany.Domain.Repositories;

/// <summary>
/// Класс <c>Репозиторий клиентов</c>.
/// Содержит методы для получения, добавления, изменения и удаления клиента.
/// </summary>
public class ClientRepository(TaxiCompanyDbContext context) : IRepository<Client>
{
    /// <summary>
    /// Получает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент, если найден; иначе null.</returns>
    public async Task<Client?> GetAsync(int id) => await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Получает всех клиентов.
    /// </summary>
    /// <returns>Коллекция всех клиентов.</returns>
    public async Task<IEnumerable<Client>> GetAsync() => await context.Clients.ToListAsync();

    /// <summary>
    /// Добавляет нового клиента.
    /// </summary>
    /// <param name="value">Объект клиента для добавления.</param>
    public async Task PostAsync(Client value)
    {
        context.Clients.Add(value);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого клиента.</param>
    /// <param name="value">Новые данные для клиента.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public async Task<bool> PutAsync(int id, Client value)
    {
        var oldClient = await GetAsync(id);

        if (oldClient == null) return false;

        oldClient.FullName = value.FullName;
        oldClient.PhoneNumber = value.PhoneNumber;
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var oldClient = await GetAsync(id);

        if (oldClient == null) return false;

        context.Clients.Remove(oldClient);
        await context.SaveChangesAsync();

        return true;
    }
}
