using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;

namespace TaxiCompany.WebApi.Controllers;

/// <summary>
/// Класс <c>Контроллер водителей</c> для управления водителями в API.
/// Предоставляет конечные точки для получения, добавления, изменения и удаления водителей.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DriverController(IRepository<Driver> repository) : ControllerBase
{
    /// <summary>
    /// Получает список всех водителей.
    /// </summary>
    /// <returns>
    /// Возвращает результат операции, который содержит список водителей.
    /// Если водители не найдены, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Driver>), 200)]
    public IActionResult Get()
    {
        var drivers = repository.Get();

        if (drivers == null) return NotFound();

        return Ok(drivers);
    }

    /// <summary>
    /// Получает водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор водителя.</param>
    /// <returns>
    /// Возвращает результат операции, который содержит объект водителя.
    /// Если водитель не найден, возвращает статус 404 Not Found.
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Driver), 200)]
    public IActionResult Get(int id)
    {
        var driver = repository.Get(id);

        if (driver == null) return NotFound();
        
        return Ok(driver);
    }

    /// <summary>
    /// Добавляет нового водителя.
    /// </summary>
    /// <param name="value">Объект водителя, который нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные водителя некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public IActionResult Post([FromBody] Driver value)
    {
        repository.Post(value);

        return Ok();
    }

    /// <summary>
    /// Обновляет данные водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор водителя, данные которого нужно обновить.</param>
    /// <param name="value">Объект водителя с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если водитель с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Driver value)
    {
        if(repository.Put(id, value)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Удаляет водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор водителя, которого нужно удалить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если удаление прошло успешно.
    /// Если водитель с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.Delete(id)) return Ok();
        else return NotFound();
    }
}
