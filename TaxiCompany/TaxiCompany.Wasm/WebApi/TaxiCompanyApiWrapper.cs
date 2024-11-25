namespace TaxiCompany.Wasm.WebApi;

public class TaxiCompanyApiWrapper(IConfiguration configuration) : ITaxiCompanyApiWrapper
{
    public readonly TaxiCompanyApi _host = new(configuration["OpenApi:ServerUrl"], new HttpClient());

    public async Task<IEnumerable<Car>> GetCars() => await _host.CarAllAsync();
    public async Task<IEnumerable<Client>> GetClients() => await _host.ClientAllAsync();
    public async Task<IEnumerable<Driver>> GetDrivers() => await _host.DriverAllAsync();
    public async Task<IEnumerable<Trip>> GetTrips() => await _host.TripAllAsync();

    public async Task<Car> GetCar(int id) => await _host.CarGETAsync(id);
    public async Task<Client> GetClient(int id) => await _host.ClientGETAsync(id);
    public async Task<Driver> GetDriver(int id) => await _host.DriverGETAsync(id);
    public async Task<Trip> GetTrip(int id) => await _host.TripGETAsync(id);

    public async Task CreateCar(CarDto car) => await _host.CarPOSTAsync(car);
    public async Task CreateClient(ClientDto client) => await _host.ClientPOSTAsync(client);
    public async Task CreateDriver(DriverDto driver) => await _host.DriverPOSTAsync(driver);
    public async Task CreateTrip(TripDto trip) => await _host.TripPOSTAsync(trip);

    public async Task UpdateCar(int id, CarDto car) => await _host.CarPUTAsync(id, car);
    public async Task UpdateClient(int id, ClientDto client) => await _host.ClientPUTAsync(id, client);
    public async Task UpdateDriver(int id, DriverDto driver) => await _host.DriverPUTAsync(id, driver);
    public async Task UpdateTrip(int id, TripDto trip) => await _host.TripPUTAsync(id, trip);

    public async Task DeleteCar(int id) => await _host.CarDELETEAsync(id);
    public async Task DeleteClient(int id) => await _host.ClientDELETEAsync(id);
    public async Task DeleteDriver(int id) => await _host.DriverDELETEAsync(id);
    public async Task DeleteTrip(int id) => await _host.TripDELETEAsync(id);

    public async Task<DriverCarInfoDto> GetDriverAndCar(int id) => await _host.DriverAsync(id);
    public async Task<IEnumerable<ClientDto>> GetClientsByDate(DateTime startDate, DateTime endDate) => await _host.PassengersAsync(startDate, endDate);
    public async Task<IEnumerable<ClientTripCountDto>> GetCountTrips() => await _host.TripCountsAsync();
    public async Task<IEnumerable<DriverTripCountDto>> GetTopDrivers() => await _host.TopDriversAsync();
    public async Task<IEnumerable<DriverTripStatsDto>> GetDriverTripStats() => await _host.DriverTripStatsAsync();
    public async Task<IEnumerable<ClientTripCountDto>> GetClientsMaxTrips(DateTime startDate, DateTime endDate) => await _host.TopClientsAsync(startDate, endDate);
}
