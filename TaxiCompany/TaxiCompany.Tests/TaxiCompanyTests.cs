using TaxiCompany.Domain;

namespace TaxiCompany.Tests;
/// <summary>
/// Класс для проведения юнит-тестов
/// </summary>
/// <param name="fixture"></param>
public class TaxiCompanyTests(TaxiCompanyFixture fixture) : IClassFixture<TaxiCompanyFixture>
{
    private readonly TaxiCompanyFixture _fixture = fixture;
    /// <summary>
    /// Выводит все сведения о конкретном водителе и его автомобиле
    /// </summary>
    [Fact]
    public void GetDriverAndCar()
    {
        var driverId = 5;
        var expectedDriver = _fixture.DriversList[4];
        var expectedCar = _fixture.CarsList[4];

        var result = _fixture.DriversList
            .Join(_fixture.CarsList,
                  driver => driver.AssignedCarId,  
                  car => car.Id,                  
                  (driver, car) => new { Driver = driver, Car = car }) 
            .Where(x => x.Driver.Id == driverId)  
            .FirstOrDefault();

        Assert.Equal(expectedDriver, result.Driver);
        Assert.Equal(expectedCar, result.Car);
    }
    /// <summary>
    /// Выводит всех пассажиров, совершивших поездки за заданный период, упорядочивая по ФИО
    /// </summary>
    [Fact]
    public void GetClientsByDate()
    {
        var expectedData = new List<Client>
        {
            _fixture.ClientsList[5],
            _fixture.ClientsList[10]
        };

        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 6, 30);

