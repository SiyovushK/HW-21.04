using Domain.DTOs.RestaurantDTOs;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetRestaurantDTO>> CreateRestaurant(CreateRestaurantDTO createRestaurant)
    {
        return await restaurantService.CreateRestaurantAsync(createRestaurant);
    }

    [HttpPut]
    public async Task<Response<GetRestaurantDTO>> UpdateRestaurant(int restaurantId, UpdateRestaurantDTO updateRestaurant)
    {
        return await restaurantService.UpdateRestaurantAsync(restaurantId, updateRestaurant);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteRestaurant(int restaurantId)
    {
        return await restaurantService.DeleteRestaurantAsync(restaurantId);
    }

    [HttpGet("filter")]
    public async Task<Response<List<GetRestaurantDTO>>> GetAllRestaurants([FromQuery] RestaurantFilter filter)
    {
        return await restaurantService.GetAllRestaurantsAsync(filter);
    }

    [HttpGet("ActiveAndSorterRestaurants")]
    public async Task<Response<List<GetRestaurantDTO>>> ActiveAndSorterRestaurants(int pageNumber = 1, int pageSize = 10)
    {
        return await restaurantService.ActiveAndSorterRestaurants(pageNumber, pageSize);
    }

    [HttpGet("id")]
    public async Task<Response<GetRestaurantDTO>> GetRestaurantById(int restaurantId)
    {
        return await restaurantService.GetRestaurantByIdAsync(restaurantId);
    }
}