namespace Domain.DTOs.MenuDTOs;

public class MenuCategoryAndPriceDTO
{
    public string Category { get; set; } = string.Empty;
    public decimal AveragePrice { get; set; }
}