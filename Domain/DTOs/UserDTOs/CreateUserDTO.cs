using Domain.Enums;

namespace Domain.DTOs.UserDTOs;

public class CreateUserDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public UserRole Role { get; set; }
}