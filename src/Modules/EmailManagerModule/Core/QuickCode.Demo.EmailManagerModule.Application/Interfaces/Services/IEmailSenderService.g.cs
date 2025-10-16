using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.EmailSender;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.EmailSender
{
    public partial interface IEmailSenderService
    {
        Task<Response<EmailSenderDto>> InsertAsync(EmailSenderDto request);
        Task<Response<bool>> DeleteAsync(EmailSenderDto request);
        Task<Response<bool>> UpdateAsync(int id, EmailSenderDto request);
        Task<Response<List<EmailSenderDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<EmailSenderDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetInfoMessagesForEmailSendersResponseDto>>> GetInfoMessagesForEmailSendersAsync(int emailSendersId);
        Task<Response<GetInfoMessagesForEmailSendersResponseDto>> GetInfoMessagesForEmailSendersDetailsAsync(int emailSendersId, int infoMessagesId);
        Task<Response<List<GetOtpMessagesForEmailSendersResponseDto>>> GetOtpMessagesForEmailSendersAsync(int emailSendersId);
        Task<Response<GetOtpMessagesForEmailSendersResponseDto>> GetOtpMessagesForEmailSendersDetailsAsync(int emailSendersId, int otpMessagesId);
        Task<Response<List<GetCampaignMessagesForEmailSendersResponseDto>>> GetCampaignMessagesForEmailSendersAsync(int emailSendersId);
        Task<Response<GetCampaignMessagesForEmailSendersResponseDto>> GetCampaignMessagesForEmailSendersDetailsAsync(int emailSendersId, int campaignMessagesId);
    }
}