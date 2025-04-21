using System.Net;
using AutoMapper;
using Domain.DTOs.CourierDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourierService(DataContext context, IMapper mapper) : ICourierService
{
    public async Task<Response<GetCourierDTO>> CreateCourierAsync(CreateCourierDTO createCourier)
    {
        var user = await context.Users.FindAsync(createCourier.UserId);
        if (user == null)
            return new Response<GetCourierDTO>(HttpStatusCode.NotFound, "User not found");

        user.Role = Domain.Enums.UserRole.Courier;

        var courier = mapper.Map<Courier>(createCourier);
        courier.User = user;

        await context.Couriers.AddAsync(courier);
        var result = await context.SaveChangesAsync();

        var getCourierDto = mapper.Map<GetCourierDTO>(courier);

        return result == 0
            ? new Response<GetCourierDTO>(HttpStatusCode.InternalServerError, "Courier couldn't be created")
            : new Response<GetCourierDTO>(getCourierDto);
    }

    public async Task<Response<GetCourierDTO>> UpdateCourierAsync(int courierId, UpdateCourierDTO updateCourier)
    {
        var courier = await context.Couriers
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == courierId);

        if(courier == null)
            return new Response<GetCourierDTO>(HttpStatusCode.NotFound, "Courier not found");

        mapper.Map(updateCourier, courier);

        var result = await context.SaveChangesAsync();

        var getCourierDto = mapper.Map<GetCourierDTO>(courier);

        return result == 0
            ? new Response<GetCourierDTO>(HttpStatusCode.InternalServerError, "Courier couldn't be updated")
            : new Response<GetCourierDTO>(getCourierDto);
    }

    public async Task<Response<string>> DeleteCourierAsync(int courierId)
    {
        var courier = await context.Couriers.FindAsync(courierId);
        if(courier == null)
            return new Response<string>(HttpStatusCode.NotFound, "Courier not found");

        context.Couriers.Remove(courier);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Courier couldn't be deleted")
            : new Response<string>("Courier deleted successfully");
    }

    public async Task<Response<GetCourierDTO>> GetCourierByIdAsync(int courierID)
    {
        var courier = await context.Couriers.FindAsync(courierID);
        if(courier == null)
            return new Response<GetCourierDTO>(HttpStatusCode.NotFound, "Courier not found");

        var getCourierDto = mapper.Map<GetCourierDTO>(courier);

        return new Response<GetCourierDTO>(getCourierDto);
    }

    public async Task<Response<List<GetCourierDTO>>> GetAllCouriersAsync()
    {
        var couriers = await context.Couriers.ToListAsync();
        if(couriers.Count == 0)
            return new Response<List<GetCourierDTO>>(HttpStatusCode.NotFound, "No Couriers found");

        var getCouriersDto = mapper.Map<List<GetCourierDTO>>(couriers);

        return new Response<List<GetCourierDTO>>(getCouriersDto);
    }

    //Task8
    public async Task<Response<List<GetCourierDTO>>> TopFiveRatingCouriers()
    {
        var query = await context.Couriers
            .OrderByDescending(c => c.Rating)
            .Take(5)
            .ToListAsync();

        if(query.Count == 0)
            return new Response<List<GetCourierDTO>>(HttpStatusCode.NotFound, "Couriers are not found");
        
        var couriers = mapper.Map<List<GetCourierDTO>>(query);
        
        return new Response<List<GetCourierDTO>>(couriers);
    }
}