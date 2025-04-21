namespace Domain.DTOs.RestaurantDTOs;

public class RestaurantsWithOrderCount
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public decimal OrdersCount { get; set; }
}