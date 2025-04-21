using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public UserRole Role { get; set; }

    public virtual List<Order> Orders { get; set; } = new List<Order>();
    public virtual Courier Courier { get; set; }
}