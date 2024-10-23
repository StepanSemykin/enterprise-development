
namespace TaxiCompany.Domain.Repositories;

public class TripRepository : IRepository<Trip>
{
    private readonly List<Trip> _trips = [];

    public IEnumerable<Trip> Get() => _trips;

    public Trip? Get(int id) => _trips.FirstOrDefault(t => t.Id == id);  

    public void Post(Trip value)
    {
       _trips.Add(value);
    }

    public bool Put(int id, Trip value)
    {
        var oldTrip = Get(id);

        if (oldTrip == null) return false;

        oldTrip.Departure = value.Departure;
        oldTrip.Destination = value.Destination;
        oldTrip.Date = value.Date;
        oldTrip.DrivingTime = value.DrivingTime;    
        oldTrip.Cost = value.Cost;  
        oldTrip.AssignedClientId = value.AssignedClientId;
        oldTrip.AssignedCarId = value.AssignedCarId;

        return true;
    }

    public bool Delete(int id)
    {
        var oldTrip = Get(id);

        if (oldTrip == null) return false;

        _trips.Remove(oldTrip); 

        return true;
    }
}
