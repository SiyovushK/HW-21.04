using System.Net;
using AutoMapper;
using Domain.DTOs.MenuDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MenuService(DataContext context, IMapper mapper) : IMenuService
{
    public async Task<Response<GetMenuDTO>> CreateMenuAsync(CreateMenuDTO createMenu)
    {
        var menu = mapper.Map<Menu>(createMenu);

        await context.Menus.AddAsync(menu);
        var result = await context.SaveChangesAsync();

        var getMenuDto = mapper.Map<GetMenuDTO>(menu);

        return result == 0
            ? new Response<GetMenuDTO>(HttpStatusCode.InternalServerError, "Menu couldn't be created")
            : new Response<GetMenuDTO>(getMenuDto);
    }

    public async Task<Response<GetMenuDTO>> UpdateMenuAsync(int menuId, UpdateMenuDTO updateMenu)
    {
        var menu = await context.Menus.FindAsync(menuId);
        if(menu == null)
            return new Response<GetMenuDTO>(HttpStatusCode.NotFound, "Menu not found");

        mapper.Map(updateMenu, menu);

        var result = await context.SaveChangesAsync();

        var getMenuDto = mapper.Map<GetMenuDTO>(menu);

        return result == 0
            ? new Response<GetMenuDTO>(HttpStatusCode.InternalServerError, "Menu couldn't be updated")
            : new Response<GetMenuDTO>(getMenuDto);
    }

    public async Task<Response<string>> DeleteMenuAsync(int menuId)
    {
        var menu = await context.Menus.FindAsync(menuId);
        if(menu == null)
            return new Response<string>(HttpStatusCode.NotFound, "Menu not found");

        context.Menus.Remove(menu);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Menu couldn't be deleted")
            : new Response<string>("Menu deleted successfully");
    }

    public async Task<Response<GetMenuDTO>> GetMenuByIdAsync(int menuID)
    {
        var menu = await context.Menus.FindAsync(menuID);
        if(menu == null)
            return new Response<GetMenuDTO>(HttpStatusCode.NotFound, "Menu not found");

        var getMenuDto = mapper.Map<GetMenuDTO>(menu);

        return new Response<GetMenuDTO>(getMenuDto);
    }

    public async Task<Response<List<GetMenuDTO>>> GetAllMenusAsync()
    {
        var menus = await context.Menus.ToListAsync();
        if(menus.Count == 0)
            return new Response<List<GetMenuDTO>>(HttpStatusCode.NotFound, "No Menus found");

        var getMenusDto = mapper.Map<List<GetMenuDTO>>(menus);

        return new Response<List<GetMenuDTO>>(getMenusDto);
    }

    //Task2
    public async Task<Response<List<GetMenuDTO>>> DishesSorted(int pageNumber = 1, int pageSize = 10)
    {
        var query = context.Menus
            .Where(m => m.IsAvailable && m.Price < 1000);
        
        var menus = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalRecords = await query.CountAsync();

        var getMenuDto = mapper.Map<List<GetMenuDTO>>(menus);

        return new PagedResposne<List<GetMenuDTO>>(getMenuDto, pageSize, pageNumber, totalRecords);
    }

    //Task4
    public async Task<Response<List<MenuCategoryAndPriceDTO>>> AveragePriceByCategory()
    {
        var menus = await context.Menus
            .GroupBy(m => m.Category)
            .Select(g => new MenuCategoryAndPriceDTO
            {
                Category = g.Key,
                AveragePrice = g.Average(m => m.Price)
            })
            .ToListAsync();

        return new Response<List<MenuCategoryAndPriceDTO>>(menus);
    }

    //Task10
    public async Task<Response<CategoryAndCountDTO>> CategoryWithMostDishes()
    {
        var query = await context.Menus
            .GroupBy(m => m.Category)
            .Select(g => new CategoryAndCountDTO
            {
                Category = g.Key,
                DishCount = g.Count()
            })
            .OrderByDescending(g => g.DishCount)
            .FirstOrDefaultAsync();
        
        return query == null
            ? new Response<CategoryAndCountDTO>(HttpStatusCode.NotFound, "Category with most dishes is not found")
            : new Response<CategoryAndCountDTO>(query); 
    }
}