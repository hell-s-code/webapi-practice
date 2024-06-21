using api.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using System.Text;

namespace api.Services;

public class UserService
{
    readonly dbContext _dbContext;
    public UserService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<User>> GetAllDapper()
    {
        StringBuilder sql = new StringBuilder();
        sql.AppendLine(@"
            select id, first_name AS firstname, last_name AS lastname, gender
            from public.user
        ");

        return await _dbContext.QueryAsync<User>(sql.ToString(), new { }) as List<User>;
    }
    public async Task<User> GetDapper(string? FirstName, string? LastName)
    {
        StringBuilder sql = new StringBuilder();
        sql.AppendLine(@"
            SELECT id, 
            first_name ""firstname"", 
            last_name ""lastname"", 
            gender 
            FROM public.user 
            WHERE 1 = 1
        ");
        if (!String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName))
        {
           sql.AppendLine("and LOWER(first_name) = LOWER(@fname) and LOWER(last_name) = LOWER(@lname)");
        }
        else if (!String.IsNullOrWhiteSpace(FirstName))
        {
            sql.AppendLine("and LOWER(first_name) = LOWER(@fname)");
        }
        else if (!String.IsNullOrWhiteSpace(LastName)) { 
            sql.AppendLine("and LOWER(last_name) = LOWER(@lname)");
        }
        return (User)await this._dbContext.QueryFirstOrDefaultAsync<User>(sql.ToString(), new { fname = FirstName, lname = LastName });
    }
    public async Task<User> PostUser(User user)
    {
        StringBuilder sql = new StringBuilder();
        sql.AppendLine(@"
            SELECT id, 
            first_name ""firstname"", 
            last_name ""lastname"", 
            gender 
            FROM public.user 
            WHERE first_name = @fname or last_name = @lname
        ");
        List<User> result = (List<User>)await this._dbContext.QueryAsync<User>(sql.ToString(), new { fname = user.FirstName, lname = user.LastName });

        if (result.Count > 0)
        {
            return user = null;
        }
        if (result.Count == 0)
        {
            sql = new StringBuilder();
            sql.AppendLine(@"
                insert into ""user"" (first_name,last_name,gender) 
                values  (@fname, 
                        @lname,
                        @gender)"
                );

            await this._dbContext.ExecuteAsync(sql.ToString(), new { fname = user.FirstName, lname = user.LastName, gender = user.Gender });
        }
        return user;
        
    }
    public async Task<User> PutUpdatee(User user)
    {
        StringBuilder sql = new StringBuilder();
        sql.AppendLine(@"
            SELECT id, 
            first_name ""firstname"", 
            last_name ""lastname"", 
            gender 
            FROM public.user 
            WHERE first_name = @fname or last_name = @lname
        ");
        List<User> result = (List<User>)await this._dbContext.QueryAsync<User>(sql.ToString(), new { fname = user.FirstName, lname = user.LastName });

        if (result.Count > 0)
        {
            return user = null;
        }
        if (result.Count == 0)
        {
            sql = new StringBuilder();
            sql.AppendLine(@"
                update ""user""
                set  First_Name = @fname, 
                     Last_Name = @lname
                WHERE id = @id"
                );

            await this._dbContext.ExecuteAsync(sql.ToString(), new { fname = user.FirstName, lname = user.LastName, gender = user.Gender ,id = user.Id});
        }
        return user;

    }
    public async Task<User> Delete(Guid id)
    {
        StringBuilder sql = new StringBuilder();
        sql.AppendLine(@"delete from public.user where id = @id");
        return await _dbContext.QueryFirstOrDefaultAsync<User>(sql.ToString(), new { Id = id });
    }
}
