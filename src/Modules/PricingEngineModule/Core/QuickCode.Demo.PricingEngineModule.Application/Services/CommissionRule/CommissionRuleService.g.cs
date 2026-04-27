using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CommissionRule;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.CommissionRule
{
    public partial class CommissionRuleService : ICommissionRuleService
    {
        private readonly ILogger<CommissionRuleService> _logger;
        private readonly ICommissionRuleRepository _repository;
        public CommissionRuleService(ILogger<CommissionRuleService> logger, ICommissionRuleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CommissionRuleDto>> InsertAsync(CommissionRuleDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CommissionRuleDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CommissionRuleDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CommissionRuleDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CommissionRuleDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetByModelIdResponseDto>>> GetByModelIdAsync(int commissionRuleCommissionModelId, int? page, int? size)
        {
            var returnValue = await _repository.GetByModelIdAsync(commissionRuleCommissionModelId, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveRulesByModelResponseDto>>> GetActiveRulesByModelAsync(int commissionRuleCommissionModelId, bool commissionRuleIsActive, int? page, int? size)
        {
            var returnValue = await _repository.GetActiveRulesByModelAsync(commissionRuleCommissionModelId, commissionRuleIsActive, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeactivateAsync(int commissionRuleId, DeactivateRequestDto updateRequest)
        {
            var returnValue = await _repository.DeactivateAsync(commissionRuleId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}