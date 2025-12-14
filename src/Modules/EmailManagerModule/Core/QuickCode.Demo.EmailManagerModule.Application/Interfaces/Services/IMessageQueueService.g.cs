using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.MessageQueue;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.MessageQueue
{
    public partial interface IMessageQueueService
    {
        Task<Response<MessageQueueDto>> InsertAsync(MessageQueueDto request);
        Task<Response<bool>> DeleteAsync(MessageQueueDto request);
        Task<Response<bool>> UpdateAsync(int id, MessageQueueDto request);
        Task<Response<List<MessageQueueDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<MessageQueueDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int messageQueuesId);
        Task<Response<List<GetByCampaignResponseDto>>> GetByCampaignAsync(int messageQueuesCampaignId);
        Task<Response<List<GetPendingQueueResponseDto>>> GetPendingQueueAsync(MessageStatus messageQueuesStatus);
        Task<Response<long>> GetQueueCountAsync(int messageQueuesCampaignId);
        Task<Response<List<GetQueueDetailsResponseDto>>> GetQueueDetailsAsync(MessageStatus messageQueuesStatus, bool campaignsIsActive);
        Task<Response<int>> UpdateStatusAsync(int messageQueuesId, UpdateStatusRequestDto updateRequest);
        Task<Response<int>> UpdatePriorityAsync(int messageQueuesId, UpdatePriorityRequestDto updateRequest);
    }
}