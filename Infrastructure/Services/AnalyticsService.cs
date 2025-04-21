using System.Collections.Immutable;
using System.Net;
using AutoMapper;
using Domain.DTOs.CourierDTOs;
using Domain.DTOs.MenuDTOs;
using Domain.DTOs.OrderDTOs;
using Domain.DTOs.RestaurantDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Services;

public class AnalyticsService(DataContext context, IMapper mapper) : IAnalyticsService
{
    //11
    public async Task<Response<List<RestaurantsWithOrderCount>>> TopTenRestaurantsByOrders()
    {
        var date = DateTime.UtcNow.AddMonths(-1);

        var query = await context.Orders
            .Where(o => o.CreatedAt >= date)    
            .GroupBy(o => o.Restaurant)
            .Select(g => new RestaurantsWithOrderCount
            {
                RestaurantId = g.Key.Id,
                RestaurantName = g.Key.Name,
                OrdersCount = g.Count()
            })
            .OrderByDescending(g => g.OrdersCount)
            .Take(10)
            .ToListAsync();
        
        return query.Count == 0
            ? new Response<List<RestaurantsWithOrderCount>>(HttpStatusCode.NotFound, "Restaurants are not found")
            : new Response<List<RestaurantsWithOrderCount>>(query);
    }

    //12
    public async Task<Response<List<RestaurantProfitByDay>>> RestaurantsProfitLastWeek()
    {
        var date = DateTime.UtcNow.AddDays(-7);

        var restaurants = await context.Orders
            .Where(o => o.CreatedAt.Date >= date)
            .GroupBy(o => new { o.Restaurant.Id, o.Restaurant.Name, o.CreatedAt.Date })
            .Select(g => new RestaurantProfitByDay
            {
                RestaurantId = g.Key.Id,
                RestaurantName = g.Key.Name,
                Date = g.Key.Date,
                TotalProfit = g.Sum(o => o.TotalAmount)
            })
            .OrderByDescending(g => g.Date)
            .ToListAsync();
        
        return restaurants.Count == 0
            ? new Response<List<RestaurantProfitByDay>>(HttpStatusCode.NotFound, "Restaurants are not found")
            : new Response<List<RestaurantProfitByDay>>(restaurants);
    }

    //13
    public async Task<Response<List<RestaurantWIthPopularDishes>>> TopThreeDishesOfRestaurants()
    {
        var result = await context.Restaurants
            .Select(r => new RestaurantWIthPopularDishes
            {
                RestaurantId = r.Id,
                RestaurantName = r.Name,
                PopularDishes = r.Menus
                    .SelectMany(m => m.OrderDetails)
                    .GroupBy(od => od.MenuItem)
                    .OrderByDescending(g => g.Count())
                    .Take(3)
                    .Select(g => new DishesOfMenu
                    {
                        Id = g.Key.Id,  
                        Name = g.Key.Name,
                        Description = g.Key.Description
                    })
                    .Take(3)
                    .ToList()
            })
            .Where(r => r.PopularDishes.Any())
            .ToListAsync();
        
        return result.Count == 0
            ? new Response<List<RestaurantWIthPopularDishes>>(HttpStatusCode.NotFound, "Restaurants with dishes are not found")
            : new Response<List<RestaurantWIthPopularDishes>>(result);
    }

    //14
    public async Task<Response<string>> MostBusyTime()
    {
        var time = await context.Orders
            .GroupBy(o => o.CreatedAt.Hour)
            .Select(g => new 
            { 
                Hour = g.Key, 
                Count = g.Count() 
            })
            .OrderByDescending(g => g.Count)
            .FirstOrDefaultAsync();
        
        return new Response<string>($"The most busy time is: {time.Hour}:00");
    }

    //15
    public async Task<Response<List<AddressAndCheck>>> AverageCheckByAddress()
    {
        var query = await context.Orders
            .GroupBy(o => o.DeliveryAddress)
            .Select(g => new AddressAndCheck
            {   
                Address = g.Key,
                AverageCheck = g.Average(o => o.TotalAmount)
            })
            .ToListAsync();
        
        return new Response<List<AddressAndCheck>>(query);
    }

