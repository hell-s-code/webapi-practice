using api.Models;

namespace api.Services;

public class RoleService
{
    readonly dbContext _dbContext;
    public RoleService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
