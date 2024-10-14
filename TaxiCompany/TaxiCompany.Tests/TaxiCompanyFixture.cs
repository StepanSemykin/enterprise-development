using TaxiCompany.Domain;

namespace TaxiCompany.Tests;

public class TaxiCompanyFixture
{
    public List<Car> CarsList =
    [
        new() {Id = 1, Colour = "Blue", Model = "Hyundai", SerialNumber = "111", RealeseYear = new DateTime(2019), AssignedDriverId = 1},
        new() {Id = 2, Colour = "White", Model = "Volvo", SerialNumber = "123", RealeseYear = new DateTime(2010), AssignedDriverId = 2},
        new() {Id = 3, Colour = "White", Model = "Toyota", SerialNumber = "231", RealeseYear = new DateTime(2012), AssignedDriverId = 3},
        new() {Id = 4, Colour = "Yellow", Model = "Hyundai", SerialNumber = "943", RealeseYear = new DateTime(2023), AssignedDriverId = 4},
        new() {Id = 5, Colour = "White", Model = "VAZ", SerialNumber = "874", RealeseYear = new DateTime(2022), AssignedDriverId = 5},
        new() {Id = 6, Colour = "Red", Model = "BMW", SerialNumber = "456", RealeseYear = new DateTime(2019), AssignedDriverId = 6},
        new() {Id = 7, Colour = "Black", Model = "Kia", SerialNumber = "911", RealeseYear = new DateTime(2018), AssignedDriverId = 7}
    ];

    public List<Client> ClientsList =
    [
        new() {Id = 1, FullName = "Stepan Semykin", PhoneNumber = "89225535002"},
        new() {Id = 2, FullName = "Sergey Sidorov", PhoneNumber = "89225333102"},
        new() {Id = 3, FullName = "Ivan Ivanov", PhoneNumber = "89225534801"},
        new() {Id = 4, FullName = "Pavel Petrov", PhoneNumber = "89225331101"},
        new() {Id = 5, FullName = "Dmitry Donskoy", PhoneNumber = "89225531234"},
        new() {Id = 6, FullName = "Alexey Smirnov", PhoneNumber = "89225535500"},
        new() {Id = 7, FullName = "Olga Kuznetsova", PhoneNumber = "89225537722"},
        new() {Id = 8, FullName = "Natalia Popova", PhoneNumber = "89225530044"},
        new() {Id = 9, FullName = "Mikhail Lukin", PhoneNumber = "89225531212"},
        new() {Id = 10, FullName = "Svetlana Romanova", PhoneNumber = "89225536699"},
        new() {Id = 11, FullName = "Yury Gagarin", PhoneNumber = "89225535511"}
    ];

    public List<Driver> DriversList =
    [
        new() {Id = 1, FullName = "Andrey Kovalev", PhoneNumber = "89165531245", Passport = "1234 4512", Address = "Lenina 12, Moscow", AssignedCarId = 1},
        new() {Id = 2, FullName = "Maxim Pavlov", PhoneNumber = "89163332111", Passport = "2345 1234", Address = "Pushkina 5, Saint Petersburg", AssignedCarId = 2},
        new() {Id = 3, FullName = "Vladimir Kozlov", PhoneNumber = "89167778901", Passport = "3456 2345", Address = "Gagarina 8, Kazan", AssignedCarId = 3},
        new() {Id = 4, FullName = "Artem Makarov", PhoneNumber = "89162331457", Passport = "4567 3456", Address = "Kirova 20, Novosibirsk", AssignedCarId = 4},
        new() {Id = 5, FullName = "Nikita Frolov", PhoneNumber = "89165536587", Passport = "5678 4567", Address = "Suvorova 10, Yekaterinburg", AssignedCarId = 5},
        new() {Id = 6, FullName = "Ekaterina Morozova", PhoneNumber = "89167895332", Passport = "6789 5678", Address = "Tverskaya 22, Moscow", AssignedCarId = 6},
        new() {Id = 7, FullName = "Sofia Petrova", PhoneNumber = "89164427755", Passport = "7890 6789", Address = "Nevsky 15, Saint Petersburg", AssignedCarId = 7}
    ];

