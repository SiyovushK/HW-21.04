using Domain.DTOs.RestaurantDTOs;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IRestaurantService
{
    Task<Response<GetRestaurantDTO>> CreateRestaurantAsync(CreateRestaurantDTO createRestaurant);
    Task<Response<GetRestaurantDTO>> UpdateRestaurantAsync(int restaurantId, UpdateRestaurantDTO updateRestaurant);
    Task<Response<string>> DeleteRestaurantAsync(int restaurantId);
    Task<Response<GetRestaurantDTO>> GetRestaurantByIdAsync(int restaurantID);
    Task<Response<List<GetRestaurantDTO>>> GetAllRestaurantsAsync(RestaurantFilter filter);
    Task<Response<List<GetRestaurantDTO>>> ActiveAndSorterRestaurants(int pageNumber = 1, int pageSize = 10);
}