using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;

namespace TaxiCompany.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController(IRepository<Car> repository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Car>), 200)]
    public IActionResult Get()
    {
        var cars = repository.Get();

        if (cars == null) return NotFound();

        return Ok(cars);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Car), 200)]
    public IActionResult Get(int id)
    {
        var car = repository.Get(id);

        if (car == null) return NotFound();

        return Ok(car);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Car value)
    {
        repository.Post(value);

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Car value)
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
