namespace Domain.DTOs.UserDTOs;

public class FavouriteUserCategory
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}