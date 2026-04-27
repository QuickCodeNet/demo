using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CampaignApplicability;
using QuickCode.Demo.PricingEngineModule.Application.Services.CampaignApplicability;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Api.Controllers
{
    public partial class CampaignApplicabilitiesController : QuickCodeBaseApiController
    {
        private readonly ICampaignApplicabilityService service;
        private readonly ILogger<CampaignApplicabilitiesController> logger;
        private readonly IServiceProvider serviceProvider;
        public CampaignApplicabilitiesController(ICampaignApplicabilityService service, IServiceProvider serviceProvider, ILogger<CampaignApplicabilitiesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CampaignApplicabilityDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "CampaignApplicability", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "CampaignApplicability") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{campaignId:int}/{scope}/{scopeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CampaignApplicabilityDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int campaignId, RuleScope scope, int scopeId)
        {
            var response = await service.GetItemAsync(campaignId, scope, scopeId);
            if (HandleResponseError(response, logger, "CampaignApplicability", $"CampaignId: '{campaignId}', Scope: '{scope}', ScopeId: '{scopeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CampaignApplicabilityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(CampaignApplicabilityDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "CampaignApplicability") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { campaignId = response.Value.CampaignId, scope = response.Value.Scope, scopeId = response.Value.ScopeId }, response.Value);
        }

        [HttpPut("{campaignId:int}/{scope}/{scopeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int campaignId, RuleScope scope, int scopeId, CampaignApplicabilityDto model)
        {
            if (!(model.CampaignId == campaignId && model.Scope == scope && model.ScopeId == scopeId))
            {
                return BadRequest($"CampaignId: '{campaignId}', Scope: '{scope}', ScopeId: '{scopeId}' must be equal to model.CampaignId: '{model.CampaignId}', model.Scope: '{model.Scope}', model.ScopeId: '{model.ScopeId}'");
            }

            var response = await service.UpdateAsync(campaignId, scope, scopeId, model);
            if (HandleResponseError(response, logger, "CampaignApplicability", $"CampaignId: '{campaignId}', Scope: '{scope}', ScopeId: '{scopeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{campaignId:int}/{scope}/{scopeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int campaignId, RuleScope scope, int scopeId)
        {
            var response = await service.DeleteItemAsync(campaignId, scope, scopeId);
            if (HandleResponseError(response, logger, "CampaignApplicability", $"CampaignId: '{campaignId}', Scope: '{scope}', ScopeId: '{scopeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-campaign-id/{campaignApplicabilityCampaignId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByCampaignIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByCampaignIdAsync(int campaignApplicabilityCampaignId)
        {
            var response = await service.GetByCampaignIdAsync(campaignApplicabilityCampaignId);
            if (HandleResponseError(response, logger, "CampaignApplicability", $"CampaignApplicabilityCampaignId: '{campaignApplicabilityCampaignId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("remove-applicability/{campaignApplicabilityCampaignId:int}/{campaignApplicabilityScope}/{campaignApplicabilityScopeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RemoveApplicabilityAsync(int campaignApplicabilityCampaignId, RuleScope campaignApplicabilityScope, int campaignApplicabilityScopeId)
        {
            var response = await service.RemoveApplicabilityAsync(campaignApplicabilityCampaignId, campaignApplicabilityScope, campaignApplicabilityScopeId);
            if (HandleResponseError(response, logger, "CampaignApplicability", $"CampaignApplicabilityCampaignId: '{campaignApplicabilityCampaignId}', CampaignApplicabilityScope: '{campaignApplicabilityScope}', CampaignApplicabilityScopeId: '{campaignApplicabilityScopeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}