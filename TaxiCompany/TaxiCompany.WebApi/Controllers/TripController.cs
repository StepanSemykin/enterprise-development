using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;
using TaxiCompany.WebApi.DTO;

namespace TaxiCompany.WebApi.Controllers;

/// <summary>
/// Класс <c>Контроллер поездок</c> для управления поездками в API.
/// Предоставляет конечные точки для получения, добавления, изменения и удаления поездок.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TripController(IRepository<Trip> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получает список всех поездок.
    /// </summary>
    /// <returns>
    /// Возвращает результат операции, который содержит список поездок.
    /// Если поездки не найдены, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Trip>), 200)]
    public IActionResult Get()
    {
        var trips = repository.Get();

        if (trips == null) return NotFound();

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
    public IActionResult Get(int id)
    {
        var trip = repository.Get(id);

        if (trip == null) return NotFound();

        return Ok(trip);
    }

    /// <summary>
    /// Добавляет новую поездку.
    /// </summary>
    /// <param name="value">Объект поездки, которую нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные поездки некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public IActionResult Post([FromBody] TripDTO valueDTO)
    {
        var value = mapper.Map<Trip>(valueDTO); 

        repository.Post(value);

        return Ok();
    }

    /// <summary>
    /// Обновляет данные поездки по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор поездки, данные которой нужно обновить.</param>
    /// <param name="value">Объект поездки с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если поездка с указанным идентификатором не найдена, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TripDTO valueDTO)
    {
        var value = mapper.Map<Trip>(valueDTO);

        if (repository.Put(id, value)) return Ok();
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
    public IActionResult Delete(int id)
    {
        if (repository.Delete(id)) return Ok();
        else return NotFound();
    }
}
