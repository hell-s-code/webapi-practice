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
    [HttpGet("getRoleDapper")]

    public async Task<ActionResult> GetDapper()
    {

        return Ok(await _userService.GetAllDapper());
    }
    [HttpGet("Name")]
    public async Task<ActionResult> GetDapper(string FirstName = "", string LastName = "")
    {
        return Ok(await _userService.GetDapper( FirstName,  LastName));
    }
    [HttpPost("Create")]
    public async Task<ActionResult> PostUser([FromBody] User user)
    {
        return Ok(await _userService.PostUser(user)); 
    }
    [HttpPut("Update")]
    public async Task<ActionResult> PutUpdatee([FromBody] User user)
    {
        return Ok(await _userService.PutUpdatee(user));
    }
    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete([FromBody] Guid id)
    {
        return Ok(await _userService.Delete(id));
    }
}
