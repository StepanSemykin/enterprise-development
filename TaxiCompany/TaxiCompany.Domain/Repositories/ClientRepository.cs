namespace TaxiCompany.Domain.Repositories;

public class ClientRepository : IRepository<Client>
{
    private readonly List<Client> _clients = [];

    public Client? Get(int id) => _clients.FirstOrDefault(c => c.Id == id);

    public IEnumerable<Client> Get() => _clients;

    public void Post(Client value)
    {
        _clients.Add(value);
    }

    public bool Put(int id, Client value)
    {
        var oldClient = Get(id);

        if (oldClient == null) return false;

        oldClient.FullName = value.FullName;
        oldClient.PhoneNumber = value.PhoneNumber;

        return true;
    }

    public bool Delete(int id)
    {
        var oldClient = Get(id);

        if (oldClient == null) return false;

        _clients.Remove(oldClient);

        return true;
    }
}
