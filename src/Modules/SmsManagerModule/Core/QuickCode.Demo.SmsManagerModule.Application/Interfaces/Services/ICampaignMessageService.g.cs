using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.CampaignMessage;

namespace QuickCode.Demo.SmsManagerModule.Application.Services.CampaignMessage
{
    public partial interface ICampaignMessageService
    {
        Task<Response<CampaignMessageDto>> InsertAsync(CampaignMessageDto request);
        Task<Response<bool>> DeleteAsync(CampaignMessageDto request);
        Task<Response<bool>> UpdateAsync(int id, CampaignMessageDto request);
        Task<Response<List<CampaignMessageDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CampaignMessageDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
    }
}