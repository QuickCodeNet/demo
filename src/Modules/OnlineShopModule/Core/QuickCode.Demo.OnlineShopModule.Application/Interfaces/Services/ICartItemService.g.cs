using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.CartItem;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.CartItem
{
    public partial interface ICartItemService
    {
        Task<Response<CartItemDto>> InsertAsync(CartItemDto request);
        Task<Response<bool>> DeleteAsync(CartItemDto request);
        Task<Response<bool>> UpdateAsync(int id, CartItemDto request);
        Task<Response<List<CartItemDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CartItemDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetCartItemsWithTotalResponseDto>>> GetCartItemsWithTotalAsync(int cartItemsCartId);
    }
}