using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services;
using PDMDocumentSystem.Services.Interfaces;

namespace PDMDocumentSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UserController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [Authorize]
    [HttpGet("authorize")]
    public async Task<ActionResult<User>> Authorize()
    {
        var user = this.User;
        var userEmail = user.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type == "preferred_username")
            .Value;
        var newUser = await _authService.AuthenticateAsync(userEmail);
        
        return Ok(newUser);
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