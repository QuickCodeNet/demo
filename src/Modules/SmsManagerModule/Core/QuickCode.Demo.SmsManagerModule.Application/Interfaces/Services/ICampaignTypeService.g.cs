using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.CampaignType;

namespace QuickCode.Demo.SmsManagerModule.Application.Services.CampaignType
{
    public partial interface ICampaignTypeService
    {
        Task<Response<CampaignTypeDto>> InsertAsync(CampaignTypeDto request);
        Task<Response<bool>> DeleteAsync(CampaignTypeDto request);
        Task<Response<bool>> UpdateAsync(int id, CampaignTypeDto request);
        Task<Response<List<CampaignTypeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CampaignTypeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetCampaignMessagesForCampaignTypesResponseDto>>> GetCampaignMessagesForCampaignTypesAsync(int campaignTypesId);
        Task<Response<GetCampaignMessagesForCampaignTypesResponseDto>> GetCampaignMessagesForCampaignTypesDetailsAsync(int campaignTypesId, int campaignMessagesId);
    }
}