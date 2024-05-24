using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
    public ActionResult Get()
    {
        return Ok(_roleService.GetAllRoles());
    }

    [HttpGet("{name}")]
    public ActionResult Get(string name)
    {
        return Ok(_roleService.GetRoleByName(name));
    }


    [HttpPost]
    public ActionResult Post([FromBody] Role role) 
    {
        try
        {
            _roleService.AddRole(role);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public ActionResult Put([FromBody] Role role)
    {

        try
        {
            _roleService.UpdateRole(role);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _roleService.DeleteRoleByID(id);
        return Ok();
    }

  


}
