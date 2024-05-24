using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _userService.GetAllUsers());
    }

    [HttpGet("{name}")]
    public async Task<ActionResult> Get(string name)
    {
        return Ok(await _userService.GetUserByName(name));
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        try
        {
            await _userService.AddUser(user);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        
        try
        {
            await _userService.UpdateUser(user);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUser(id);
        return Ok();
    }

}
