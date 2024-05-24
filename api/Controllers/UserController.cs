using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
    public async Task<IActionResult> Get()
    {
        return Ok(await _userService.GetUser());
    }

    [HttpGet("{FirstOrLastName}")]
    public async Task<IActionResult> GetUserByName(string FirstOrLastName)
    {
        return Ok(await _userService.GetUserByName(FirstOrLastName));
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        List<User> checkdup = await _userService.GetUser();
        if(!checkdup.Any(result=>result.FirstName == user.FirstName || result.LastName == user.LastName)){
            await _userService.AddUser(user);
            return Ok(user);
        }
        else
        {
            return Ok("Duplicated");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        List<User> checkdup = await _userService.GetUser();
        if(!checkdup.Any(result=>result.FirstName == user.FirstName || result.LastName == user.LastName)){
            await _userService.UpdateUser(user);
            return Ok(user);
        }
        else
        {
            return Ok("Duplicated");
        }
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteUser(User user)
    {
        List<User> checkdup = await _userService.GetUser();
        if (!checkdup.Any(result => result.Id == user.Id)) { 
            await _userService.DeleteUser(user);
            return Ok(user);
        }
        else
        {
            return Ok("Not Found");
        }
    }
}
