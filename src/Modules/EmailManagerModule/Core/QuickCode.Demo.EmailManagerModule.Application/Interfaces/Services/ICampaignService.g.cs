using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.Campaign;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.Campaign
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
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int campaignsId);
        Task<Response<List<GetActiveCampaignsResponseDto>>> GetActiveCampaignsAsync(bool campaignsIsActive);
        Task<Response<GetByNameResponseDto>> GetByNameAsync(string campaignsName);
        Task<Response<bool>> ExistsByNameAsync(string campaignsName);
        Task<Response<long>> GetCampaignsCountAsync(bool campaignsIsActive);
        Task<Response<List<GetMessagesForCampaignsResponseDto>>> GetMessagesForCampaignsAsync(int campaignsId);
        Task<Response<GetMessagesForCampaignsResponseDto>> GetMessagesForCampaignsDetailsAsync(int campaignsId, int messagesId);
        Task<Response<List<GetMessageQueuesForCampaignsResponseDto>>> GetMessageQueuesForCampaignsAsync(int campaignsId);
        Task<Response<GetMessageQueuesForCampaignsResponseDto>> GetMessageQueuesForCampaignsDetailsAsync(int campaignsId, int messageQueuesId);
        Task<Response<int>> UpdateStatusAsync(int campaignsId, UpdateStatusRequestDto updateRequest);
        Task<Response<int>> UpdatePriorityAsync(int campaignsId, UpdatePriorityRequestDto updateRequest);
    }
}