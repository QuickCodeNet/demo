using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.SmsSender;

namespace QuickCode.Demo.SmsManagerModule.Application.Services.SmsSender
{
    public partial interface ISmsSenderService
    {
        Task<Response<SmsSenderDto>> InsertAsync(SmsSenderDto request);
        Task<Response<bool>> DeleteAsync(SmsSenderDto request);
        Task<Response<bool>> UpdateAsync(int id, SmsSenderDto request);
        Task<Response<List<SmsSenderDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SmsSenderDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetInfoMessagesForSmsSendersResponseDto>>> GetInfoMessagesForSmsSendersAsync(int smsSendersId);
        Task<Response<GetInfoMessagesForSmsSendersResponseDto>> GetInfoMessagesForSmsSendersDetailsAsync(int smsSendersId, int infoMessagesId);
        Task<Response<List<GetOtpMessagesForSmsSendersResponseDto>>> GetOtpMessagesForSmsSendersAsync(int smsSendersId);
        Task<Response<GetOtpMessagesForSmsSendersResponseDto>> GetOtpMessagesForSmsSendersDetailsAsync(int smsSendersId, int otpMessagesId);
        Task<Response<List<GetCampaignMessagesForSmsSendersResponseDto>>> GetCampaignMessagesForSmsSendersAsync(int smsSendersId);
        Task<Response<GetCampaignMessagesForSmsSendersResponseDto>> GetCampaignMessagesForSmsSendersDetailsAsync(int smsSendersId, int campaignMessagesId);
    }
}