using Domain.DTOs.CourierDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourierController(ICourierService courierService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetCourierDTO>> CreateCourier(CreateCourierDTO createCourier)
    {
        return await courierService.CreateCourierAsync(createCourier);
    }

    [HttpPut]
    public async Task<Response<GetCourierDTO>> UpdateCourier(int courierId, UpdateCourierDTO updateCourier)
    {
        return await courierService.UpdateCourierAsync(courierId, updateCourier);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteCourier(int courierId)
    {
        return await courierService.DeleteCourierAsync(courierId);
    }

    [HttpGet]
    public async Task<Response<List<GetCourierDTO>>> GetAllCouriers()
    {
        return await courierService.GetAllCouriersAsync();
    }

    [HttpGet("TopFiveRatingCouriers")]
    public async Task<Response<List<GetCourierDTO>>> TopFiveRatingCouriers()
    {
        return await courierService.TopFiveRatingCouriers();
    }

    [HttpGet("id")]
    public async Task<Response<GetCourierDTO>> GetCourierById(int courierId)
    {
        return await courierService.GetCourierByIdAsync(courierId);
    }
}