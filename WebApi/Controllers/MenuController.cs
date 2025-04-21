using Domain.DTOs.MenuDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController(IMenuService menuService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetMenuDTO>> CreateMenu(CreateMenuDTO createMenu)
    {
        return await menuService.CreateMenuAsync(createMenu);
    }

    [HttpPut]
    public async Task<Response<GetMenuDTO>> UpdateMenu(int menuId, UpdateMenuDTO updateMenu)
    {
        return await menuService.UpdateMenuAsync(menuId, updateMenu);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteMenu(int menuId)
    {
        return await menuService.DeleteMenuAsync(menuId);
    }

    [HttpGet]
    public async Task<Response<List<GetMenuDTO>>> GetAllMenus()
    {
        return await menuService.GetAllMenusAsync();
    }

    [HttpGet("DishesSorted")]
    public async Task<Response<List<GetMenuDTO>>> DishesSorted(int pageNumber = 1, int pageSize = 10)
    {
        return await menuService.DishesSorted(pageNumber, pageSize);
    }

    [HttpGet("AveragePriceByCategory")]
    public async Task<Response<List<MenuCategoryAndPriceDTO>>> AveragePriceByCategory()
    {
        return await menuService.AveragePriceByCategory();
    }

    [HttpGet("CategoryWithMostDishes")]
    public async Task<Response<CategoryAndCountDTO>> CategoryWithMostDishes()
    {
        return await menuService.CategoryWithMostDishes();
    }

    [HttpGet("id")]
    public async Task<Response<GetMenuDTO>> GetMenuById(int menuId)
    {
        return await menuService.GetMenuByIdAsync(menuId);
    }
}