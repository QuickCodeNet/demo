using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Campaign;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Campaign
{
    public partial interface ICampaignService
    {
        Task<Response<CampaignDto>> InsertAsync(CampaignDto request);
        Task<Response<bool>> DeleteAsync(CampaignDto request);
        Task<Response<bool>> UpdateAsync(int id, CampaignDto request);
        Task<Response<List<CampaignDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CampaignDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveCampaignsResponseDto>>> GetActiveCampaignsAsync(bool campaignsIsActive);
        Task<Response<List<GetCouponsForCampaignsResponseDto>>> GetCouponsForCampaignsAsync(int campaignsId);
        Task<Response<GetCouponsForCampaignsResponseDto>> GetCouponsForCampaignsDetailsAsync(int campaignsId, int couponsId);
    }
}