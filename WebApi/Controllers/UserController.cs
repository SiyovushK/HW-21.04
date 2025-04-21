using Domain.DTOs.UserDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetUserDTO>> CreateUser(CreateUserDTO createUser)
    {
        return await userService.CreateUserAsync(createUser);
    }

    [HttpPut]
    public async Task<Response<GetUserDTO>> UpdateUser(int userId, UpdateUserDTO updateUser)
    {
        return await userService.UpdateUserAsync(userId, updateUser);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteUser(int userId)
    {
        return await userService.DeleteUserAsync(userId);
    }

    [HttpGet]
    public async Task<Response<List<GetUserDTO>>> GetAllUsers()
    {
        return await userService.GetAllUsersAsync();
    }

    [HttpGet("UsersWithOrdersCount")]
    public async Task<Response<List<UserAndOrderCountDTO>>> UsersWithOrdersCount()
    {
        return await userService.UsersWithOrdersCount();
    }

    [HttpGet("id")]
    public async Task<Response<GetUserDTO>> GetUserById(int userId)
    {
        return await userService.GetUserByIdAsync(userId);
    }
}