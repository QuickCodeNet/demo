using System;
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
    public partial class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly IEmailSenderRepository _repository;
        public EmailSenderService(ILogger<EmailSenderService> logger, IEmailSenderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<EmailSenderDto>> InsertAsync(EmailSenderDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(EmailSenderDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, EmailSenderDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<EmailSenderDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<EmailSenderDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetInfoMessagesForEmailSendersResponseDto>>> GetInfoMessagesForEmailSendersAsync(int emailSendersId)
        {
            var returnValue = await _repository.GetInfoMessagesForEmailSendersAsync(emailSendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetInfoMessagesForEmailSendersResponseDto>> GetInfoMessagesForEmailSendersDetailsAsync(int emailSendersId, int infoMessagesId)
        {
            var returnValue = await _repository.GetInfoMessagesForEmailSendersDetailsAsync(emailSendersId, infoMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessagesForEmailSendersResponseDto>>> GetOtpMessagesForEmailSendersAsync(int emailSendersId)
        {
            var returnValue = await _repository.GetOtpMessagesForEmailSendersAsync(emailSendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessagesForEmailSendersResponseDto>> GetOtpMessagesForEmailSendersDetailsAsync(int emailSendersId, int otpMessagesId)
        {
            var returnValue = await _repository.GetOtpMessagesForEmailSendersDetailsAsync(emailSendersId, otpMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCampaignMessagesForEmailSendersResponseDto>>> GetCampaignMessagesForEmailSendersAsync(int emailSendersId)
        {
            var returnValue = await _repository.GetCampaignMessagesForEmailSendersAsync(emailSendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCampaignMessagesForEmailSendersResponseDto>> GetCampaignMessagesForEmailSendersDetailsAsync(int emailSendersId, int campaignMessagesId)
        {
            var returnValue = await _repository.GetCampaignMessagesForEmailSendersDetailsAsync(emailSendersId, campaignMessagesId);
            return returnValue.ToResponse();
        }
    }
}