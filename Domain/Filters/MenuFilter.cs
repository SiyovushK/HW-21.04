namespace Domain.Filters;

public class MenuFilter
{
    public string? Name { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public bool? IsAvailable { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}