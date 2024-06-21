using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("All")]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await this._userService.GetUser());
    }

    [HttpGet("Search")]
    public async Task<ActionResult> SearchByName([FromQuery] string FirstName = "", string LastName = "")
    {
        return Ok(await this._userService.SearchUser(FirstName, LastName));
    }

    [HttpPost("Add")]
    public async Task<ActionResult> AddUser([FromBody] User user)
    {
        return Ok(await this._userService.AddUser(user));
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateUser([FromBody] User user)
    {
        return Ok(await this._userService.UpdateUser(user));
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteUser([FromBody] Guid id)
    {
        return Ok(await this._userService.DeleteUser(id));
    }
}
