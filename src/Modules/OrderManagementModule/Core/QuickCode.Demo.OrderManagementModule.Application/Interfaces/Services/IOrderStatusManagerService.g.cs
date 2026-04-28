using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OrderManagementModule.Domain.Entities;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.OrderStatusManager;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Application.Services.OrderStatusManager
{
    public partial interface IOrderStatusManagerService
    {
        Task<Response<OrderStatusManagerDto>> InsertAsync(OrderStatusManagerDto request);
        Task<Response<bool>> DeleteAsync(OrderStatusManagerDto request);
        Task<Response<bool>> UpdateAsync(int id, OrderStatusManagerDto request);
        Task<Response<List<OrderStatusManagerDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<OrderStatusManagerDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
    }
}