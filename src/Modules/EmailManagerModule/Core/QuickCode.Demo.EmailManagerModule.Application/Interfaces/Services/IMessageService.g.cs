using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.Message;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.Message
{
    public partial interface IMessageService
    {
        Task<Response<MessageDto>> InsertAsync(MessageDto request);
        Task<Response<bool>> DeleteAsync(MessageDto request);
        Task<Response<bool>> UpdateAsync(int id, MessageDto request);
        Task<Response<List<MessageDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<MessageDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int messagesId);
        Task<Response<List<GetByCampaignResponseDto>>> GetByCampaignAsync(int messagesCampaignId);
        Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(MessageStatus messagesStatus);
        Task<Response<List<GetPendingMessagesResponseDto>>> GetPendingMessagesAsync(MessageStatus messagesStatus);
        Task<Response<long>> GetMessagesCountAsync(int messagesCampaignId);
        Task<Response<List<GetMessagesWithCampaignResponseDto>>> GetMessagesWithCampaignAsync(bool campaignsIsActive, MessageStatus messagesStatus);
        Task<Response<List<GetMessageQueuesForMessagesResponseDto>>> GetMessageQueuesForMessagesAsync(int messagesId);
        Task<Response<GetMessageQueuesForMessagesResponseDto>> GetMessageQueuesForMessagesDetailsAsync(int messagesId, int messageQueuesId);
        Task<Response<List<GetMessageLogsForMessagesResponseDto>>> GetMessageLogsForMessagesAsync(int messagesId);
        Task<Response<GetMessageLogsForMessagesResponseDto>> GetMessageLogsForMessagesDetailsAsync(int messagesId, int messageLogsId);
        Task<Response<int>> UpdateStatusAsync(int messagesId, UpdateStatusRequestDto updateRequest);
        Task<Response<int>> IncrementAttemptAsync(int messagesId, IncrementAttemptRequestDto updateRequest);
    }
}