    public List<Trip> TripsList =
    [
        new() 
        {
            Id = 1, 
            Departure = "Lenina Street", 
            Destination = "Pushkina Street", 
            Date = new DateTime(2024, 10, 23), 
            DrivingTime = new TimeOnly(0, 23, 13), 
            Cost = 234, AssignedClientId = 1, 
            AssignedCarId = 1
        },
        new() 
        {
            Id = 2, 
            Departure = "Gagarina Avenue", 
            Destination = "Tverskaya Street", 
            Date = new DateTime(2024, 11, 02), 
            DrivingTime = new TimeOnly(0, 30, 8), 
            Cost = 150, 
            AssignedClientId = 2, 
            AssignedCarId = 2
        },
        new() 
        {
            Id = 3, 
            Departure = "Kirova Street", 
            Destination = "Suvorova Street", 
            Date = new DateTime(2024, 12, 05), 
            DrivingTime = new TimeOnly(0, 15, 12), 
            Cost = 100, AssignedClientId = 3, 
            AssignedCarId = 3
        },
        new() 
        {
            Id = 4, 
            Departure = "Nevsky Prospekt", 
            Destination = "Liteiny Prospekt",
            Date = new DateTime(2024, 8, 28), 
            DrivingTime = new TimeOnly(0, 40, 34), 
            Cost = 200, 
            AssignedClientId = 4, 
            AssignedCarId = 4
        },
        new() 
        {
            Id = 5, 
            Departure = "Sovetskaya Street", 
            Destination = "Revolution Square", 
            Date = new DateTime(2024, 11, 10), 
            DrivingTime = new TimeOnly(0, 25, 32), 
            Cost = 180, 
            AssignedClientId = 5, 
            AssignedCarId = 5
        },
        new() 
        {
            Id = 6,
            Departure = "Komsomolskaya Street", 
            Destination = "Pobedy Avenue",
            Date = new DateTime(2024, 2, 15), 
            DrivingTime = new TimeOnly(0, 35, 22), 
            Cost = 250, 
            AssignedClientId = 6, 
            AssignedCarId = 6
        },
        new() 
        {
            Id = 7, 
            Departure = "Central Park", 
            Destination = "Railway Station", 
            Date = new DateTime(2024, 12, 20), 
            DrivingTime = new TimeOnly(0, 20, 2), 
            Cost = 170, 
            AssignedClientId = 7, 
            AssignedCarId = 7
        },
        new() 
        {
            Id = 8, 
            Departure = "Kirova Street", 
            Destination = "Pushkina Street", 
            Date = new DateTime(2024, 10, 23), 
            DrivingTime = new TimeOnly(0, 23, 13), 
            Cost = 234, 
            AssignedClientId = 1, 
            AssignedCarId = 5
        },
        new() 
        {
            Id = 9, 
            Departure = "Lenina Street", 
            Destination = "Pushkina Street", 
            Date = new DateTime(2024, 5, 23), 
            DrivingTime = new TimeOnly(0, 13, 13), 
            Cost = 223, 
            AssignedClientId = 11, 
            AssignedCarId = 2
        },
        new() 
        {
            Id = 10, 
            Departure = "Airport Road", 
            Destination = "City Center", 
            Date = new DateTime(2024, 12, 02), 
            DrivingTime = new TimeOnly(1, 55, 1), 
            Cost = 350, 
            AssignedClientId = 10, 
            AssignedCarId = 3
        },
        new() 
        {
            Id = 11, 
            Departure = "Main Street", 
            Destination = "City Hall",
            Date = new DateTime(2024, 1, 29), 
            DrivingTime = new TimeOnly(1, 28, 28), 
            Cost = 798, 
            AssignedClientId = 11, 
            AssignedCarId = 4
        },
        new() 
        {
            Id = 12, 
            Departure = "Shopping Mall", 
            Destination = "Train Station", 
            Date = new DateTime(2024, 11, 22), 
            DrivingTime = new TimeOnly(0, 40, 45), 
            Cost = 567, 
            AssignedClientId = 4,
            AssignedCarId = 2
        },
        new() 
        {
            Id = 13, 
            Departure = "Sports Complex", 
            Destination = "Cinema", 
            Date = new DateTime(2024, 4, 30), 
            DrivingTime = new TimeOnly(0, 35, 39),
            Cost = 270,
            AssignedClientId = 6, 
            AssignedCarId = 6
        }
    ];
}
