using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet()]
    public async Task<ActionResult> Get()
    {
        return Ok(_roleService.GetAll());
    }
    [HttpGet("Name")]
    public async Task<ActionResult> Get(string Name)
    {
        return Ok(_roleService.GetName(Name));
    }
    [HttpPost("Create")]
    public async Task<ActionResult> Post([FromBody] Role role)
    {
        return Ok(_roleService.Post(role));
    }
    [HttpPut("Update")]
    public async Task<ActionResult> Put([FromBody] Role role)
    {
        return Ok(_roleService.Put(role));
       
    }
    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
        return Ok(_roleService.Delete(id));
    }

}

