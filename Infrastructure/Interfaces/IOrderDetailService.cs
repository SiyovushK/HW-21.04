using Domain.DTOs.OrderDetailDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IOrderDetailService
{
    Task<Response<GetOrderDetailDTO>> CreateOrderDetailAsync(CreateOrderDetailDTO createOrderDetail);
    Task<Response<GetOrderDetailDTO>> UpdateOrderDetailAsync(int orderDetailId, UpdateOrderDetailDTO updateOrderDetail);
    Task<Response<string>> DeleteOrderDetailAsync(int orderDetailId);
    Task<Response<GetOrderDetailDTO>> GetOrderDetailByIdAsync(int OrderDetailId);
    Task<Response<GetOrderDetailDTO>> GetOrderDetailByOrderIdAsync(int orderId);
    Task<Response<List<GetOrderDetailDTO>>> GetAllOrderDetailsAsync();
}