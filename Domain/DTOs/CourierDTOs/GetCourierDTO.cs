using Domain.Enums;

namespace Domain.DTOs.CourierDTOs;

public class GetCourierDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public CourierStatus Status { get; set; }
    public string CurrentLocation { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public TransportType TransportType { get; set; }
}