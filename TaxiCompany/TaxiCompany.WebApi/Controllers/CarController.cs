using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain.Entities;
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
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Car>), 200)]
    public async Task<IActionResult> Get()
    {
        var cars = await repository.GetAsync();

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
    public async Task<IActionResult> Get(int id)
    {
        var car = await repository.GetAsync(id);

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
    public async Task<IActionResult> Post([FromBody] CarDto valueDto)
    {
        var value = mapper.Map<Car>(valueDto);

        var driver = await repositoryDrivers.GetAsync(value.AssignedDriverId);
        if (driver == null) return BadRequest($"Driver with ID {value.AssignedDriverId} was not found.");

        await repository.PostAsync(value);

        driver.AssignedCarId = value.Id;
        await repositoryDrivers.PutAsync(driver.Id, driver);

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
    public async Task<IActionResult> Put(int id, [FromBody] CarDto valueDto)
    {
        var value = mapper.Map<Car>(valueDto);

        var oldValue = await repository.GetAsync(id);
        if (oldValue == null) return BadRequest($"Car with ID {id} was not found.");
        if (oldValue.AssignedDriverId != value.AssignedDriverId)
        {
            var driver = await repositoryDrivers.GetAsync(oldValue.AssignedDriverId);
            if (driver == null) return BadRequest($"Driver with ID {oldValue.AssignedDriverId} was not found.");
            driver.AssignedCarId = 0;
            await repositoryDrivers.PutAsync(driver.Id, driver);
            var newDriver = await repositoryDrivers.GetAsync(value.AssignedDriverId);
            if (newDriver == null) return BadRequest($"Driver with ID {oldValue.AssignedDriverId} was not found.");
            var car = await repository.GetAsync(newDriver.AssignedCarId);
            if (car == null) return BadRequest($"Car with ID {newDriver.AssignedCarId} was not found.");
            car.AssignedDriverId = 0;
            await repository.PutAsync(car.Id, car);
            newDriver.AssignedCarId = id;
            await repositoryDrivers.PutAsync(newDriver.Id, newDriver);
        };

        if (await repository.PutAsync(id, value)) return Ok();
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
    public async Task<IActionResult> Delete(int id)
    {
        var car = await repository.GetAsync(id);
        if (car == null) return BadRequest($"Car with ID {id} was not found.");
        var driver = await repositoryDrivers.GetAsync(car.AssignedDriverId);
        if (driver == null) return BadRequest($"Driver with ID {car.AssignedDriverId} was not found.");
        driver.AssignedCarId = 0;
        await repositoryDrivers.PutAsync(driver.Id, driver);

        if (await repository.DeleteAsync(id)) return Ok();
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
    [ProducesResponseType(typeof(DriverCarInfoDto), 200)]
    public async Task<IActionResult> GetDriverAndCar(int driverId)
    {
        var driver = await repositoryDrivers.GetAsync(driverId);
        if (driver == null) return NotFound($"Driver with ID {driverId} was not found.");
        var car = await repository.GetAsync(driver.AssignedCarId);
        if (car == null) return NotFound($"Car assigned to driver with ID {driverId} was not found.");

        var driverDto = mapper.Map<DriverDto>(driver);
        var carDto = mapper.Map<CarDto>(car);

        var driverCarInfo = new DriverCarInfoDto
        {
            Driver = driverDto,
            Car = carDto
        };

        return Ok(driverCarInfo);
    }
}
