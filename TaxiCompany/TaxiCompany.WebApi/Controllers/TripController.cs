using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;

namespace TaxiCompany.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripController(IRepository<Trip> repository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Trip>), 200)]
    public IActionResult Get()
    {
        var trips = repository.Get();

        if (trips == null) return NotFound();

        return Ok(trips);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Trip), 200)]
    public IActionResult Get(int id)
    {
        var trip = repository.Get(id);

        if (trip == null) return NotFound();

        return Ok(trip);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Trip value)
    {
        repository.Post(value);

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Trip value)
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