        var passengers = _fixture.TripsList
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .Select(t => _fixture.ClientsList.First(c => c.Id == t.AssignedClientId))
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(expectedData, passengers);
    }
    /// <summary>
    /// Выводит количество поездок каждого пассажира
    /// </summary>
    [Fact]
    public void GetCountTrips()
    {
        var expectedData = new[]
        {
            new { Client = _fixture.ClientsList[0], TripCount = 2 },
            new { Client = _fixture.ClientsList[1], TripCount = 1 },
            new { Client = _fixture.ClientsList[2], TripCount = 1 },
            new { Client = _fixture.ClientsList[3], TripCount = 2 },
            new { Client = _fixture.ClientsList[4], TripCount = 1 },
            new { Client = _fixture.ClientsList[5], TripCount = 2 },
            new { Client = _fixture.ClientsList[6], TripCount = 1 },
            new { Client = _fixture.ClientsList[7], TripCount = 0 },
            new { Client = _fixture.ClientsList[8], TripCount = 0 },
            new { Client = _fixture.ClientsList[9], TripCount = 1 },
            new { Client = _fixture.ClientsList[10], TripCount = 2 }
        };

        var result = _fixture.ClientsList
            .GroupJoin(
                _fixture.TripsList,
                client => client.Id,
                trip => trip.AssignedClientId,
                (client, trips) => new
                {
                    Client = client,
                    TripCount = trips.Count()
                }
            )
            .ToList();

        Assert.Equal(expectedData.Length, result.Count); 

        for (int i = 0; i < expectedData.Length; i++)
        {
            Assert.Equal(expectedData[i].Client.Id, result[i].Client.Id); 
            Assert.Equal(expectedData[i].TripCount, result[i].TripCount); 
        }
    }
    /// <summary>
    /// Выводит топ 5 водителей по совершенному количеству поездок
    /// </summary>
    [Fact]
    public void GetTopDrivers()
    {
        var expectedData = new[]
        {
            new { Driver = _fixture.DriversList[1], TripCount = 3 },
            new { Driver = _fixture.DriversList[2], TripCount = 2 },
            new { Driver = _fixture.DriversList[3], TripCount = 2 },
            new { Driver = _fixture.DriversList[4], TripCount = 2 },
            new { Driver = _fixture.DriversList[5], TripCount = 2 }
        };

        var drivers = _fixture.TripsList
            .GroupBy(trip => trip.AssignedCarId)  
            .Select(group => new
            {
                CarId = group.Key,                
                TripCount = group.Count()         
            })
            .Join(_fixture.DriversList,           
                  carGroup => carGroup.CarId,     
                  driver => driver.AssignedCarId, 
                  (carGroup, driver) => new       
                  {
                      Driver = driver,
                      TripCount = carGroup.TripCount
                  })
            .OrderByDescending(d => d.TripCount) 
            .Take(5)                              
            .ToList();

        Assert.Equal(expectedData, drivers);
    }
    /// <summary>
    ///  Выводит информацию о количестве поездок, среднем времени и максимальном времени поездки для каждого водителя
    /// </summary>
    [Fact]
    public void GetDriverTripStats()
    {
        var expectedData = new[]
        {
            new 
            { 
                Driver = _fixture.DriversList[0], 
                TripCount = 1, 
                AvgDrivingTime = new TimeOnly(0, 23, 13), 
                MaxDrivingTime = new TimeOnly(0, 23, 13) 
            },
            new 
            { 
                Driver = _fixture.DriversList[1], 
                TripCount = 3, 
                AvgDrivingTime = new TimeOnly(0, 28, 2),
                MaxDrivingTime = new TimeOnly(0, 40, 45) 
            },
            new 
            { 
                Driver = _fixture.DriversList[2], 
                TripCount = 2, 
                AvgDrivingTime = new TimeOnly(1, 5, 6, 500), 
                MaxDrivingTime = new TimeOnly(1, 55, 1) 
            },
            new 
            { 
                Driver = _fixture.DriversList[3], 
                TripCount = 2, 
                AvgDrivingTime = new TimeOnly(1, 4, 31), 
                MaxDrivingTime = new TimeOnly(1, 28, 28) 
            },
            new 
            { 
                Driver = _fixture.DriversList[4], 
                TripCount = 2, 
                AvgDrivingTime = new TimeOnly(0, 24, 22, 500), 
                MaxDrivingTime = new TimeOnly(0, 25, 32) 
            },
            new 
            { 
                Driver = _fixture.DriversList[5], 
                TripCount = 2, 
                AvgDrivingTime = new TimeOnly(0, 35, 30, 500), 
                MaxDrivingTime = new TimeOnly(0, 35, 39) 
            },
            new 
            { 
                Driver = _fixture.DriversList[6], 
                TripCount = 1, 
                AvgDrivingTime = new TimeOnly(0, 20, 2), 
                MaxDrivingTime = new TimeOnly(0, 20, 2) 
            }
        };

        var drivers = _fixture.TripsList
            .GroupBy(trip => trip.AssignedCarId)  
            .Select(group => new
            {
                CarId = group.Key,                
                TripCount = group.Count(),        
                AvgDrivingTime = new TimeOnly(0, 0).Add(group
                    .Select(t => t.DrivingTime.ToTimeSpan())  
                    .Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2) 
                    .Divide(group.Count() == 0 ? 1 : group.Count())), 
                MaxDrivingTime = group.Max(t => t.DrivingTime)  
            })
            .Join(_fixture.DriversList,          
                  carGroup => carGroup.CarId,      
                  driver => driver.AssignedCarId,  
                  (carGroup, driver) => new        
                  {
                      Driver = driver,
                      TripCount = carGroup.TripCount,
                      AvgDrivingTime = carGroup.AvgDrivingTime,
                      MaxDrivingTime = carGroup.MaxDrivingTime
                  })
            .ToList();

        Assert.Equal(expectedData.Length, drivers.Count);

        for (int i = 0; i < expectedData.Length; i++)
        {
            Assert.Equal(expectedData[i].Driver.Id, drivers[i].Driver.Id);  
            Assert.Equal(expectedData[i].TripCount, drivers[i].TripCount);  
            Assert.Equal(expectedData[i].AvgDrivingTime, drivers[i].AvgDrivingTime);  
            Assert.Equal(expectedData[i].MaxDrivingTime, drivers[i].MaxDrivingTime);  
        }
    }
    /// <summary>
    /// Выводит информацию о пассажирах, совершивших максимальное число поездок за указанный период
    /// </summary>
    [Fact]
    public void GetClientsMaxTrips()
    {
        var expectedData = new List<Client> 
        {
            _fixture.ClientsList[0],
            _fixture.ClientsList[3],
            _fixture.ClientsList[5],
            _fixture.ClientsList[10]
        };

        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 12, 31);

        var trips = _fixture.TripsList
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .GroupBy(t => t.AssignedClientId)
            .Select(group => new
            {
                Client = _fixture.ClientsList.First(c => c.Id == group.Key),
                TripCount = group.Count()
            })
            .ToList();
        var maxTripCount = trips.Max(c => c.TripCount);
        var clients = trips
            .Where(c => c.TripCount == maxTripCount)
            .Select(c => c.Client)
            .ToList();

        Assert.Equal(expectedData, clients);
    }
}