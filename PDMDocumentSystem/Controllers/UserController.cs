using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services.Interfaces;

namespace PDMDocumentSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _userService.GetAllUsersAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return new ObjectResult(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        await _userService.CreateUserAsync(user);
        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult<User>> Put(User user)
    {
        await _userService.UpdateUserAsync(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        await _userService.DeleteUserAsync(user);
        return Ok(user);
    }   
}