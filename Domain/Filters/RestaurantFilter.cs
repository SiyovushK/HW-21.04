namespace Domain.Filters;

public class RestaurantFilter
{
    public string? Name { get; set; }
    public decimal? RatingFrom { get; set; }
    public decimal? RatingTo { get; set; }
    public bool? IsActive { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}