namespace Domain.DTOs.UserDTOs;

public class UserAndOrderCountDTO
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int OrderCount { get; set; }
}