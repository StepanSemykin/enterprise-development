using Microsoft.AspNetCore.Mvc;
using TaxiCompany.Domain;
using TaxiCompany.Domain.Repositories;

namespace TaxiCompany.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController(IRepository<Driver> repository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Driver>), 200)]
    public IActionResult Get()
    {
        var drivers = repository.Get();

        if (drivers == null) return NotFound();

        return Ok(drivers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Driver), 200)]
    public IActionResult Get(int id)
    {
        var driver = repository.Get(id);

        if (driver == null) return NotFound();
        
        return Ok(driver);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Driver value)
    {
        repository.Post(value);

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Driver value)
    {
        if(repository.Put(id, value)) return Ok();
        else return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.Delete(id)) return Ok();
        else return NotFound();
    }
}
