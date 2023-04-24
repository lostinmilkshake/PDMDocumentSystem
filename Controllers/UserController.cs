using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PDMDocumentSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IGenericRepository<User> _repository;

    public UserController(IGenericRepository<User> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _repository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
            return NotFound();
        return new ObjectResult(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        await _repository.CreateAsync(user);
        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult<User>> Put(User user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        if (!_repository.GetAllAsync().Result.Any(x => x.Id == user.Id))
        {
            return NotFound();
        }

        await _repository.UpdateAsync(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete(Guid id)
    {
        var user = _repository.GetAllAsync().Result.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(user);
        return Ok(user);
    }   
}