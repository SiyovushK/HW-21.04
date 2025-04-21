using AutoMapper;
using Domain.DTOs.CourierDTOs;
using Domain.DTOs.MenuDTOs;
using Domain.DTOs.OrderDetailDTOs;
using Domain.DTOs.OrderDTOs;
using Domain.DTOs.RestaurantDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<User, GetUserDTO>();
        CreateMap<CreateUserDTO, User>();
        CreateMap<UpdateUserDTO, User>();

        CreateMap<Restaurant, GetRestaurantDTO>();
        CreateMap<CreateRestaurantDTO, Restaurant>();
        CreateMap<UpdateRestaurantDTO, Restaurant>();

        CreateMap<Order, GetOrderDTO>();
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<UpdateOrderDTO, Order>();

        CreateMap<OrderDetail, GetOrderDetailDTO>();
        CreateMap<CreateOrderDetailDTO, OrderDetail>();
        CreateMap<UpdateOrderDetailDTO, OrderDetail>();

        CreateMap<Menu, GetMenuDTO>();
        CreateMap<CreateMenuDTO, Menu>();
        CreateMap<UpdateMenuDTO, Menu>();

        CreateMap<Courier, GetCourierDTO>();
        CreateMap<CreateCourierDTO, Courier>();
        CreateMap<UpdateCourierDTO, Courier>(); 
    }
}