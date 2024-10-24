namespace TaxiCompany.Domain.Repositories;

public class CarRepository : IRepository<Car>
{
    private readonly List<Car> _cars = [];

    public Car? Get(int id) => _cars.FirstOrDefault(c => c.Id == id);   

    public IEnumerable<Car> Get() => _cars;

    public void Post(Car value)
    {
        _cars.Add(value);
    }

    public bool Put(int id, Car value)
    {
        var oldCar = Get(id);

        if (oldCar == null) return false;

        oldCar.Colour = value.Colour;   
        oldCar.Model = value.Model;
        oldCar.SerialNumber = value.SerialNumber;
        oldCar.RealeseYear = value.RealeseYear;
        oldCar.AssignedDriverId = value.AssignedDriverId;

        return true;
    }

    public bool Delete(int id)
    {
        var oldCar = Get(id);

        if (oldCar == null) return false;

        _cars.Remove(oldCar);

        return true;
    }
}
