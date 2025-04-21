using System.Net;
using AutoMapper;
using Domain.DTOs.OrderDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService(DataContext context, IMapper mapper) : IOrderService
{
    public async Task<Response<GetOrderDTO>> CreateOrderAsync(CreateOrderDTO createOrder)
    {
        var order = mapper.Map<Order>(createOrder);

        await context.Orders.AddAsync(order);
        var result = await context.SaveChangesAsync();

        var getOrderDto = mapper.Map<GetOrderDTO>(order);

        return result == 0
            ? new Response<GetOrderDTO>(HttpStatusCode.InternalServerError, "Order couldn't be created")
            : new Response<GetOrderDTO>(getOrderDto);
    }

    public async Task<Response<GetOrderDTO>> UpdateOrderAsync(int orderId, UpdateOrderDTO updateOrder)
    {
        var order = await context.Orders.FindAsync(orderId);
        if(order == null)
            return new Response<GetOrderDTO>(HttpStatusCode.NotFound, "Order not found");

        mapper.Map(updateOrder, order);

        var result = await context.SaveChangesAsync();

        var getOrderDto = mapper.Map<GetOrderDTO>(order);

        return result == 0
            ? new Response<GetOrderDTO>(HttpStatusCode.InternalServerError, "Order couldn't be updated")
            : new Response<GetOrderDTO>(getOrderDto);
    }

    public async Task<Response<string>> DeleteOrderAsync(int orderId)
    {
        var order = await context.Orders.FindAsync(orderId);
        if(order == null)
            return new Response<string>(HttpStatusCode.NotFound, "Order not found");

        context.Orders.Remove(order);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Order couldn't be deleted")
            : new Response<string>("Order deleted successfully");
    }

    public async Task<Response<GetOrderDTO>> GetOrderByIdAsync(int orderID)
    {
        var order = await context.Orders.FindAsync(orderID);
        if(order == null)
            return new Response<GetOrderDTO>(HttpStatusCode.NotFound, "Order not found");

        var getOrderDto = mapper.Map<GetOrderDTO>(order);

        return new Response<GetOrderDTO>(getOrderDto);
    }

    public async Task<Response<List<GetOrderDTO>>> GetAllOrdersAsync()
    {
        var orders = await context.Orders.ToListAsync();
        if(orders.Count == 0)
            return new Response<List<GetOrderDTO>>(HttpStatusCode.NotFound, "No orders found");

        var getOrdersDto = mapper.Map<List<GetOrderDTO>>(orders);

        return new Response<List<GetOrderDTO>>(getOrdersDto);
    }

    //Task3
    public async Task<Response<List<OrderStatusDTO>>> CountOfOrdersByStatus()
    {
        var orders = await context.Orders
            .GroupBy(o => o.OrderStatus)
            .Select(g => new OrderStatusDTO
            {
                Status = g.Key.ToString(),
                Count = g.Count()
            })
            .ToListAsync();
        
        return new Response<List<OrderStatusDTO>>(orders);
    }

    //Task6
    public async Task<Response<List<GetOrderDTO>>> GetOrdersOfCourier(int courierId)
    {
        var info = await context.Couriers.FindAsync(courierId);
        if (info == null)
            return new Response<List<GetOrderDTO>>(HttpStatusCode.NotFound, "Courier not found");

        var query = await context.Orders
            .Where(o => o.CourierId == courierId)
            .ToListAsync();
        
        var orders = mapper.Map<List<GetOrderDTO>>(query);

        return new Response<List<GetOrderDTO>>(orders);
    }

    //Task7
    public async Task<Response<decimal>> TotalSumOfToday()
    {
        var date = DateTime.UtcNow.Date;

        var query = await context.Orders
            .Where(o => o.CreatedAt.Date == date && o.OrderStatus != Domain.Enums.OrderStatus.Cancelled)
            .SumAsync(o => o.TotalAmount);

        return new Response<decimal>(query);
    }

    //Task9
    public async Task<Response<List<GetOrderDTO>>> OrdersAboveAverageCheck()
    {
        decimal averagePrice = await context.Orders
            .AverageAsync(o => o.TotalAmount);
        
        var query = await context.Orders
            .Where(o => o.TotalAmount > averagePrice)
            .ToListAsync();

        var orders = mapper.Map<List<GetOrderDTO>>(query);

        return query.Count == 0
            ? new Response<List<GetOrderDTO>>(HttpStatusCode.NotFound, "Orders not found")
            : new Response<List<GetOrderDTO>>(orders); 
    }
}