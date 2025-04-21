using Domain.DTOs.CourierDTOs;
using Domain.DTOs.OrderDTOs;
using Domain.DTOs.RestaurantDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IAnalyticsService
{
    Task<Response<List<RestaurantsWithOrderCount>>> TopTenRestaurantsByOrders();
    Task<Response<List<RestaurantProfitByDay>>> RestaurantsProfitLastWeek();
    Task<Response<List<RestaurantWIthPopularDishes>>> TopThreeDishesOfRestaurants();
    Task<Response<string>> MostBusyTime();
    Task<Response<List<AddressAndCheck>>> AverageCheckByAddress();
    Task<Response<List<AddressAndTimeDelivery>>> AverageTimeDeliveryByAddress();
    Task<Response<List<FavouriteUserCategory>>> FavouriteCategoriesOfUsers();
    Task<Response<List<UsersOverFiveOrders>>> UserOverFiveOrdersMonth();
    Task<Response<List<AverageDeliveryTimeAndRating>>> CourierStats();
    Task<Response<List<CouriersEarningsCountDTO>>> CourierEarnings();
}