using AutoMapper;
using TaxiCompany.Domain;
using TaxiCompany.WebApi.DTO;   

namespace TaxiCompany.WebApi;

public class Mapping :  Profile
{
    public Mapping()
    {
        CreateMap<Car, CarDTO>().ReverseMap();
        CreateMap<Client, ClientDTO>().ReverseMap();
        CreateMap<Driver, DriverDTO>().ReverseMap();
        CreateMap<Trip, TripDTO>().ReverseMap();
    }
}