using Domain.Enums;

namespace Domain.DTOs.UserDTOs;

public class UsersOverFiveOrders
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int OrdersCount { get; set; }
}