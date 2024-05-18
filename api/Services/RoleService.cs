using api.Models;

namespace api.Services;

public class RoleService
{
    readonly dbContext _dbContext;
    public RoleService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Role>> GetRoles()
    {
        return (List<Role>)await _dbContext.QueryAsync<Role>("select * from role");
    }
}
