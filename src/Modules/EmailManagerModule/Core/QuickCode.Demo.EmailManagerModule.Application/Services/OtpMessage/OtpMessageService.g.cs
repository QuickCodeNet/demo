using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.OtpMessage
{
    public partial class OtpMessageService : IOtpMessageService
    {
        private readonly ILogger<OtpMessageService> _logger;
        private readonly IOtpMessageRepository _repository;
        public OtpMessageService(ILogger<OtpMessageService> logger, IOtpMessageRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<OtpMessageDto>> InsertAsync(OtpMessageDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(OtpMessageDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, OtpMessageDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<OtpMessageDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<OtpMessageDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int otpMessagesId)
        {
            var returnValue = await _repository.GetByIdAsync(otpMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByRecipientResponseDto>>> GetByRecipientAsync(string otpMessagesRecipient)
        {
            var returnValue = await _repository.GetByRecipientAsync(otpMessagesRecipient);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> ExistsByHashAsync(string otpMessagesHashCode)
        {
            var returnValue = await _repository.ExistsByHashAsync(otpMessagesHashCode);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessageQueuesForOtpMessagesResponseDto>>> GetOtpMessageQueuesForOtpMessagesAsync(int otpMessagesId)
        {
            var returnValue = await _repository.GetOtpMessageQueuesForOtpMessagesAsync(otpMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessageQueuesForOtpMessagesResponseDto>> GetOtpMessageQueuesForOtpMessagesDetailsAsync(int otpMessagesId, int otpMessageQueuesId)
        {
            var returnValue = await _repository.GetOtpMessageQueuesForOtpMessagesDetailsAsync(otpMessagesId, otpMessageQueuesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessageLogsForOtpMessagesResponseDto>>> GetOtpMessageLogsForOtpMessagesAsync(int otpMessagesId)
        {
            var returnValue = await _repository.GetOtpMessageLogsForOtpMessagesAsync(otpMessagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessageLogsForOtpMessagesResponseDto>> GetOtpMessageLogsForOtpMessagesDetailsAsync(int otpMessagesId, int otpMessageLogsId)
        {
            var returnValue = await _repository.GetOtpMessageLogsForOtpMessagesDetailsAsync(otpMessagesId, otpMessageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int otpMessagesId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(otpMessagesId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> IncrementAttemptAsync(int otpMessagesId, IncrementAttemptRequestDto updateRequest)
        {
            var returnValue = await _repository.IncrementAttemptAsync(otpMessagesId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}