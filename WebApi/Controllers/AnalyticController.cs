using Domain.DTOs.CourierDTOs;
using Domain.DTOs.OrderDTOs;
using Domain.DTOs.RestaurantDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticController(IAnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("TopTenRestaurantsByOrders")]
    public async Task<Response<List<RestaurantsWithOrderCount>>> TopTenRestaurantsByOrders()
    {
        return await analyticsService.TopTenRestaurantsByOrders();
    }

    [HttpGet("RestaurantsProfitLastWeek")]
    public async Task<Response<List<RestaurantProfitByDay>>> RestaurantsProfitLastWeek()
    {
        return await analyticsService.RestaurantsProfitLastWeek();
    }

    [HttpGet("TopThreeDishesOfRestaurants")]
    public async Task<Response<List<RestaurantWIthPopularDishes>>> TopThreeDishesOfRestaurants()
    {
        return await analyticsService.TopThreeDishesOfRestaurants();
    }

    [HttpGet("MostBusyTime")]
    public async Task<Response<string>> MostBusyTime()
    {
        return await analyticsService.MostBusyTime();
    }

    [HttpGet("AverageCheckByAddress")]
    public async Task<Response<List<AddressAndCheck>>> AverageCheckByAddress()
    {
        return await analyticsService.AverageCheckByAddress();
    }

    [HttpGet("AverageTimeDeliveryByAddress")]
    public async Task<Response<List<AddressAndTimeDelivery>>> AverageTimeDeliveryByAddress()
    {
        return await analyticsService.AverageTimeDeliveryByAddress();
    }

    [HttpGet("FavouriteCategoriesOfUsers")]
    public async Task<Response<List<FavouriteUserCategory>>> FavouriteCategoriesOfUsers()
    {
        return await analyticsService.FavouriteCategoriesOfUsers();
    }

    [HttpGet("UserOverFiveOrdersMonth")]
    public async Task<Response<List<UsersOverFiveOrders>>> UserOverFiveOrdersMonth()
    {
        return await analyticsService.UserOverFiveOrdersMonth();
    }

    [HttpGet("CourierStats")]
    public async Task<Response<List<AverageDeliveryTimeAndRating>>> CourierStats()
    {
        return await analyticsService.CourierStats();
    }

    [HttpGet("CourierEarnings")]
    public async Task<Response<List<CouriersEarningsCountDTO>>> CourierEarnings()
    {
        return await analyticsService.CourierEarnings();
    }
}