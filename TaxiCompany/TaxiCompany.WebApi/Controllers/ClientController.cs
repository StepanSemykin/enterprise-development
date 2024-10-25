using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;
using TaxiCompany.WebApi.DTO;

namespace TaxiCompany.WebApi.Controllers;

/// <summary>
/// Класс <c>Контроллер клиентов</c> для управления клиентами в API.
/// Предоставляет конечные точки для получения, добавления, изменения и удаления клиентов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController(IRepository<Client> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получает список всех клиентов.
    /// </summary>
    /// <returns>
    /// Возвращает результат операции, который содержит список клиентов.
    /// Если клиенты не найдены, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
    public IActionResult Get()
    {
        var clients = repository.Get();

        if (clients == null) return NotFound();

        return Ok(clients);
    }

    /// <summary>
    /// Получает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>
    /// Возвращает результат операции, который содержит объект клиента.
    /// Если клиент не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Client), 200)]
    public IActionResult Get(int id)
    {
        var client = repository.Get(id);

        if (client == null) return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// Добавляет нового клиента.
    /// </summary>
    /// <param name="value">Объект клиента, который нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные клиента некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public IActionResult Post([FromBody] ClientDTO valueDTO)
    {
        var value = mapper.Map<Client>(valueDTO);

        repository.Post(value);

        return Ok();
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента, данные которого нужно обновить.</param>
    /// <param name="value">Объект клиента с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если клиент с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ClientDTO valueDTO)
    {
        var value = mapper.Map<Client>(valueDTO);

        if (repository.Put(id, value)) return Ok();
        else return NotFound();
    }

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента, которого нужно удалить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если удаление прошло успешно.
    /// Если клиент с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.Delete(id)) return Ok();
        else return NotFound();
    }
}