    //16
    public async Task<Response<List<AddressAndTimeDelivery>>> AverageTimeDeliveryByAddress()
    {
        var query = await context.Orders
            .Where(o => o.DeliveredAt != null)
            .GroupBy(o => o.DeliveryAddress)
            .Select(g => new AddressAndTimeDelivery
            {
                Address = g.Key,
                AverageDeliveryTime = g.Average(o => (o.DeliveredAt.Value - o.CreatedAt).TotalMinutes)
            })
            .ToListAsync();

        return new Response<List<AddressAndTimeDelivery>>(query);
    }

    //17
    public async Task<Response<List<FavouriteUserCategory>>> FavouriteCategoriesOfUsers()
    {
        var query = await context.Orders
            .Where(o => o.OrderDetails.Any())
            .SelectMany(o => o.OrderDetails
                .Select(od => new 
                {
                    UserId = o.User.Id,
                    UserName = o.User.Name,
                    Category = od.MenuItem.Category
                })
            )
            .GroupBy(x => new { x.UserId, x.UserName, x.Category })
            .Select(g => new
            {
                g.Key.UserId,
                g.Key.UserName,
                g.Key.Category,
                Count = g.Count()
            })
            .GroupBy(x => new { x.UserId, x.UserName })
            .Select(g => new FavouriteUserCategory
            {
                UserId = g.Key.UserId,
                UserName = g.Key.UserName,
                Category = g
                    .OrderByDescending(c => c.Count)
                    .Select(c => c.Category)
                    .FirstOrDefault()
            })
            .ToListAsync();
        
        return new Response<List<FavouriteUserCategory>>(query);
    }

    //18
    public async Task<Response<List<UsersOverFiveOrders>>> UserOverFiveOrdersMonth()
    {
        var date = DateTime.UtcNow.AddMonths(-1);        

        var users = await context.Orders
            .Where(o => o.CreatedAt >= date)
            .GroupBy(o => o.User)
            .Where(g => g.Count() > 5)
            .Select(g => new UsersOverFiveOrders
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Email = g.Key.Email,
                Phone = g.Key.Phone,
                Address = g.Key.Address,
                OrdersCount = g.Count()
            })
            .OrderByDescending(g => g.OrdersCount)
            .ToListAsync();

        return users.Count == 0
            ? new Response<List<UsersOverFiveOrders>>(HttpStatusCode.NotFound, "No users found with more than 5 orders")
            : new Response<List<UsersOverFiveOrders>>(users);
    }

    //19
    public async Task<Response<List<AverageDeliveryTimeAndRating>>> CourierStats()
    {
        var couriers = await context.Orders
            .Where(o => o.OrderStatus != Domain.Enums.OrderStatus.Cancelled && o.DeliveredAt != null)
            .GroupBy(o => o.Courier)
            .Select(g => new AverageDeliveryTimeAndRating
            {
                CourierId = g.Key.Id,
                Name = g.Key.User.Name,
                AverageDeliveryTime = g.Average(o => (o.DeliveredAt.Value - o.CreatedAt).TotalMinutes),
                AverageRating = g.Key.Rating
            })
            .ToListAsync();

        return couriers.Count == 0
            ? new Response<List<AverageDeliveryTimeAndRating>>(HttpStatusCode.NotFound, "No couriers found")
            : new Response<List<AverageDeliveryTimeAndRating>>(couriers);
    }

    //20
    public async Task<Response<List<CouriersEarningsCountDTO>>> CourierEarnings()
    {
        var couriers = await context.Orders
            .GroupBy(o => o.Courier)
            .Select(g => new CouriersEarningsCountDTO
            {
                CourierId = g.Key.Id,
                Name = g.Key.User.Name,
                Earnings = g.Sum(o => o.TotalAmount)
            })
            .ToListAsync();

        return couriers.Count == 0
            ? new Response<List<CouriersEarningsCountDTO>>(HttpStatusCode.NotFound, "No couriers found")
            : new Response<List<CouriersEarningsCountDTO>>(couriers);
    }
}