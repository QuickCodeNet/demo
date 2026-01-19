using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.Sender
{
    public partial class SenderService : ISenderService
    {
        private readonly ILogger<SenderService> _logger;
        private readonly ISenderRepository _repository;
        public SenderService(ILogger<SenderService> logger, ISenderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SenderDto>> InsertAsync(SenderDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SenderDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SenderDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SenderDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SenderDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int sendersId)
        {
            var returnValue = await _repository.GetByIdAsync(sendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveSendersResponseDto>>> GetActiveSendersAsync(bool sendersIsActive)
        {
            var returnValue = await _repository.GetActiveSendersAsync(sendersIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetByNameResponseDto>> GetByNameAsync(string sendersName)
        {
            var returnValue = await _repository.GetByNameAsync(sendersName);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> ExistsByEmailAsync(string sendersEmailAddress)
        {
            var returnValue = await _repository.ExistsByEmailAsync(sendersEmailAddress);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessageLogsForSendersResponseDto>>> GetMessageLogsForSendersAsync(int sendersId)
        {
            var returnValue = await _repository.GetMessageLogsForSendersAsync(sendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessageLogsForSendersResponseDto>> GetMessageLogsForSendersDetailsAsync(int sendersId, int messageLogsId)
        {
            var returnValue = await _repository.GetMessageLogsForSendersDetailsAsync(sendersId, messageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessageLogsForSendersResponseDto>>> GetOtpMessageLogsForSendersAsync(int sendersId)
        {
            var returnValue = await _repository.GetOtpMessageLogsForSendersAsync(sendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessageLogsForSendersResponseDto>> GetOtpMessageLogsForSendersDetailsAsync(int sendersId, int otpMessageLogsId)
        {
            var returnValue = await _repository.GetOtpMessageLogsForSendersDetailsAsync(sendersId, otpMessageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessageQueuesForSendersResponseDto>>> GetMessageQueuesForSendersAsync(int sendersId)
        {
            var returnValue = await _repository.GetMessageQueuesForSendersAsync(sendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessageQueuesForSendersResponseDto>> GetMessageQueuesForSendersDetailsAsync(int sendersId, int messageQueuesId)
        {
            var returnValue = await _repository.GetMessageQueuesForSendersDetailsAsync(sendersId, messageQueuesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessageQueuesForSendersResponseDto>>> GetOtpMessageQueuesForSendersAsync(int sendersId)
        {
            var returnValue = await _repository.GetOtpMessageQueuesForSendersAsync(sendersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessageQueuesForSendersResponseDto>> GetOtpMessageQueuesForSendersDetailsAsync(int sendersId, int otpMessageQueuesId)
        {
            var returnValue = await _repository.GetOtpMessageQueuesForSendersDetailsAsync(sendersId, otpMessageQueuesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int sendersId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(sendersId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdatePriorityAsync(int sendersId, UpdatePriorityRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdatePriorityAsync(sendersId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateDailyLimitAsync(int sendersId, UpdateDailyLimitRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateDailyLimitAsync(sendersId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}