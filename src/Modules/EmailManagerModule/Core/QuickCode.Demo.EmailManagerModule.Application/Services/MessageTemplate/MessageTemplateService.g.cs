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
    public partial class MessageTemplateService : IMessageTemplateService
    {
        private readonly ILogger<MessageTemplateService> _logger;
        private readonly IMessageTemplateRepository _repository;
        public MessageTemplateService(ILogger<MessageTemplateService> logger, IMessageTemplateRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<MessageTemplateDto>> InsertAsync(MessageTemplateDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(MessageTemplateDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(string name, MessageTemplateDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Name);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<MessageTemplateDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<MessageTemplateDto>> GetItemAsync(string name)
        {
            var returnValue = await _repository.GetByPkAsync(name);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(string name)
        {
            var deleteItem = await _repository.GetByPkAsync(name);
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

        public async Task<Response<GetByNameResponseDto>> GetByNameAsync(string messageTemplatesName)
        {
            var returnValue = await _repository.GetByNameAsync(messageTemplatesName);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByTypeResponseDto>>> GetByTypeAsync(TemplateTypes messageTemplatesType)
        {
            var returnValue = await _repository.GetByTypeAsync(messageTemplatesType);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> ExistsByNameAsync(string messageTemplatesName)
        {
            var returnValue = await _repository.ExistsByNameAsync(messageTemplatesName);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCampaignsForMessageTemplatesResponseDto>>> GetCampaignsForMessageTemplatesAsync(string messageTemplatesName)
        {
            var returnValue = await _repository.GetCampaignsForMessageTemplatesAsync(messageTemplatesName);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCampaignsForMessageTemplatesResponseDto>> GetCampaignsForMessageTemplatesDetailsAsync(string messageTemplatesName, int campaignsId)
        {
            var returnValue = await _repository.GetCampaignsForMessageTemplatesDetailsAsync(messageTemplatesName, campaignsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessagesForMessageTemplatesResponseDto>>> GetMessagesForMessageTemplatesAsync(string messageTemplatesName)
        {
            var returnValue = await _repository.GetMessagesForMessageTemplatesAsync(messageTemplatesName);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessagesForMessageTemplatesResponseDto>> GetMessagesForMessageTemplatesDetailsAsync(string messageTemplatesName, int messagesId)
        {
            var returnValue = await _repository.GetMessagesForMessageTemplatesDetailsAsync(messageTemplatesName, messagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessagesForMessageTemplatesResponseDto>>> GetOtpMessagesForMessageTemplatesAsync(string messageTemplatesName)
        {
            var returnValue = await _repository.GetOtpMessagesForMessageTemplatesAsync(messageTemplatesName);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessagesForMessageTemplatesResponseDto>> GetOtpMessagesForMessageTemplatesDetailsAsync(string messageTemplatesName, int otpMessagesId)
        {
            var returnValue = await _repository.GetOtpMessagesForMessageTemplatesDetailsAsync(messageTemplatesName, otpMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessageLogsForMessageTemplatesResponseDto>>> GetOtpMessageLogsForMessageTemplatesAsync(string messageTemplatesName)
        {
            var returnValue = await _repository.GetOtpMessageLogsForMessageTemplatesAsync(messageTemplatesName);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessageLogsForMessageTemplatesResponseDto>> GetOtpMessageLogsForMessageTemplatesDetailsAsync(string messageTemplatesName, int otpMessageLogsId)
        {
            var returnValue = await _repository.GetOtpMessageLogsForMessageTemplatesDetailsAsync(messageTemplatesName, otpMessageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessageLogsForMessageTemplatesResponseDto>>> GetMessageLogsForMessageTemplatesAsync(string messageTemplatesName)
        {
            var returnValue = await _repository.GetMessageLogsForMessageTemplatesAsync(messageTemplatesName);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessageLogsForMessageTemplatesResponseDto>> GetMessageLogsForMessageTemplatesDetailsAsync(string messageTemplatesName, int messageLogsId)
        {
            var returnValue = await _repository.GetMessageLogsForMessageTemplatesDetailsAsync(messageTemplatesName, messageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateContentAsync(string messageTemplatesName, UpdateContentRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateContentAsync(messageTemplatesName, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateTypeAsync(string messageTemplatesName, UpdateTypeRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateTypeAsync(messageTemplatesName, updateRequest);
            return returnValue.ToResponse();
        }
    }
}