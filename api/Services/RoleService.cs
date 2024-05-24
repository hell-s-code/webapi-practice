using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.Services;

public class RoleService
{
    readonly dbContext _dbContext;
    public RoleService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Role> GetAllRoles()
    {
        return _dbContext.Roles.OrderBy(odb => odb.Name).ToList();
    }

    public Role GetRoleByName(string name)
    {
        return _dbContext.Roles.FirstOrDefault(e => e.Name == name);
    }

    public void AddRole(Role role)
    {
        if (_dbContext.Roles.Any(e => e.Name == role.Name))
        {
            throw new InvalidOperationException("ชื่อRoleนี้มีอยู่แล้ว");
        }
        _dbContext.Roles.Add(role);
        _dbContext.SaveChanges();
    }

    public void UpdateRole([FromBody] Role role)
    {
        if (_dbContext.Roles.FirstOrDefault(r => r.Name == role.Name) != null)
        {
            throw new InvalidOperationException("ชื่อRoleนี้มีอยู่แล้ว");

        }
        _dbContext.Attach(role).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void DeleteRoleByID(Guid id)
    {
        var x = _dbContext.Roles.FirstOrDefault(e => e.Id == id);
        _dbContext.Roles.Remove(x);
        _dbContext.SaveChanges();
    }

    

}
