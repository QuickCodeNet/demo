using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.OtpMessageQueue
{
    public partial class OtpMessageQueueService : IOtpMessageQueueService
    {
        private readonly ILogger<OtpMessageQueueService> _logger;
        private readonly IOtpMessageQueueRepository _repository;
        public OtpMessageQueueService(ILogger<OtpMessageQueueService> logger, IOtpMessageQueueRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<OtpMessageQueueDto>> InsertAsync(OtpMessageQueueDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(OtpMessageQueueDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, OtpMessageQueueDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<OtpMessageQueueDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<OtpMessageQueueDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int otpMessageQueuesId)
        {
            var returnValue = await _repository.GetByIdAsync(otpMessageQueuesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByOtpMessageResponseDto>>> GetByOtpMessageAsync(int otpMessageQueuesOtpMessageId)
        {
            var returnValue = await _repository.GetByOtpMessageAsync(otpMessageQueuesOtpMessageId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetPendingQueueResponseDto>>> GetPendingQueueAsync(MessageStatus otpMessageQueuesStatus)
        {
            var returnValue = await _repository.GetPendingQueueAsync(otpMessageQueuesStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int otpMessageQueuesId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(otpMessageQueuesId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdatePriorityAsync(int otpMessageQueuesId, UpdatePriorityRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdatePriorityAsync(otpMessageQueuesId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}