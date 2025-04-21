using Domain.DTOs.UserDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<GetUserDTO>> CreateUserAsync(CreateUserDTO createUser);
    Task<Response<GetUserDTO>> UpdateUserAsync(int userId, UpdateUserDTO updateUser);
    Task<Response<string>> DeleteUserAsync(int userId);
    Task<Response<GetUserDTO>> GetUserByIdAsync(int userID);
    Task<Response<List<GetUserDTO>>> GetAllUsersAsync();
    Task<Response<List<UserAndOrderCountDTO>>> UsersWithOrdersCount();
}