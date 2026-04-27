using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CommissionModel;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.CommissionModel
{
    public partial class CommissionModelService : ICommissionModelService
    {
        private readonly ILogger<CommissionModelService> _logger;
        private readonly ICommissionModelRepository _repository;
        public CommissionModelService(ILogger<CommissionModelService> logger, ICommissionModelRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CommissionModelDto>> InsertAsync(CommissionModelDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CommissionModelDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CommissionModelDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CommissionModelDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CommissionModelDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetActiveResponseDto>>> GetActiveAsync(bool commissionModelIsActive, int? page, int? size)
        {
            var returnValue = await _repository.GetActiveAsync(commissionModelIsActive, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetByNameResponseDto>> GetByNameAsync(string commissionModelName)
        {
            var returnValue = await _repository.GetByNameAsync(commissionModelName);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeactivateAsync(int commissionModelId, DeactivateRequestDto updateRequest)
        {
            var returnValue = await _repository.DeactivateAsync(commissionModelId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}