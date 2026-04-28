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
    public partial class OrderStatusManagerService : IOrderStatusManagerService
    {
        private readonly ILogger<OrderStatusManagerService> _logger;
        private readonly IOrderStatusManagerRepository _repository;
        public OrderStatusManagerService(ILogger<OrderStatusManagerService> logger, IOrderStatusManagerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<OrderStatusManagerDto>> InsertAsync(OrderStatusManagerDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(OrderStatusManagerDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, OrderStatusManagerDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<OrderStatusManagerDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<OrderStatusManagerDto>> GetItemAsync(int id)
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
    }
}