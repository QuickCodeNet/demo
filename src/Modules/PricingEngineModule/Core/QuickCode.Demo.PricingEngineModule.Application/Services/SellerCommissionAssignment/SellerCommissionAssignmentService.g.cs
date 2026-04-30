using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.SellerCommissionAssignment;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.SellerCommissionAssignment
{
    public partial class SellerCommissionAssignmentService : ISellerCommissionAssignmentService
    {
        private readonly ILogger<SellerCommissionAssignmentService> _logger;
        private readonly ISellerCommissionAssignmentRepository _repository;
        public SellerCommissionAssignmentService(ILogger<SellerCommissionAssignmentService> logger, ISellerCommissionAssignmentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SellerCommissionAssignmentDto>> InsertAsync(SellerCommissionAssignmentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SellerCommissionAssignmentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int sellerId, SellerCommissionAssignmentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.SellerId);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SellerCommissionAssignmentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SellerCommissionAssignmentDto>> GetItemAsync(int sellerId)
        {
            var returnValue = await _repository.GetByPkAsync(sellerId);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int sellerId)
        {
            var deleteItem = await _repository.GetByPkAsync(sellerId);
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

        public async Task<Response<GetBySellerIdResponseDto>> GetBySellerIdAsync(int sellerCommissionAssignmentSellerId)
        {
            var returnValue = await _repository.GetBySellerIdAsync(sellerCommissionAssignmentSellerId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByModelIdResponseDto>>> GetByModelIdAsync(int sellerCommissionAssignmentCommissionModelId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetByModelIdAsync(sellerCommissionAssignmentCommissionModelId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RemoveAssignmentAsync(int sellerCommissionAssignmentSellerId)
        {
            var returnValue = await _repository.RemoveAssignmentAsync(sellerCommissionAssignmentSellerId);
            return returnValue.ToResponse();
        }
    }
}