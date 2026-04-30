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
    public partial interface ICommissionRuleService
    {
        Task<Response<CommissionRuleDto>> InsertAsync(CommissionRuleDto request);
        Task<Response<bool>> DeleteAsync(CommissionRuleDto request);
        Task<Response<bool>> UpdateAsync(int id, CommissionRuleDto request);
        Task<Response<List<CommissionRuleDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CommissionRuleDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByModelIdResponseDto>>> GetByModelIdAsync(int commissionRuleCommissionModelId, int? pageNumber, int? pageSize);
        Task<Response<List<GetActiveRulesByModelResponseDto>>> GetActiveRulesByModelAsync(int commissionRuleCommissionModelId, int? pageNumber, int? pageSize);
        Task<Response<int>> DeactivateAsync(int commissionRuleId, DeactivateRequestDto updateRequest);
    }
}