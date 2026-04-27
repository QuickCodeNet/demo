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
    public partial interface IPromotionalCampaignService
    {
        Task<Response<PromotionalCampaignDto>> InsertAsync(PromotionalCampaignDto request);
        Task<Response<bool>> DeleteAsync(PromotionalCampaignDto request);
        Task<Response<bool>> UpdateAsync(int id, PromotionalCampaignDto request);
        Task<Response<List<PromotionalCampaignDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<PromotionalCampaignDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveCampaignsResponseDto>>> GetActiveCampaignsAsync(bool promotionalCampaignIsActive, int? page, int? size);
        Task<Response<List<GetCampaignsInDateRangeResponseDto>>> GetCampaignsInDateRangeAsync(DateTime promotionalCampaignStartDateFrom, DateTime promotionalCampaignStartDateTo, int? page, int? size);
        Task<Response<int>> DeactivateAsync(int promotionalCampaignId, DeactivateRequestDto updateRequest);
    }
}