using System.Net;
using AutoMapper;
using Domain.DTOs.UserDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context, IMapper mapper) : IUserService
{
    public async Task<Response<GetUserDTO>> CreateUserAsync(CreateUserDTO createUser)
    {
        var user = mapper.Map<User>(createUser);

        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        var getUserDto = mapper.Map<GetUserDTO>(user);

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.InternalServerError, "User couldn't be created")
            : new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<GetUserDTO>> UpdateUserAsync(int userId, UpdateUserDTO updateUser)
    {
        var user = await context.Users.FindAsync(userId);
        if(user == null)
            return new Response<GetUserDTO>(HttpStatusCode.NotFound, "User not found");

        mapper.Map(updateUser, user);

        var result = await context.SaveChangesAsync();

        var getUserDto = mapper.Map<GetUserDTO>(user);

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.InternalServerError, "User couldn't be updated")
            : new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<string>> DeleteUserAsync(int userId)
    {
        var user = await context.Users.FindAsync(userId);
        if(user == null)
            return new Response<string>(HttpStatusCode.NotFound, "User not found");

        context.Users.Remove(user);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "User couldn't be deleted")
            : new Response<string>("User deleted successfully");
    }

    public async Task<Response<GetUserDTO>> GetUserByIdAsync(int userID)
    {
        var user = await context.Users.FindAsync(userID);
        if(user == null)
            return new Response<GetUserDTO>(HttpStatusCode.NotFound, "User not found");

        var getUserDto = mapper.Map<GetUserDTO>(user);

        return new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<List<GetUserDTO>>> GetAllUsersAsync()
    {
        var users = await context.Users.ToListAsync();
        if(users.Count == 0)
            return new Response<List<GetUserDTO>>(HttpStatusCode.NotFound, "No users found");

        var getUsersDto = mapper.Map<List<GetUserDTO>>(users);

        return new Response<List<GetUserDTO>>(getUsersDto);
    }

    //Task5
    public async Task<Response<List<UserAndOrderCountDTO>>> UsersWithOrdersCount()
    {
        var users = await context.Users
            .Select(u => new UserAndOrderCountDTO
            {
                UserId = u.Id,
                Name = u.Name,
                Phone = u.Phone,
                OrderCount = u.Orders.Count()
            })
            .ToListAsync();

        return new Response<List<UserAndOrderCountDTO>>(users);
    }
}