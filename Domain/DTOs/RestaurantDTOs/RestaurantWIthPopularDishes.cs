using Domain.DTOs.MenuDTOs;
using Domain.Entities;

namespace Domain.DTOs.RestaurantDTOs;

public class RestaurantWIthPopularDishes
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public List<DishesOfMenu> PopularDishes { get; set; } = new List<DishesOfMenu>();
}