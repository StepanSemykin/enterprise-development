using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;
using TaxiCompany.WebApi.Dto;

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
    /// <param name="valueDto">Объект автомобиля, который нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные автомобиля некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public IActionResult Post([FromBody] CarDto valueDto)
    {
        var value = mapper.Map<Car>(valueDto);
        
        repository.Post(value);

        var driver = repositoryDrivers.Get(value.AssignedDriverId);
        if (driver == null) return BadRequest($"Driver with ID {value.AssignedDriverId} was not found.");
        driver.AssignedCarId = value.Id;

        return Ok();
    }

    /// <summary>
    /// Обновляет данные автомобиля по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля, данные которого нужно обновить.</param>
    /// <param name="valueDto">Объект автомобиля с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если автомобиль с указанным идентификатором не найден, возвращает статус 400 Bad Request.
    /// Если водитель с указанным идентификатором не найден, возвращает статус 400 Bad Request.
    /// Если данные автомобиля некорректны, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CarDto valueDto)
    {
        var value = mapper.Map<Car>(valueDto);

        var oldValue = repository.Get(id);
        if (oldValue == null) return BadRequest($"Car with ID {id} was not found.");
        if (oldValue.AssignedDriverId != value.AssignedDriverId)
        {
            var driver = repositoryDrivers.Get(oldValue.AssignedDriverId);
            if (driver == null) return BadRequest($"Driver with ID {oldValue.AssignedDriverId} was not found.");
            driver.AssignedCarId = 0;
            var newDriver = repositoryDrivers.Get(value.AssignedDriverId);
            if (newDriver == null) return BadRequest($"Driver with ID {oldValue.AssignedDriverId} was not found.");
            var car = repository.Get(newDriver.AssignedCarId);
            if (car == null) return BadRequest($"Car with ID {newDriver.AssignedCarId} was not found.");
            car.AssignedDriverId = 0;
            newDriver.AssignedCarId = id;
        };

        if (repository.Put(id, value)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Удаляет автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля, который нужно удалить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если удаление прошло успешно.
    /// Если автомобиль с указанным идентификатором не найден, возвращает статус 400 Bad Request.
    /// Если водитель с указанным идентификатором не найден, возвращает статус 400 Bad Request.
    /// Если данные автомобиля некорректны, возвращает статус 404 Not Found.
    /// </returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var car = repository.Get(id);
        if (car == null) return BadRequest($"Car with ID {id} was not found.");
        var driver = repositoryDrivers.Get(car.AssignedDriverId);
        if (driver == null) return BadRequest($"Driver with ID {car.AssignedDriverId} was not found.");
        driver.AssignedCarId = 0;

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

        var driverDto = mapper.Map<DriverDto>(driver);
        var carDto = mapper.Map<CarDto>(car);

        var driverCarInfo = new DriverCarInfoDTO
        {
            Driver = driverDto,
            Car = carDto
        };

        return Ok(driverCarInfo);
    }
}
