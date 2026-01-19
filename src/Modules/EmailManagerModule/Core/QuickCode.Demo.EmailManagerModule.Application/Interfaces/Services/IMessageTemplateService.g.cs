using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.MessageTemplate
{
    public partial interface IMessageTemplateService
    {
        Task<Response<MessageTemplateDto>> InsertAsync(MessageTemplateDto request);
        Task<Response<bool>> DeleteAsync(MessageTemplateDto request);
        Task<Response<bool>> UpdateAsync(string name, MessageTemplateDto request);
        Task<Response<List<MessageTemplateDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<MessageTemplateDto>> GetItemAsync(string name);
        Task<Response<bool>> DeleteItemAsync(string name);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByNameResponseDto>> GetByNameAsync(string messageTemplatesName);
        Task<Response<List<GetByTypeResponseDto>>> GetByTypeAsync(TemplateTypes messageTemplatesType);
        Task<Response<bool>> ExistsByNameAsync(string messageTemplatesName);
        Task<Response<List<GetCampaignsForMessageTemplatesResponseDto>>> GetCampaignsForMessageTemplatesAsync(string messageTemplatesName);
        Task<Response<GetCampaignsForMessageTemplatesResponseDto>> GetCampaignsForMessageTemplatesDetailsAsync(string messageTemplatesName, int campaignsId);
        Task<Response<List<GetMessagesForMessageTemplatesResponseDto>>> GetMessagesForMessageTemplatesAsync(string messageTemplatesName);
        Task<Response<GetMessagesForMessageTemplatesResponseDto>> GetMessagesForMessageTemplatesDetailsAsync(string messageTemplatesName, int messagesId);
        Task<Response<List<GetOtpMessagesForMessageTemplatesResponseDto>>> GetOtpMessagesForMessageTemplatesAsync(string messageTemplatesName);
        Task<Response<GetOtpMessagesForMessageTemplatesResponseDto>> GetOtpMessagesForMessageTemplatesDetailsAsync(string messageTemplatesName, int otpMessagesId);
        Task<Response<List<GetOtpMessageLogsForMessageTemplatesResponseDto>>> GetOtpMessageLogsForMessageTemplatesAsync(string messageTemplatesName);
        Task<Response<GetOtpMessageLogsForMessageTemplatesResponseDto>> GetOtpMessageLogsForMessageTemplatesDetailsAsync(string messageTemplatesName, int otpMessageLogsId);
        Task<Response<List<GetMessageLogsForMessageTemplatesResponseDto>>> GetMessageLogsForMessageTemplatesAsync(string messageTemplatesName);
        Task<Response<GetMessageLogsForMessageTemplatesResponseDto>> GetMessageLogsForMessageTemplatesDetailsAsync(string messageTemplatesName, int messageLogsId);
        Task<Response<int>> UpdateContentAsync(string messageTemplatesName, UpdateContentRequestDto updateRequest);
        Task<Response<int>> UpdateTypeAsync(string messageTemplatesName, UpdateTypeRequestDto updateRequest);
    }
}