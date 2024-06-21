using System.Reflection.Metadata.Ecma335;
using System.Text;
using api.Models;

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
        return (List<User>)await _dbContext.QueryAsync<User>("SELECT id, first_name AS firstname, last_name AS lastname, gender FROM public.user ORDER BY UPPER(first_name), UPPER(last_name)");
    }

    public async Task<User> SearchUser(string FirstName, string LastName)
    {
        return (User) await this._dbContext.QueryFirstOrDefaultAsync<User>("SELECT id, first_name AS firstname, last_name AS lastname, gender FROM public.user WHERE UPPER(first_name) = UPPER(@fname) or UPPER(last_name) = UPPER(@lname)", new { fname = FirstName, lname = LastName });
    }

    public async Task<string> AddUser(User user)
    {
        string name = (string)await this._dbContext.ExecuteScalarAsync<string>("SELECT first_name AS name FROM public.user WHERE UPPER(first_name) = UPPER(@fname) AND UPPER(last_name) = UPPER(@lname)", new { fname = user.FirstName, lname = user.LastName });
        if (name == null)
        {
            if ((await this._dbContext.ExecuteAsync(@"INSERT INTO public.user(id, first_name, last_name, gender) VALUES(@id, @fname, @lname, @gender)", new { id = Guid.NewGuid(), fname = user.FirstName, lname = user.LastName, gender = user.Gender })) != 0)
            {
                return "Add success";
            }
            else
            {
                return "Add fail";
            }
        }
        else
        {
            return "Add fail duplicate";
        }
    }

    public async Task<string> UpdateUser(User user)
    {
        User isDuplicateName = (User)await this._dbContext.QueryFirstOrDefaultAsync<User>("SELECT id FROM public.user WHERE UPPER(first_name) = UPPER(@fname) and UPPER(last_name) = UPPER(@lname) and id <> @id", new { fname = user.FirstName, lname = user.LastName, id = user.Id });
        if (isDuplicateName == null)
        {
            User selectUpdate = (User)await this._dbContext.QueryFirstOrDefaultAsync<User>("SELECT id, first_name AS firstname, last_name AS lastname, gender FROM public.user WHERE id = @id", new { id = user.Id });
            if (selectUpdate != null)
            {
                int executed = await this._dbContext.ExecuteAsync(@"UPDATE public.user SET first_name = @fname, last_name = @lname, gender = @gender WHERE id = @id", new {id = user.Id, fname = user.FirstName, lname = user.LastName, gender = user.Gender });
                if (executed != 0)
                {
                    return ("Update success");
                }
                else
                {
                    return ("Update fail");
                }
            }
            else
            {
                return ("Update fail not found");
            }
            
        }
        else
        {
            return ("Update fail duplicate");
        }
    }

    public async Task<string> DeleteUser(Guid id)
    {
        User user = (User)await this._dbContext.QueryFirstOrDefaultAsync<User>("SELECT id, first_name AS firstname, last_name AS lastname, gender FROM public.user WHERE id = @id", new { id = id });
        await this._dbContext.ExecuteAsync(@"DELETE FROM public.user WHERE id = @id", new { id = user.Id });
        return "Success";
    }


}
