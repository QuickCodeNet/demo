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
    public partial class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _repository;
        public OrderService(ILogger<OrderService> logger, IOrderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<OrderDto>> InsertAsync(OrderDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(OrderDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, OrderDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<OrderDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<OrderDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<GetByOrderNumberResponseDto>> GetByOrderNumberAsync(string orderOrderNumber)
        {
            var returnValue = await _repository.GetByOrderNumberAsync(orderOrderNumber);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByCustomerIdResponseDto>>> GetByCustomerIdAsync(int orderCustomerId, int? page, int? size)
        {
            var returnValue = await _repository.GetByCustomerIdAsync(orderCustomerId, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int orderSellerId, int? page, int? size)
        {
            var returnValue = await _repository.GetBySellerIdAsync(orderSellerId, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(OrderStatus orderStatus, int? page, int? size)
        {
            var returnValue = await _repository.GetByStatusAsync(orderStatus, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByDateRangeResponseDto>>> GetByDateRangeAsync(DateTime orderOrderDateFrom, DateTime orderOrderDateTo, int? page, int? size)
        {
            var returnValue = await _repository.GetByDateRangeAsync(orderOrderDateFrom, orderOrderDateTo, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOrdersForSettlementResponseDto>>> GetOrdersForSettlementAsync(int orderSellerId, OrderStatus orderStatus, int? page, int? size)
        {
            var returnValue = await _repository.GetOrdersForSettlementAsync(orderSellerId, orderStatus, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMonthlyRevenueBySellerResponseDto>> GetMonthlyRevenueBySellerAsync(int orderSellerId)
        {
            var returnValue = await _repository.GetMonthlyRevenueBySellerAsync(orderSellerId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int orderId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(orderId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> CancelOrderAsync(int orderId, CancelOrderRequestDto updateRequest)
        {
            var returnValue = await _repository.CancelOrderAsync(orderId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}