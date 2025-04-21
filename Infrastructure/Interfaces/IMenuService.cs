using Domain.DTOs.MenuDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IMenuService
{
    Task<Response<GetMenuDTO>> CreateMenuAsync(CreateMenuDTO createMenu);
    Task<Response<GetMenuDTO>> UpdateMenuAsync(int menuId, UpdateMenuDTO updateMenu);
    Task<Response<string>> DeleteMenuAsync(int menuId);
    Task<Response<GetMenuDTO>> GetMenuByIdAsync(int menuID);
    Task<Response<List<GetMenuDTO>>> GetAllMenusAsync();
    Task<Response<List<GetMenuDTO>>> DishesSorted(int pageNumber = 1, int pageSize = 10);
    Task<Response<List<MenuCategoryAndPriceDTO>>> AveragePriceByCategory();
    Task<Response<CategoryAndCountDTO>> CategoryWithMostDishes();
}