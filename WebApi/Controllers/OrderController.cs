using Domain.DTOs.OrderDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetOrderDTO>> CreateOrder(CreateOrderDTO createOrder)
    {
        return await orderService.CreateOrderAsync(createOrder);
    }

    [HttpPut]
    public async Task<Response<GetOrderDTO>> UpdateOrder(int orderId, UpdateOrderDTO updateOrder)
    {
        return await orderService.UpdateOrderAsync(orderId, updateOrder);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteOrder(int orderId)
    {
        return await orderService.DeleteOrderAsync(orderId);
    }

    [HttpGet]
    public async Task<Response<List<GetOrderDTO>>> GetAllOrders()
    {
        return await orderService.GetAllOrdersAsync();
    }

    [HttpGet("CountOfOrdersByStatus")]
    public async Task<Response<List<OrderStatusDTO>>> CountOfOrdersByStatus()
    {
        return await orderService.CountOfOrdersByStatus();
    }

    [HttpGet("OrdersOfCourier")]
    public async Task<Response<List<GetOrderDTO>>> GetOrdersOfCourier(int courierId)
    {
        return await orderService.GetOrdersOfCourier(courierId);
    }

    [HttpGet("TotalSumOfToday")]
    public async Task<Response<decimal>> TotalSumOfToday()
    {
        return await orderService.TotalSumOfToday();
    }

    [HttpGet("OrdersAboveAverageCheck")]
    public async Task<Response<List<GetOrderDTO>>> OrdersAboveAverageCheck()
    {
        return await orderService.OrdersAboveAverageCheck();
    }

    [HttpGet("id")]
    public async Task<Response<GetOrderDTO>> GetOrderById(int orderId)
    {
        return await orderService.GetOrderByIdAsync(orderId);
    }
}