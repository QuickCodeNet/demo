using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.PromotionalCampaign;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.PromotionalCampaign
{
    public partial class PromotionalCampaignService : IPromotionalCampaignService
    {
        private readonly ILogger<PromotionalCampaignService> _logger;
        private readonly IPromotionalCampaignRepository _repository;
        public PromotionalCampaignService(ILogger<PromotionalCampaignService> logger, IPromotionalCampaignRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<PromotionalCampaignDto>> InsertAsync(PromotionalCampaignDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(PromotionalCampaignDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, PromotionalCampaignDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<PromotionalCampaignDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<PromotionalCampaignDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetActiveCampaignsResponseDto>>> GetActiveCampaignsAsync(bool promotionalCampaignIsActive, int? page, int? size)
        {
            var returnValue = await _repository.GetActiveCampaignsAsync(promotionalCampaignIsActive, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCampaignsInDateRangeResponseDto>>> GetCampaignsInDateRangeAsync(DateTime promotionalCampaignStartDateFrom, DateTime promotionalCampaignStartDateTo, int? page, int? size)
        {
            var returnValue = await _repository.GetCampaignsInDateRangeAsync(promotionalCampaignStartDateFrom, promotionalCampaignStartDateTo, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeactivateAsync(int promotionalCampaignId, DeactivateRequestDto updateRequest)
        {
            var returnValue = await _repository.DeactivateAsync(promotionalCampaignId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}