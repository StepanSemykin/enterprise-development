namespace TaxiCompany.Domain.Repositories;

public class DriverRepository : IRepository<Driver>
{
    private readonly List<Driver> _drivers = [];

    public Driver? Get(int id) => _drivers.FirstOrDefault(d => d.Id == id);

    public IEnumerable<Driver> Get() => _drivers;

    public void Post(Driver value)
    {
        _drivers.Add(value);
    }

    public bool Put(int id, Driver value)
    {
        var oldDriver = Get(id);
        
        if (oldDriver == null) return false;

        oldDriver.FullName = value.FullName;
        oldDriver.PhoneNumber = value.PhoneNumber;
        oldDriver.Passport = value.Passport;
        oldDriver.Address = value.Address;
        oldDriver.AssignedCarId = value.AssignedCarId;

        return true;
    }

    public bool Delete(int id)
    {
        var oldDriver = Get(id);

        if (oldDriver == null) return false;

        _drivers.Remove(oldDriver);

        return true;
    }
}
