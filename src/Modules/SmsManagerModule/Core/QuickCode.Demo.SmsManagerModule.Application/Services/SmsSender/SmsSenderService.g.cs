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
    public partial class SmsSenderService : ISmsSenderService
    {
        private readonly ILogger<SmsSenderService> _logger;
        private readonly ISmsSenderRepository _repository;
        public SmsSenderService(ILogger<SmsSenderService> logger, ISmsSenderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SmsSenderDto>> InsertAsync(SmsSenderDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SmsSenderDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SmsSenderDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SmsSenderDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SmsSenderDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetInfoMessagesForSmsSendersResponseDto>>> GetInfoMessagesForSmsSendersAsync(int smsSendersId)
        {
            var returnValue = await _repository.GetInfoMessagesForSmsSendersAsync(smsSendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetInfoMessagesForSmsSendersResponseDto>> GetInfoMessagesForSmsSendersDetailsAsync(int smsSendersId, int infoMessagesId)
        {
            var returnValue = await _repository.GetInfoMessagesForSmsSendersDetailsAsync(smsSendersId, infoMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessagesForSmsSendersResponseDto>>> GetOtpMessagesForSmsSendersAsync(int smsSendersId)
        {
            var returnValue = await _repository.GetOtpMessagesForSmsSendersAsync(smsSendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessagesForSmsSendersResponseDto>> GetOtpMessagesForSmsSendersDetailsAsync(int smsSendersId, int otpMessagesId)
        {
            var returnValue = await _repository.GetOtpMessagesForSmsSendersDetailsAsync(smsSendersId, otpMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCampaignMessagesForSmsSendersResponseDto>>> GetCampaignMessagesForSmsSendersAsync(int smsSendersId)
        {
            var returnValue = await _repository.GetCampaignMessagesForSmsSendersAsync(smsSendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCampaignMessagesForSmsSendersResponseDto>> GetCampaignMessagesForSmsSendersDetailsAsync(int smsSendersId, int campaignMessagesId)
        {
            var returnValue = await _repository.GetCampaignMessagesForSmsSendersDetailsAsync(smsSendersId, campaignMessagesId);
            return returnValue.ToResponse();
        }
    }
}