using Domain.DTOs.OrderDetailDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailController(IOrderDetailService orderDetailService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetOrderDetailDTO>> CreateOrderDetail(CreateOrderDetailDTO createOrderDetail)
    {
        return await orderDetailService.CreateOrderDetailAsync(createOrderDetail);
    }

    [HttpPut]
    public async Task<Response<GetOrderDetailDTO>> UpdateOrderDetail(int orderDetailId, UpdateOrderDetailDTO updateOrderDetail)
    {
        return await orderDetailService.UpdateOrderDetailAsync(orderDetailId, updateOrderDetail);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteOrderDetail(int orderDetailId)
    {
        return await orderDetailService.DeleteOrderDetailAsync(orderDetailId);
    }

    [HttpGet("OrderDetailByOrderId")]
    public async Task<Response<GetOrderDetailDTO>> GetOrderDetailByOrderId(int orderId)
    {
        return await orderDetailService.GetOrderDetailByOrderIdAsync(orderId);
    }

    [HttpGet("id")]
    public async Task<Response<GetOrderDetailDTO>> GetOrderDetailById(int orderDetailId)
    {
        return await orderDetailService.GetOrderDetailByIdAsync(orderDetailId);
    }

    [HttpGet]
    public async Task<Response<List<GetOrderDetailDTO>>> GetAllOrderDetails()
    {
        return await orderDetailService.GetAllOrderDetailsAsync();
    }
}