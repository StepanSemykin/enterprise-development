using AutoMapper;
using TaxiCompany.Domain;
using TaxiCompany.WebApi.Dto;   

namespace TaxiCompany.WebApi;

public class Mapping :  Profile
{
    public Mapping()
    {
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Driver, DriverDto>().ReverseMap();
        CreateMap<Trip, TripDto>().ReverseMap();
    }
}