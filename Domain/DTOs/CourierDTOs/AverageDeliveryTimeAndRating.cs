namespace Domain.DTOs.CourierDTOs;

public class AverageDeliveryTimeAndRating
{
    public int CourierId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double AverageDeliveryTime { get; set; }
    public decimal AverageRating { get; set; }
}