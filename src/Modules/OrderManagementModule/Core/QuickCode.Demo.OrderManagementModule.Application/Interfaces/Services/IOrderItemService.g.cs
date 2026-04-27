using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OrderManagementModule.Domain.Entities;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.OrderItem;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Application.Services.OrderItem
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
        Task<Response<List<GetByOrderIdResponseDto>>> GetByOrderIdAsync(int orderItemOrderId, int? page, int? size);
    }
}