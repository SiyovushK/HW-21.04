namespace Domain.Entities;

public class Menu
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public int PreparationTime { get; set; }
    public int Weight { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;
    
    public virtual Restaurant Restaurant { get; set; }
    public virtual List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}