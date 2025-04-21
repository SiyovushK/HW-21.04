using System.Net;
using AutoMapper;
using Domain.DTOs.RestaurantDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RestaurantService(DataContext context, IMapper mapper) : IRestaurantService
{
    public async Task<Response<GetRestaurantDTO>> CreateRestaurantAsync(CreateRestaurantDTO createRestaurant)
    {
        var restaurant = mapper.Map<Restaurant>(createRestaurant);

        await context.Restaurants.AddAsync(restaurant);
        var result = await context.SaveChangesAsync();

        var getRestaurantDto = mapper.Map<GetRestaurantDTO>(restaurant);

        return result == 0
            ? new Response<GetRestaurantDTO>(HttpStatusCode.InternalServerError, "Restaurant couldn't be created")
            : new Response<GetRestaurantDTO>(getRestaurantDto);
    }

    public async Task<Response<GetRestaurantDTO>> UpdateRestaurantAsync(int restaurantId, UpdateRestaurantDTO updateRestaurant)
    {
        var restaurant = await context.Restaurants.FindAsync(restaurantId);
        if(restaurant == null)
            return new Response<GetRestaurantDTO>(HttpStatusCode.NotFound, "Restaurant not found");

        mapper.Map(updateRestaurant, restaurant);

        var result = await context.SaveChangesAsync();

        var getRestaurantDto = mapper.Map<GetRestaurantDTO>(restaurant);

        return result == 0
            ? new Response<GetRestaurantDTO>(HttpStatusCode.InternalServerError, "Restaurant couldn't be updated")
            : new Response<GetRestaurantDTO>(getRestaurantDto);
    }

    public async Task<Response<string>> DeleteRestaurantAsync(int restaurantId)
    {
        var restaurant = await context.Restaurants.FindAsync(restaurantId);
        if(restaurant == null)
            return new Response<string>(HttpStatusCode.NotFound, "Restaurant not found");

        context.Restaurants.Remove(restaurant);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Restaurant couldn't be deleted")
            : new Response<string>("Restaurant deleted successfully");
    }

    public async Task<Response<GetRestaurantDTO>> GetRestaurantByIdAsync(int restaurantID)
    {
        var restaurant = await context.Restaurants.FindAsync(restaurantID);
        if(restaurant == null)
            return new Response<GetRestaurantDTO>(HttpStatusCode.NotFound, "Restaurant not found");

        var getRestaurantDto = mapper.Map<GetRestaurantDTO>(restaurant);

        return new Response<GetRestaurantDTO>(getRestaurantDto);
    }

    public async Task<Response<List<GetRestaurantDTO>>> GetAllRestaurantsAsync(RestaurantFilter filter)
    {
        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize = filter.PageSize < 10 ? 10 : filter.PageSize;

        var restaurantQuery = context.Restaurants.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            restaurantQuery = restaurantQuery
                .Where(r => r.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.RatingFrom != null)
        {
            restaurantQuery = restaurantQuery
                .Where(r => r.Rating >= filter.RatingFrom);
        }

        if (filter.RatingTo != null)
        {
            restaurantQuery = restaurantQuery
                .Where(r => r.Rating <= filter.RatingTo);
        }

        if (filter.IsActive != null)
        {
            restaurantQuery = restaurantQuery
                .Where(r => r.IsActive == filter.IsActive);
        }

        var totalRecords = await restaurantQuery.CountAsync();

        var rest = await restaurantQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var getRestaurantsDto = mapper.Map<List<GetRestaurantDTO>>(rest);

        return new PagedResposne<List<GetRestaurantDTO>>(getRestaurantsDto, pageSize, pageNumber, totalRecords);
    }

    //Task1
    public async Task<Response<List<GetRestaurantDTO>>> ActiveAndSorterRestaurants(int pageNumber = 1, int pageSize = 10)
    {
        var query = context.Restaurants
            .Where(r => r.IsActive)
            .OrderByDescending(r => r.Rating);
        
        var restaurants = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalRecords = await query.CountAsync();

        var getRestaurantDto = mapper.Map<List<GetRestaurantDTO>>(restaurants);

        return new PagedResposne<List<GetRestaurantDTO>>(getRestaurantDto, pageSize, pageNumber, totalRecords);
    }
}