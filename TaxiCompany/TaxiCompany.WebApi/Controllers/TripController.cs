using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using TaxiCompany.Domain.Entities;
using TaxiCompany.Domain.Repositories;
using TaxiCompany.WebApi.Dto;

namespace TaxiCompany.WebApi.Controllers;

/// <summary>
/// Класс <c>Контроллер поездок</c> для управления поездками в API.
/// Предоставляет конечные точки для получения, добавления, изменения и удаления поездок.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TripController(IRepository<Trip> repository, IRepository<Client> repositoryClients, IRepository<Car> repositoryCars, IRepository<Driver> repositoryDrivers, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получает список всех поездок.
    /// </summary>
    /// <returns>
    /// Возвращает результат операции, который содержит список поездок.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Trip>), 200)]
    public async Task<IActionResult> Get()
    {
        var trips = await repository.GetAsync();

        return Ok(trips);
    }

    /// <summary>
    /// Получает поездку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор поездки.</param>
    /// <returns>
    /// Возвращает результат операции, который содержит объект поездки.
    /// Если поездка не найдена, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Trip), 200)]
    public async Task<IActionResult> Get(int id)
    {
        var trip = await repository.GetAsync(id);

        if (trip == null) return NotFound();

        return Ok(trip);
    }

    /// <summary>
    /// Добавляет новую поездку.
    /// </summary>
    /// <param name="valueDto">Объект поездки, которую нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные поездки некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TripDto valueDto)
    {
        var value = mapper.Map<Trip>(valueDto);

        var car = await repositoryCars.GetAsync(value.AssignedCarId);
        if (car == null) return BadRequest($"Car with ID {value.AssignedCarId} was not found.");
        var client = await repositoryClients.GetAsync(value.AssignedClientId);
        if (client == null) return BadRequest($"Client with ID {value.AssignedClientId} was not found.");

        await repository.PostAsync(value);

        return Ok();
    }

    /// <summary>
    /// Обновляет данные поездки по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор поездки, данные которой нужно обновить.</param>
    /// <param name="valueDto">Объект поездки с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если данные поездки некорректны, возвращает статус 400 Bad Request.
    /// Если поездка с указанным идентификатором не найдена, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TripDto valueDto)
    {
        var value = mapper.Map<Trip>(valueDto);

        var car = await repositoryCars.GetAsync(value.AssignedCarId);
        if (car == null) return BadRequest($"Car with ID {value.AssignedCarId} was not found.");
        var client = await repositoryClients.GetAsync(value.AssignedClientId);
        if (client == null) return BadRequest($"Client with ID {value.AssignedClientId} was not found.");

        if (await repository.PutAsync(id, value)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Удаляет поездку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор поездки, которую нужно удалить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если удаление прошло успешно.
    /// Если поездка с указанным идентификатором не найдена, возвращает статус 404 Not Found.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await repository.DeleteAsync(id)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Получает всех пассажиров, совершивших поездки за заданный период.
    /// </summary>
    /// <param name="startDate">Дата начала периода.</param>
    /// <param name="endDate">Дата окончания периода.</param>
    /// <returns>
    /// Возвращает список пассажиров, упорядоченный по ФИО.
    /// Если даты указаны некорректно, возвращает статус 400 Bad Request.
    /// Если поездки не найдены, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("passengers")]
    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    public async Task<IActionResult> GetClientsByDate(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate) return BadRequest("Start date cannot be later than end date.");

        var trips = await repository.GetAsync();
        if (trips == null) return NotFound();

        var filteredTrips = trips
            .Where(trip => trip.Date >= startDate && trip.Date <= endDate)
            .ToList();
        if (filteredTrips == null) return NotFound("No trips found for the specified date range.");

        var clientIds = filteredTrips.Select(t => t.AssignedClientId).Distinct();
        var clients = await repositoryClients.GetAsync();

        var sortedClients = clients
            .Where(client => clientIds.Contains(client.Id))
            .OrderBy(client => client.FullName)
            .ToList();

        return Ok(sortedClients);
    }

    /// <summary>
    /// Получает количество поездок для каждого клиента.
    /// </summary>
    /// <returns>
    /// Возвращает статус 200 OK с количеством поездок для каждого клиента.
    /// Если нет поездок, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("trip-counts")]
    [ProducesResponseType(typeof(IEnumerable<ClientTripCountDto>), 200)]
    public async Task<IActionResult> GetCountTrips()
    {
        var trips = await repository.GetAsync();
        if (trips == null) return NotFound();

        var clients = await repositoryClients.GetAsync();

        var tripCounts = trips
            .GroupBy(trip => trip.AssignedClientId)
            .Select(group => new ClientTripCountDto
            {
                Client = mapper.Map<ClientDto>(clients.FirstOrDefault(c => c.Id == group.Key)),
                TripCount = group.Count()
            })
            .ToList();

        return Ok(tripCounts);
    }

    /// <summary>
    /// Получает топ 5 водителей по количеству совершенных поездок.
    /// </summary>
    /// <returns>
    /// Возвращает статус 200 OK с информацией о топ-5 водителях и количестве их поездок.
    /// Если нет поездок или авто, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("top-drivers")]
    [ProducesResponseType(typeof(IEnumerable<DriverTripCountDto>), 200)]
    public async Task<IActionResult> GetTopDrivers()
    {
        var trips = await repository.GetAsync();
        if (trips == null) return NotFound();
        var cars = await repositoryCars.GetAsync();
        if (cars == null) return NotFound();

        var drivers = await repositoryDrivers.GetAsync();

        var tripCounts = trips
            .GroupBy(trip => trip.AssignedCarId)
            .Select(group => new
            {
                CarId = group.Key,
                TripCount = group.Count()
            })
            .ToList();

        var topDrivers = tripCounts
            .Select(tc => {
                var car = cars.FirstOrDefault(c => c.Id == tc.CarId);
                if (car == null) return null; 

                var driver = drivers.FirstOrDefault(d => d.Id == car.AssignedDriverId);
                return new DriverTripCountDto
                {
                    Driver = mapper.Map<DriverDto>(driver),
                    TripCount = tc.TripCount
                };
            })
            .OrderByDescending(d => d!.TripCount)
            .Take(5)
            .ToList();

        return Ok(topDrivers);
    }

    /// <summary>
    /// Получает статистику по количеству поездок, среднему и максимальному времени поездки для каждого водителя.
    /// </summary>
    /// <returns>
    /// Возвращает статус 200 OK с информацией по каждому водителю.
    /// Если поездок нет, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("driver-trip-stats")]
    [ProducesResponseType(typeof(IEnumerable<DriverTripStatsDto>), 200)]
    public async Task<IActionResult> GetDriverTripStats()
    {
        var trips = await repository.GetAsync();
        if (trips == null) return NotFound();

        var cars = await repositoryCars.GetAsync();
        if (cars == null) return NotFound();

        var drivers = await repositoryDrivers.GetAsync();

        var driverStats = trips
            .GroupBy(trip => trip.AssignedCarId)
            .Select(group =>
            {
                var car = cars.FirstOrDefault(c => c.Id == group.Key);
                if (car == null) return null;

                var driver = drivers.FirstOrDefault(d => d.Id == car.AssignedDriverId);
                if (driver == null) return null;

                return new DriverTripStatsDto
                {
                    Driver = mapper.Map<DriverDto>(driver),
                    TripCount = group.Count(),
                    AverageDrivingTime = new TimeOnly(Convert.ToInt64(group.Average(trip => trip.DrivingTime.Ticks))),
                    MaxDrivingTime = group.Max(trip => trip.DrivingTime)
                };
            })
            .Where(d => d != null)
            .ToList();

        return Ok(driverStats);
    }

    /// <summary>
    /// Получает информацию о пассажирах, совершивших максимальное количество поездок за указанный период.
    /// </summary>
    /// <param name="startDate">Дата начала периода.</param>
    /// <param name="endDate">Дата окончания периода.</param>
    /// <returns>
    /// Возвращает статус 200 OK с информацией о пассажирах и количестве их поездок.
    /// Если даты указаны некорректно, возвращает статус 400 Bad Request.
    /// Если поездок нет, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("top-clients")]
    [ProducesResponseType(typeof(IEnumerable<ClientTripCountDto>), 200)]
    public async Task<IActionResult> GetClientsMaxTrips(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate) return BadRequest("Start date cannot be later than end date.");

        var trips = await repository.GetAsync(); 
        if (trips == null) return NotFound();

        var filteredTrips = trips
            .Where(trip => trip.Date >= startDate && trip.Date <= endDate)
            .ToList();

        var clientTrips = trips
            .GroupBy(trip => trip.AssignedClientId)
            .Select(group => new
            {
                ClientId = group.Key,
                TripCount = group.Count()
            })
            .ToList();

        var maxTripCount = clientTrips.Max(ct => ct.TripCount);

        var topClients = await Task.WhenAll(
            clientTrips
                .Where(ct => ct.TripCount == maxTripCount)
                .Select(async ct => new ClientTripCountDto
                {
                    Client = mapper.Map<ClientDto>(await repositoryClients.GetAsync(ct.ClientId)),
                    TripCount = ct.TripCount
                })
        );

        return Ok(topClients);
    }
}
