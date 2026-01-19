using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Order;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Order
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
        Task<Response<List<GetOrdersYearlyResponseDto>>> GetOrdersYearlyAsync(int ordersUserId, decimal ordersTotalAmount);
        Task<Response<List<GetOrdersByStatusDateResponseDto>>> GetOrdersByStatusDateAsync(DateTime ordersOrderDate, OrderStatus ordersStatus);
        Task<Response<List<GetUserOrdersResponseDto>>> GetUserOrdersAsync(int ordersUserId, PaymentStatus paymentsStatus);
        Task<Response<List<GetOrderItemsForOrdersResponseDto>>> GetOrderItemsForOrdersAsync(int ordersId);
        Task<Response<GetOrderItemsForOrdersResponseDto>> GetOrderItemsForOrdersDetailsAsync(int ordersId, int orderItemsId);
        Task<Response<List<GetShipmentsForOrdersResponseDto>>> GetShipmentsForOrdersAsync(int ordersId);
        Task<Response<GetShipmentsForOrdersResponseDto>> GetShipmentsForOrdersDetailsAsync(int ordersId, int shipmentsId);
    }
}