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
}
