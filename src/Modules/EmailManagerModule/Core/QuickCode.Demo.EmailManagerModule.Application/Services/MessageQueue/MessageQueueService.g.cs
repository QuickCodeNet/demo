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
    public partial class MessageQueueService : IMessageQueueService
    {
        private readonly ILogger<MessageQueueService> _logger;
        private readonly IMessageQueueRepository _repository;
        public MessageQueueService(ILogger<MessageQueueService> logger, IMessageQueueRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<MessageQueueDto>> InsertAsync(MessageQueueDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(MessageQueueDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, MessageQueueDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<MessageQueueDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<MessageQueueDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int messageQueuesId)
        {
            var returnValue = await _repository.GetByIdAsync(messageQueuesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByCampaignResponseDto>>> GetByCampaignAsync(int messageQueuesCampaignId)
        {
            var returnValue = await _repository.GetByCampaignAsync(messageQueuesCampaignId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetPendingQueueResponseDto>>> GetPendingQueueAsync(MessageStatus messageQueuesStatus)
        {
            var returnValue = await _repository.GetPendingQueueAsync(messageQueuesStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetQueueCountAsync(int messageQueuesCampaignId)
        {
            var returnValue = await _repository.GetQueueCountAsync(messageQueuesCampaignId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetQueueDetailsResponseDto>>> GetQueueDetailsAsync(MessageStatus messageQueuesStatus, bool campaignsIsActive)
        {
            var returnValue = await _repository.GetQueueDetailsAsync(messageQueuesStatus, campaignsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int messageQueuesId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(messageQueuesId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdatePriorityAsync(int messageQueuesId, UpdatePriorityRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdatePriorityAsync(messageQueuesId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}