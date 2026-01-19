using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.OrderItem;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.OrderItem
{
    public partial interface IOrderItemService
    {
        Task<Response<OrderItemDto>> InsertAsync(OrderItemDto request);
        Task<Response<bool>> DeleteAsync(OrderItemDto request);
        Task<Response<bool>> UpdateAsync(int id, OrderItemDto request);
        Task<Response<List<OrderItemDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<OrderItemDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetOrderItemsDetailsResponseDto>>> GetOrderItemsDetailsAsync(int orderItemsOrderId);
        Task<Response<List<GetProductOrderItemsResponseDto>>> GetProductOrderItemsAsync(int orderItemsProductId);
    }
}