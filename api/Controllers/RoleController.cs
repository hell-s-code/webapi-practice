using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        this._roleService = roleService;
    }

    [HttpGet("All")]
    public async Task<ActionResult> Get()
    {
        return Ok(this._roleService.GetRolesAll());
    }

    [HttpGet("Serch")]
    public async Task<ActionResult> Get([FromQuery] string name)
    {
        return Ok(this._roleService.SerchRole(name));
    }

    [HttpPost("Add")]
    public async Task<ActionResult> Post([FromBody] Role role)
    {
        string status = await this._roleService.AddRole(role);
        return Ok(status);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] Role role)
    {
        return Ok(this._roleService.UpdateRole(role));
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete([FromBody] Guid Id) 
    {
        return Ok(this._roleService.DeleteRole(Id));
    }

}
