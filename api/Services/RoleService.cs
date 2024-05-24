using api.Models;
using System.Data;
using System.Xml.Linq;

namespace api.Services;

public class RoleService
{
    readonly dbContext _dbContext;
    public RoleService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Role> GetRoles()
    {
        return _dbContext.Roles.OrderBy(role => role.Name).ToList();
    }public List<Role> GetRoleByName(string name)
    {
        return _dbContext.Roles.Where(role => role.Name == name).ToList();
    }
    public Role AddRole(Role role) {
        _dbContext.Roles.Add(role);
        _dbContext.SaveChangesAsync();
        return role;
    }

    public Role DeleteRole(Guid id)
    {
        Role role = _dbContext.Roles.Where(role => role.Id == id).FirstOrDefault();
        _dbContext.Roles.Remove(role);
        _dbContext.SaveChangesAsync();
        return role;
    }

    public Role UpdateRole(Role role)
    {
        Role roleToUpdate = _dbContext.Roles.Where(_role => _role.Id.ToString() == role.Name).FirstOrDefault();
        role.Name = "test";
        role.Active = true;
        role.Description = "test";
        role.CreatedDate = DateTime.Now;
        role.CreatedBy = "Program";
        _dbContext.Roles.Attach(role);
        _dbContext.SaveChangesAsync();
        return role;
    }

}
