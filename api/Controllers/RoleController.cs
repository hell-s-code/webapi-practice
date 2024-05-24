using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(_roleService.GetRoles());
    }

    [HttpGet("{name}")]
    public async Task<ActionResult> Get(string name)
    {
        List<Role> result = _roleService.GetRoleByName(name);
        if (result.Count != 0)
        {
            return Ok(result);
        } else
        {
            return Ok("Data not found");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Role role)
    {
        List<Role> checkdup = _roleService.GetRoleByName(role.Name);
        if (!checkdup.Any(result => result.Name == role.Name))
        {
            return Ok(_roleService.AddRole(role));
        }
        else if (checkdup.Any(result => result.Name == role.Name))
        {
            return Ok("Duplicated Name");
        } else
        {
            return Ok("Error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRole(Guid id)
    {
        return Ok(_roleService.DeleteRole(id));
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] Role role)
    {
        return Ok(_roleService.UpdateRole(role));
    }

}
