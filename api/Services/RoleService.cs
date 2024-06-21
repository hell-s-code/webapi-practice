using System.Xml.Linq;
using System;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace api.Services;

public class RoleService
{
    readonly dbContext _dbContext;
    public RoleService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Role> GetRolesAll()
    {
        return this._dbContext.Roles.OrderBy(res => res.Name).ToList();
    }

    public Role SerchRole(string name)
    {
        return this._dbContext.Roles.Where(res => res.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
    }

    public bool CheckDuplicateName(string name)
    {
        return this._dbContext.Roles.Any(res => res.Name.ToUpper() == name.ToUpper());
    }

    public async Task<string> AddRole(Role role) 
    {
        if (this.CheckDuplicateName(role.Name))
        {
            return "Add fail";
        }
        else
        {
            role.Id = Guid.NewGuid();
            role.CreatedDate = DateTime.Now;
            role.UpdatedDate = DateTime.Now;
            this._dbContext.Roles.Add(role);
            await this._dbContext.SaveChangesAsync();
            return "Add Success";
        }
    }

    public string UpdateRole(Role role)
    {
        bool isData = this._dbContext.Roles.Any(res => res.Id == role.Id) ? true : false;
        if (isData)
        {
            Role updateRole = this._dbContext.Roles.Where(res => res.Id == role.Id).FirstOrDefault();
            updateRole.Name = role.Name;
            updateRole.Active = role.Active;
            updateRole.Description = role.Description;
            updateRole.UpdatedBy = role.UpdatedBy;
            updateRole.UpdatedDate = DateTime.Now;
            updateRole.UpdatedProgram = role.UpdatedProgram;
            updateRole.Code = role.Code;

            if (this._dbContext.Roles.Any(res => res.Name == role.Name))
            {
                return "Duplicate Name";
            }
            else
            {
                this._dbContext.Attach(updateRole);

                this._dbContext.SaveChangesAsync();
                return "Save change";
            }
        }
        else
        {
            return "Not found";
        }
    }

    public async Task<string> DeleteRole(Guid Id)
    {
        Role role = this._dbContext.Roles.Where(res => res.Id == Id).FirstOrDefault() ?? new();

        this._dbContext.Remove(role);
        await this._dbContext.SaveChangesAsync();
        return "Success";

    }

}
