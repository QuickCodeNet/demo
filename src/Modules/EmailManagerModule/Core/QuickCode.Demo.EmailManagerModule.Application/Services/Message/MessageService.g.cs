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
    public partial class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _logger;
        private readonly IMessageRepository _repository;
        public MessageService(ILogger<MessageService> logger, IMessageRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<MessageDto>> InsertAsync(MessageDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(MessageDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, MessageDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<MessageDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<MessageDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int messagesId)
        {
            var returnValue = await _repository.GetByIdAsync(messagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByCampaignResponseDto>>> GetByCampaignAsync(int messagesCampaignId)
        {
            var returnValue = await _repository.GetByCampaignAsync(messagesCampaignId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(MessageStatus messagesStatus)
        {
            var returnValue = await _repository.GetByStatusAsync(messagesStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetPendingMessagesResponseDto>>> GetPendingMessagesAsync(MessageStatus messagesStatus)
        {
            var returnValue = await _repository.GetPendingMessagesAsync(messagesStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetMessagesCountAsync(int messagesCampaignId)
        {
            var returnValue = await _repository.GetMessagesCountAsync(messagesCampaignId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessagesWithCampaignResponseDto>>> GetMessagesWithCampaignAsync(bool campaignsIsActive, MessageStatus messagesStatus)
        {
            var returnValue = await _repository.GetMessagesWithCampaignAsync(campaignsIsActive, messagesStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessageQueuesForMessagesResponseDto>>> GetMessageQueuesForMessagesAsync(int messagesId)
        {
            var returnValue = await _repository.GetMessageQueuesForMessagesAsync(messagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessageQueuesForMessagesResponseDto>> GetMessageQueuesForMessagesDetailsAsync(int messagesId, int messageQueuesId)
        {
            var returnValue = await _repository.GetMessageQueuesForMessagesDetailsAsync(messagesId, messageQueuesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessageLogsForMessagesResponseDto>>> GetMessageLogsForMessagesAsync(int messagesId)
        {
            var returnValue = await _repository.GetMessageLogsForMessagesAsync(messagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessageLogsForMessagesResponseDto>> GetMessageLogsForMessagesDetailsAsync(int messagesId, int messageLogsId)
        {
            var returnValue = await _repository.GetMessageLogsForMessagesDetailsAsync(messagesId, messageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int messagesId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(messagesId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> IncrementAttemptAsync(int messagesId, IncrementAttemptRequestDto updateRequest)
        {
            var returnValue = await _repository.IncrementAttemptAsync(messagesId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}