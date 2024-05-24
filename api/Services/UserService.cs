using api.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api.Services;

public class UserService
{
    readonly dbContext _dbContext;
    public UserService(dbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAllUsers()
    {
        List<User>users = (await _dbContext.QueryAsync<User>(@"select id as ""Id"", first_name as ""FirstName"", last_name as ""LastName"", gender as ""Gender""  from public.user ORDER BY first_name, last_name")).ToList();
        return users;
    }

    public async Task<List<User>> GetUserByName(string name)
    {
         List<User> user = (await _dbContext.QueryAsync<User>
         (@"select id as ""Id"", first_name as ""FirstName"", last_name as ""LastName"", gender as ""Gender"" from public.user WHERE first_name = @Name or last_name = @Name limit 1 ", new { Name = name })).ToList();
        return user;
    }

    public async Task<User> AddUser(User user)
    {

        int count = await _dbContext.QueryFirstOrDefaultAsync<int>(
        @"SELECT COUNT(id) FROM public.user WHERE first_name = @FirstName AND last_name = @LastName",
        new { user.FirstName, user.LastName });

        if (count > 0)
            throw new InvalidOperationException("ชื่อ-นามสกุลนี้มีผู้ใช้แล้ว");

        await _dbContext.ExecuteAsync(@"INSERT INTO public.user (id, first_name, last_name, gender) VALUES (@Id, @FirstName, @LastName, @Gender)", user);
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {

        int count = await _dbContext.QueryFirstOrDefaultAsync<int>(
        @"SELECT COUNT(id) FROM public.user WHERE first_name = @FirstName AND last_name = @LastName",
        new { user.FirstName, user.LastName });

        if (count > 0)
            throw new InvalidOperationException("ชื่อ-นามสกุลนี้มีผู้ใช้แล้ว");

        await _dbContext.ExecuteAsync(@"UPDATE public.user SET first_name = @FirstName, last_name = @LastName, gender = @Gender WHERE id = @Id ", user);
        return user;
    }

    public async Task<Guid> DeleteUser(Guid userId)
    {
        await _dbContext.ExecuteAsync(@"DELETE FROM public.user WHERE id = @UserId", new { UserId = userId });
        return userId;
    }





}
