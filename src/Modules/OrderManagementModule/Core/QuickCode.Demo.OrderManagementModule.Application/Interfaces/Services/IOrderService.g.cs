using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OrderManagementModule.Domain.Entities;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.Order;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Application.Services.Order
{
    public partial interface IOrderService
    {
        Task<Response<OrderDto>> InsertAsync(OrderDto request);
        Task<Response<bool>> DeleteAsync(OrderDto request);
        Task<Response<bool>> UpdateAsync(int id, OrderDto request);
        Task<Response<List<OrderDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<OrderDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByOrderNumberResponseDto>> GetByOrderNumberAsync(string orderOrderNumber);
        Task<Response<List<GetByCustomerIdResponseDto>>> GetByCustomerIdAsync(int orderCustomerId, int? page, int? size);
        Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int orderSellerId, int? page, int? size);
        Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(OrderStatus orderStatus, int? page, int? size);
        Task<Response<List<GetByDateRangeResponseDto>>> GetByDateRangeAsync(DateTime orderOrderDateFrom, DateTime orderOrderDateTo, int? page, int? size);
        Task<Response<List<GetOrdersForSettlementResponseDto>>> GetOrdersForSettlementAsync(int orderSellerId, OrderStatus orderStatus, int? page, int? size);
        Task<Response<GetMonthlyRevenueBySellerResponseDto>> GetMonthlyRevenueBySellerAsync(int orderSellerId);
        Task<Response<int>> UpdateStatusAsync(int orderId, UpdateStatusRequestDto updateRequest);
        Task<Response<int>> CancelOrderAsync(int orderId, CancelOrderRequestDto updateRequest);
    }
}