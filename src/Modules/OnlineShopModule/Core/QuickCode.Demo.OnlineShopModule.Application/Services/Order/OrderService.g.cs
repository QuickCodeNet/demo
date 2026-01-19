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

        public async Task<Response<List<GetOrdersYearlyResponseDto>>> GetOrdersYearlyAsync(int ordersUserId, decimal ordersTotalAmount)
        {
            var returnValue = await _repository.GetOrdersYearlyAsync(ordersUserId, ordersTotalAmount);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOrdersByStatusDateResponseDto>>> GetOrdersByStatusDateAsync(DateTime ordersOrderDate, OrderStatus ordersStatus)
        {
            var returnValue = await _repository.GetOrdersByStatusDateAsync(ordersOrderDate, ordersStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUserOrdersResponseDto>>> GetUserOrdersAsync(int ordersUserId, PaymentStatus paymentsStatus)
        {
            var returnValue = await _repository.GetUserOrdersAsync(ordersUserId, paymentsStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOrderItemsForOrdersResponseDto>>> GetOrderItemsForOrdersAsync(int ordersId)
        {
            var returnValue = await _repository.GetOrderItemsForOrdersAsync(ordersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOrderItemsForOrdersResponseDto>> GetOrderItemsForOrdersDetailsAsync(int ordersId, int orderItemsId)
        {
            var returnValue = await _repository.GetOrderItemsForOrdersDetailsAsync(ordersId, orderItemsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetShipmentsForOrdersResponseDto>>> GetShipmentsForOrdersAsync(int ordersId)
        {
            var returnValue = await _repository.GetShipmentsForOrdersAsync(ordersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetShipmentsForOrdersResponseDto>> GetShipmentsForOrdersDetailsAsync(int ordersId, int shipmentsId)
        {
            var returnValue = await _repository.GetShipmentsForOrdersDetailsAsync(ordersId, shipmentsId);
            return returnValue.ToResponse();
        }
    }
}