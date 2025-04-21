using Domain.DTOs.OrderDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    Task<Response<GetOrderDTO>> CreateOrderAsync(CreateOrderDTO createOrder);
    Task<Response<GetOrderDTO>> UpdateOrderAsync(int orderId, UpdateOrderDTO updateOrder);
    Task<Response<string>> DeleteOrderAsync(int orderId);
    Task<Response<GetOrderDTO>> GetOrderByIdAsync(int orderID);
    Task<Response<List<GetOrderDTO>>> GetAllOrdersAsync();
    Task<Response<List<OrderStatusDTO>>> CountOfOrdersByStatus();
    Task<Response<List<GetOrderDTO>>> GetOrdersOfCourier(int courierId);
    Task<Response<decimal>> TotalSumOfToday();
    Task<Response<List<GetOrderDTO>>> OrdersAboveAverageCheck();
}