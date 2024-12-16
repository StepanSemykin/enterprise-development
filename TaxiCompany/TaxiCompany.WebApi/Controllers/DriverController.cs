using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiCompany.Domain.Entities;
using TaxiCompany.Domain.Repositories;
using TaxiCompany.WebApi.Dto;

namespace TaxiCompany.WebApi.Controllers;

/// <summary>
/// Класс <c>Контроллер водителей</c> для управления водителями в API.
/// Предоставляет конечные точки для получения, добавления, изменения и удаления водителей.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DriverController(IRepository<Driver> repository, IRepository<Car> repositoryCars, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получает список всех водителей.
    /// </summary>
    /// <returns>
    /// Возвращает результат операции, который содержит список водителей.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Driver>), 200)]
    public async Task<IActionResult> Get()
    {
        var drivers = await repository.GetAsync();

        return Ok(drivers);
    }

    /// <summary>
    /// Получает водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор водителя.</param>
    /// <returns>
    /// Возвращает результат операции, который содержит объект водителя.
    /// Если водитель не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Driver), 200)]
    public async Task<IActionResult> Get(int id)
    {
        var driver = await repository.GetAsync(id);

        if (driver == null) return NotFound();

        return Ok(driver);
    }

    /// <summary>
    /// Получает водителей без назначенных авто.
    /// </summary>
    /// <returns>
    /// Возвращает результат операции, который содержит список водителей.
    /// </returns>
    [HttpGet("free")]
    [ProducesResponseType(typeof(IEnumerable<Driver>), 200)]
    public async Task<IActionResult> GetFreeDrivers()
    {
        var drivers = await repository.GetAsync();

        var assignedDriverIds = (await repositoryCars.GetAsync())
            .Select(car => car.AssignedDriverId)
            .ToHashSet();

        var freeDrivers = drivers.Where(driver => !assignedDriverIds.Contains(driver.Id));

        return Ok(freeDrivers);
    }

    /// <summary>
    /// Добавляет нового водителя.
    /// </summary>
    /// <param name="valueDto">Объект водителя, который нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные водителя некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DriverDto valueDto)
    {
        var value = mapper.Map<Driver>(valueDto);

        await repository.PostAsync(value);

        return Ok();
    }

    /// <summary>
    /// Обновляет данные водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор водителя, данные которого нужно обновить.</param>
    /// <param name="valueDto">Объект водителя с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если водитель с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] DriverDto valueDto)
    {
        var value = mapper.Map<Driver>(valueDto);

        if (await repository.PutAsync(id, value)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Удаляет водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор водителя, которого нужно удалить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если удаление прошло успешно.
    /// Если автомобиль с указанным идентификатором не найден, возвращает статус 400 Bad Request.
    /// Если водитель с указанным идентификатором не найден, возвращает статус 400 Bad Request.
    /// Если данные водителя некорректны, возвращает статус 404 Not Found.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var driver = await repository.GetAsync(id);
        if (driver == null) return NotFound($"Driver with ID {id} not found.");
        var car = await repositoryCars.GetAsync(driver.AssignedCarId);
        if (car != null)
        {
            car.AssignedDriverId = 0;
            await repositoryCars.PutAsync(car.Id, car);
        }

        if (await repository.DeleteAsync(id)) return Ok();
        else return NotFound();
    }
}
