namespace Domain.DTOs.RestaurantDTOs;

public class RestaurantProfitByDay
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TotalProfit { get; set; }
}