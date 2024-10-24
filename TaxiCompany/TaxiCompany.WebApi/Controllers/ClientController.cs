using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;

namespace TaxiCompany.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IRepository<Client> repository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
    public IActionResult Get()
    {
        var clients = repository.Get();

        if (clients == null) return NotFound();

        return Ok(clients);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Client), 200)]
    public IActionResult Get(int id)
    {
        var client = repository.Get(id);

        if (client == null) return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Client value)
    {
        repository.Post(value);

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Client value)
    {
        if (repository.Put(id, value)) return Ok();
        else return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.Delete(id)) return Ok();
        else return NotFound();
    }
}
