using api.Models;

namespace api.Services;

public class UserService
{
    readonly dbContext _dbContext;
    public UserService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
