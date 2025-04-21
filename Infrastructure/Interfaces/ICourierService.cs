using Domain.DTOs.CourierDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ICourierService
{
    Task<Response<GetCourierDTO>> CreateCourierAsync(CreateCourierDTO createCourier);
    Task<Response<GetCourierDTO>> UpdateCourierAsync(int courierId, UpdateCourierDTO updateCourier);
    Task<Response<string>> DeleteCourierAsync(int courierId);
    Task<Response<GetCourierDTO>> GetCourierByIdAsync(int courierID);
    Task<Response<List<GetCourierDTO>>> GetAllCouriersAsync();
    Task<Response<List<GetCourierDTO>>> TopFiveRatingCouriers();
}