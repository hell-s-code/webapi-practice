using api.Models;
using Microsoft.VisualBasic;
using System.Collections;

namespace api.Services;

public class UserService
{
    readonly dbContext _dbContext;
    public UserService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetUser()
    {
        List<User> user = (List<User>)await _dbContext.QueryAsync<User>(@"select id as ""Id"", first_name as ""FirstName"" , last_name as ""LastName"" , gender as ""Gender"" from public.user");
        return user;
    }

    public async Task<List<User>> GetUserByName(string name)
    {
        string sql = @"select id as ""Id"", first_name as ""FirstName"" , last_name as ""LastName"" , gender as ""Gender"" from public.user where first_name = @firstname or last_name = @lastname";
        List<User> _user = (List<User>)await _dbContext.QueryAsync<User>(sql, new
        {
            firstname = name,
            lastname = name,
        });
        return _user;
    }

    public async Task<User> AddUser(User user)
    {
        string sql = "insert into public.user (first_name,last_name,gender) values (@firstname,@lastname,@gender)";
        User _user = await _dbContext.QueryFirstOrDefaultAsync<User>(sql, new
        {
            firstname = user.FirstName.ToString(),
            lastname = user.LastName.ToString(),
            gender = user.Gender,
        });
        return _user;
    }

    public async Task<User> UpdateUser(User user)
    {
        string sql = @"update public.user set first_name = @firstname,last_name = @lastname,gender = @gender where id = @id";
        User _user = await _dbContext.QueryFirstOrDefaultAsync<User>(sql, new {
            id = user.Id,
            firstname = user.FirstName.ToString(),
            lastname = user.LastName.ToString(),
            gender = user.Gender,
        });

        return _user;
    }

    public async Task<User> DeleteUser(User user)
    {
        return await _dbContext.QueryFirstOrDefaultAsync<User>(@"delete from public.user where id = @id",new {id = user.Id});
    }
}
