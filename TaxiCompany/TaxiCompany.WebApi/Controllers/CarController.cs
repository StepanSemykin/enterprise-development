using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;
using TaxiCompany.WebApi.DTO;

namespace TaxiCompany.WebApi.Controllers;

/// <summary>
/// Класс <c>Контроллер авто</c> для управления автомобилями в API.
/// Предоставляет конечные точки для получения, добавления, изменения и удаления авто.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CarController(IRepository<Car> repository, IRepository<Driver> repositoryDrivers, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получает список всех автомобилей.
    /// </summary>
    /// <returns>
    /// Возвращает результат операции, который содержит список автомобилей.
    /// Если автомобили не найдены, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Car>), 200)]
    public IActionResult Get()
    {
        var cars = repository.Get();

        if (cars == null) return NotFound();

        return Ok(cars);
    }

    /// <summary>
    /// Получает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>
    /// Возвращает результат операции, который содержит объект автомобиля.
    /// Если автомобиль не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Car), 200)]
    public IActionResult Get(int id)
    {
        var car = repository.Get(id);

        if (car == null) return NotFound();

        return Ok(car);
    }

    /// <summary>
    /// Добавляет новый автомобиль.
    /// </summary>
    /// <param name="value">Объект автомобиля, который нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные автомобиля некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public IActionResult Post([FromBody] CarDTO valueDTO)
    {
        var value = mapper.Map<Car>(valueDTO);

        var driver = repositoryDrivers.Get(value.AssignedDriverId);
        if (driver == null) return BadRequest($"Driver with ID {value.AssignedDriverId} was not found.");

        repository.Post(value);

        return Ok();
    }

    /// <summary>
    /// Обновляет данные автомобиля по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля, данные которого нужно обновить.</param>
    /// <param name="value">Объект автомобиля с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если данные автомобиля некорректны, возвращает статус 400 Bad Request.
    /// Если автомобиль с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CarDTO valueDTO)
    {
        var value = mapper.Map<Car>(valueDTO);

        var driver = repositoryDrivers.Get(value.AssignedDriverId);
        if (driver == null) return BadRequest($"Driver with ID {value.AssignedDriverId} was not found.");

        if (repository.Put(id, value)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Удаляет автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля, который нужно удалить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если удаление прошло успешно.
    /// Если автомобиль с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.Delete(id)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Получает сведения о конкретном водителе и его автомобиле.
    /// </summary>
    /// <param name="driverId">Идентификатор водителя.</param>
    /// <returns>
    /// Возвращает результат операции, который содержит сведения о водителе и его автомобиле.
    /// Если водитель или автомобиль не найдены, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("driver/{driverId}")]
    [ProducesResponseType(typeof(DriverCarInfoDTO), 200)]
    public IActionResult GetDriverAndCar(int driverId)
    {
        var driver = repositoryDrivers.Get(driverId);
        if (driver == null) return NotFound($"Driver with ID {driverId} was not found.");
        var car = repository.Get(driver.AssignedCarId);
        if (car == null) return NotFound($"Car assigned to driver with ID {driverId} was not found.");

        var driverDTO = mapper.Map<DriverDTO>(driver);
        var carDTO = mapper.Map<CarDTO>(car);

        var driverCarInfo = new DriverCarInfoDTO
        {
            Driver = driverDTO,
            Car = carDTO
        };

        return Ok(driverCarInfo);
    }
}
