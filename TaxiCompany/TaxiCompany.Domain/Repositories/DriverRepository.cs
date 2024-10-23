namespace TaxiCompany.Domain.Repositories;

public class DriverRepository : IRepository<Driver>
{
    private readonly List<Driver> _drivers = [];

    public Driver? Get(int id) => _drivers.FirstOrDefault(d => d.Id == id);

    public IEnumerable<Driver> Get()
    {
        return _drivers;
    }

    public void Post(Driver driver)
    {
        _drivers.Add(driver);
    }

    public bool Put(int id, Driver driver)
    {
        var oldDriver = Get(id);
        
        if (oldDriver == null) return false;

        oldDriver.FullName = driver.FullName;
        oldDriver.PhoneNumber = driver.PhoneNumber;
        oldDriver.Passport = driver.Passport;
        oldDriver.Address = driver.Address;
        oldDriver.AssignedCarId = driver.AssignedCarId;

        return true;
    }

    public bool Delete(int id)
    {
        var oldDriver = Get(id);

        if (oldDriver != null) return false;

        _drivers.Remove(oldDriver);

        return true;
    }
}
