using System.Net;
using AutoMapper;
using Domain.DTOs.OrderDetailDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderDetailService(DataContext context, IMapper mapper) : IOrderDetailService
{
    public async Task<Response<GetOrderDetailDTO>> CreateOrderDetailAsync(CreateOrderDetailDTO createOrderDetail)
    {
        var orderDetail = mapper.Map<OrderDetail>(createOrderDetail);

        await context.OrderDetails.AddAsync(orderDetail);
        var result = await context.SaveChangesAsync();

        var getOrderDetailDto = mapper.Map<GetOrderDetailDTO>(orderDetail);

        return result == 0
            ? new Response<GetOrderDetailDTO>(HttpStatusCode.InternalServerError, "Order Detail couldn't be created")
            : new Response<GetOrderDetailDTO>(getOrderDetailDto);
    }

    public async Task<Response<GetOrderDetailDTO>> UpdateOrderDetailAsync(int orderDetailId, UpdateOrderDetailDTO updateOrderDetail)
    {
        var orderDetail = await context.OrderDetails.FindAsync(orderDetailId);
        if(orderDetail == null)
            return new Response<GetOrderDetailDTO>(HttpStatusCode.NotFound, "Orde rDetail not found");

        mapper.Map(updateOrderDetail, orderDetail);

        var result = await context.SaveChangesAsync();

        var getOrderDetailDto = mapper.Map<GetOrderDetailDTO>(orderDetail);

        return result == 0
            ? new Response<GetOrderDetailDTO>(HttpStatusCode.InternalServerError, "Order Detail couldn't be updated")
            : new Response<GetOrderDetailDTO>(getOrderDetailDto);
    }

    public async Task<Response<string>> DeleteOrderDetailAsync(int orderDetailId)
    {
        var orderDetail = await context.OrderDetails.FindAsync(orderDetailId);
        if(orderDetail == null)
            return new Response<string>(HttpStatusCode.NotFound, "Order Detail not found");

        context.OrderDetails.Remove(orderDetail);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Order Detail couldn't be deleted")
            : new Response<string>("Order Detail deleted successfully");
    }

    public async Task<Response<GetOrderDetailDTO>> GetOrderDetailByIdAsync(int OrderDetailId)
    {
        var orderDetail = await context.OrderDetails.FindAsync(OrderDetailId);
        if(orderDetail == null)
            return new Response<GetOrderDetailDTO>(HttpStatusCode.NotFound, "Order Detail not found");

        var getOrderDetailDto = mapper.Map<GetOrderDetailDTO>(orderDetail);

        return new Response<GetOrderDetailDTO>(getOrderDetailDto);
    }

    public async Task<Response<GetOrderDetailDTO>> GetOrderDetailByOrderIdAsync(int orderId)
    {
        var orderDetail = await context.OrderDetails
            .Where(od => od.OrderId == orderId)
            .FirstOrDefaultAsync();

        if(orderDetail == null)
            return new Response<GetOrderDetailDTO>(HttpStatusCode.NotFound, "Order Detail not found");

        var getOrderDetailDto = mapper.Map<GetOrderDetailDTO>(orderDetail);

        return new Response<GetOrderDetailDTO>(getOrderDetailDto);
    }

    public async Task<Response<List<GetOrderDetailDTO>>> GetAllOrderDetailsAsync()
    {
        var orderDetails = await context.OrderDetails.ToListAsync();
        if(orderDetails.Count == 0)
            return new Response<List<GetOrderDetailDTO>>(HttpStatusCode.NotFound, "No Order Details found");

        var getOrderDetailsDto = mapper.Map<List<GetOrderDetailDTO>>(orderDetails);

        return new Response<List<GetOrderDetailDTO>>(getOrderDetailsDto);
    }
}