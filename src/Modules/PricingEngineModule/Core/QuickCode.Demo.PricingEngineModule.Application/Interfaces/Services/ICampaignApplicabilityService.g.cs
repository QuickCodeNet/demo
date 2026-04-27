using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CampaignApplicability;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.CampaignApplicability
{
    public partial interface ICampaignApplicabilityService
    {
        Task<Response<CampaignApplicabilityDto>> InsertAsync(CampaignApplicabilityDto request);
        Task<Response<bool>> DeleteAsync(CampaignApplicabilityDto request);
        Task<Response<bool>> UpdateAsync(int campaignId, RuleScope scope, int scopeId, CampaignApplicabilityDto request);
        Task<Response<List<CampaignApplicabilityDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CampaignApplicabilityDto>> GetItemAsync(int campaignId, RuleScope scope, int scopeId);
        Task<Response<bool>> DeleteItemAsync(int campaignId, RuleScope scope, int scopeId);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByCampaignIdResponseDto>>> GetByCampaignIdAsync(int campaignApplicabilityCampaignId);
        Task<Response<int>> RemoveApplicabilityAsync(int campaignApplicabilityCampaignId, RuleScope campaignApplicabilityScope, int campaignApplicabilityScopeId);
    }
}