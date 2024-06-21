using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Collections.Generic;

namespace api.Services;

public class RoleService
{
    readonly dbContext _dbContext;
    public RoleService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Role> GetAll()
    {
        return _dbContext.Roles.ToList();
    }
    public Role GetName(string Name)
    {
        return _dbContext.Roles.Where(w => w.Name == Name).FirstOrDefault();
    }
    public string Post(Role role)
    {
        Role exName = _dbContext.Roles.Where(w => w.Name == role.Name).FirstOrDefault();
        string validate = "";
        if (exName == null)
        {
            this._dbContext.Roles.Add(role);
            this._dbContext.SaveChangesAsync();
            validate = "success";
        }
        else
        {
            validate = "fail";
        }
        return validate;
    }
    public string Put(Role role) 
    {
            Role existingRole = _dbContext.Set<Role>().Where(w => w.Id == role.Id).FirstOrDefault();
            List<Role> roles = _dbContext.Set<Role>().Where(w => w.Name == role.Name).ToList();
            if (roles.Count > 1)
            {
                return "duplicate_name";
            }
            if (existingRole == null)
            {
                return "not_found";
            }
            existingRole.Name = role.Name;
            existingRole.Description = role.Description;
            existingRole.Active = role.Active;
           _dbContext.Set<Role>().Attach(existingRole);

        this. _dbContext.SaveChangesAsync();

        return "success";
    }
    public string Delete(Guid Id)
    {
        Role RemoveRole = _dbContext.Set<Role>().Where(w => w.Id == Id).FirstOrDefault();
        _dbContext.Set<Role>().Remove(RemoveRole);
        this._dbContext.SaveChangesAsync();
        return "Remove success";
    }

}
