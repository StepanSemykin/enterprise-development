namespace TaxiCompany.Domain.Repositories;

/// <summary>
/// Класс <c>Репозиторий клиентов</c>.
/// Содержит методы для получения, добавления, изменения и удаления клиента.
/// </summary>
public class ClientRepository : IRepository<Client>
{
    private readonly List<Client> _clients = [];

    /// <summary>
    /// Получает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент, если найден; иначе null.</returns>
    public Client? Get(int id) => _clients.FirstOrDefault(c => c.Id == id);

    /// <summary>
    /// Получает всех клиентов.
    /// </summary>
    /// <returns>Коллекция всех клиентов.</returns>
    public IEnumerable<Client> Get() => _clients;

    /// <summary>
    /// Добавляет нового клиента.
    /// </summary>
    /// <param name="value">Объект клиента для добавления.</param>
    public void Post(Client value)
    {
        _clients.Add(value);
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

        _clients.Remove(oldClient);

        return true;
    }
}
