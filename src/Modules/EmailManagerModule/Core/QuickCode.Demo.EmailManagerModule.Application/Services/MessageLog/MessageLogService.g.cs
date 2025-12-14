using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.MessageLog;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.MessageLog
{
    public partial class MessageLogService : IMessageLogService
    {
        private readonly ILogger<MessageLogService> _logger;
        private readonly IMessageLogRepository _repository;
        public MessageLogService(ILogger<MessageLogService> logger, IMessageLogRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<MessageLogDto>> InsertAsync(MessageLogDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(MessageLogDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, MessageLogDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<MessageLogDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<MessageLogDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int messageLogsId)
        {
            var returnValue = await _repository.GetByIdAsync(messageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByMessageResponseDto>>> GetByMessageAsync(int messageLogsMessageId)
        {
            var returnValue = await _repository.GetByMessageAsync(messageLogsMessageId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByCampaignResponseDto>>> GetByCampaignAsync(int messageLogsCampaignId)
        {
            var returnValue = await _repository.GetByCampaignAsync(messageLogsCampaignId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetBySenderResponseDto>>> GetBySenderAsync(int? messageLogsSenderId)
        {
            var returnValue = await _repository.GetBySenderAsync(messageLogsSenderId);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetLogsCountAsync(int messageLogsCampaignId)
        {
            var returnValue = await _repository.GetLogsCountAsync(messageLogsCampaignId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetLogsWithSenderResponseDto>>> GetLogsWithSenderAsync(bool sendersIsActive)
        {
            var returnValue = await _repository.GetLogsWithSenderAsync(sendersIsActive);
            return returnValue.ToResponse();
        }
    }
}