using System.Security.Cryptography;
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
    public Client? Get(int id) => context.Clinets.FirstOrDefault(c => c.Id == id);

    /// <summary>
    /// Получает всех клиентов.
    /// </summary>
    /// <returns>Коллекция всех клиентов.</returns>
    public IEnumerable<Client> Get() => context.Clinets;

    /// <summary>
    /// Добавляет нового клиента.
    /// </summary>
    /// <param name="value">Объект клиента для добавления.</param>
    public void Post(Client value)
    {
        context.Clinets.Add(value);
        context.SaveChanges();
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого клиента.</param>
    /// <param name="value">Новые данные для клиента.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public bool Put(int id, Client value)
    {
        var oldClient = Get(id);

        if (oldClient == null) return false;

        oldClient.FullName = value.FullName;
        oldClient.PhoneNumber = value.PhoneNumber;
        context.SaveChanges();

        return true;
    }

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public bool Delete(int id)
    {
        var oldClient = Get(id);

        if (oldClient == null) return false;

        context.Clinets.Remove(oldClient);
        context.SaveChanges();

        return true;
    }
}
