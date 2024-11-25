namespace TaxiCompany.Wasm.WebApi;

public interface ITaxiCompanyApiWrapper
{
    Task<IEnumerable<Car>> GetCars();
    Task<IEnumerable<Client>> GetClients();
    Task<IEnumerable<Driver>> GetDrivers();
    Task<IEnumerable<Trip>> GetTrips();

    Task<Car> GetCar(int id);
    Task<Client> GetClient(int id);
    Task<Driver> GetDriver(int id);
    Task<Trip> GetTrip(int id);

    Task CreateCar(CarDto car);
    Task CreateClient(ClientDto client);
    Task CreateDriver(DriverDto driver);
    Task CreateTrip(TripDto trip);  

    Task UpdateCar(int id, CarDto car);
    Task UpdateClient(int id, ClientDto client);
    Task UpdateDriver(int id, DriverDto driver);    
    Task UpdateTrip(int id,TripDto trip);

    Task DeleteCar(int id);
    Task DeleteClient(int id);
    Task DeleteDriver(int id);
    Task DeleteTrip(int id);

    Task<DriverCarInfoDto> GetDriverAndCar(int id);
    Task<IEnumerable<ClientDto>> GetClientsByDate(DateTime startDate, DateTime endDate);  
    Task<IEnumerable<ClientTripCountDto>> GetCountTrips();
    Task<IEnumerable<DriverTripCountDto>> GetTopDrivers();
    Task<IEnumerable<DriverTripStatsDto>> GetDriverTripStats();
    Task<IEnumerable<ClientTripCountDto>> GetClientsMaxTrips(DateTime startDate, DateTime endDate);
}
