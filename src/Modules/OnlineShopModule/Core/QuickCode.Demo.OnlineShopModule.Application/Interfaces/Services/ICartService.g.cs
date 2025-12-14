using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Cart;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Cart
{
    public partial interface ICartService
    {
        Task<Response<CartDto>> InsertAsync(CartDto request);
        Task<Response<bool>> DeleteAsync(CartDto request);
        Task<Response<bool>> UpdateAsync(int id, CartDto request);
        Task<Response<List<CartDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CartDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetAbandonedCartsResponseDto>>> GetAbandonedCartsAsync(bool cartsIsActive);
        Task<Response<List<GetCartItemsForCartsResponseDto>>> GetCartItemsForCartsAsync(int cartsId);
        Task<Response<GetCartItemsForCartsResponseDto>> GetCartItemsForCartsDetailsAsync(int cartsId, int cartItemsId);
        Task<Response<int>> DeactivateStaleCartsAsync(bool cartsIsActive, DeactivateStaleCartsRequestDto updateRequest);
    }
}