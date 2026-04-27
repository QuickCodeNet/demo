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
    public partial class CampaignApplicabilityService : ICampaignApplicabilityService
    {
        private readonly ILogger<CampaignApplicabilityService> _logger;
        private readonly ICampaignApplicabilityRepository _repository;
        public CampaignApplicabilityService(ILogger<CampaignApplicabilityService> logger, ICampaignApplicabilityRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CampaignApplicabilityDto>> InsertAsync(CampaignApplicabilityDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CampaignApplicabilityDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int campaignId, RuleScope scope, int scopeId, CampaignApplicabilityDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.CampaignId, request.Scope, request.ScopeId);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CampaignApplicabilityDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CampaignApplicabilityDto>> GetItemAsync(int campaignId, RuleScope scope, int scopeId)
        {
            var returnValue = await _repository.GetByPkAsync(campaignId, scope, scopeId);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int campaignId, RuleScope scope, int scopeId)
        {
            var deleteItem = await _repository.GetByPkAsync(campaignId, scope, scopeId);
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

        public async Task<Response<List<GetByCampaignIdResponseDto>>> GetByCampaignIdAsync(int campaignApplicabilityCampaignId)
        {
            var returnValue = await _repository.GetByCampaignIdAsync(campaignApplicabilityCampaignId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RemoveApplicabilityAsync(int campaignApplicabilityCampaignId, RuleScope campaignApplicabilityScope, int campaignApplicabilityScopeId)
        {
            var returnValue = await _repository.RemoveApplicabilityAsync(campaignApplicabilityCampaignId, campaignApplicabilityScope, campaignApplicabilityScopeId);
            return returnValue.ToResponse();
        }
    }
}