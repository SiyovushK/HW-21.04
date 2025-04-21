namespace Domain.DTOs.OrderDetailDTOs;

public class CreateOrderDetailDTO
{
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string SpecialInstructions { get; set; } = string.Empty;
}