using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain.Entities;
using TaxiCompany.Domain.Repositories;
using TaxiCompany.WebApi.Dto;

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
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
    public async Task<IActionResult> Get()
    {
        var clients = await repository.GetAsync();

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
    public async Task<IActionResult> Get(int id)
    {
        var client = await repository.GetAsync(id);

        if (client == null) return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// Добавляет нового клиента.
    /// </summary>
    /// <param name="valueDto">Объект клиента, который нужно добавить.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если добавление прошло успешно.
    /// Если данные клиента некорректны, возвращает статус 400 Bad Request.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClientDto valueDto)
    {
        var value = mapper.Map<Client>(valueDto);

        await repository.PostAsync(value);

        return Ok();
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента, данные которого нужно обновить.</param>
    /// <param name="valueDto">Объект клиента с новыми данными.</param>
    /// <returns>
    /// Возвращает статус 200 OK, если обновление прошло успешно.
    /// Если клиент с указанным идентификатором не найден, возвращает статус 404 Not Found.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ClientDto valueDto)
    {
        var value = mapper.Map<Client>(valueDto);

        if (await repository.PutAsync(id, value)) return Ok();
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
    public async Task<IActionResult> Delete(int id)
    {
        if (await repository.DeleteAsync(id)) return Ok();
        else return NotFound();
    }
}
