using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.OtpMessageLog;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.OtpMessageLog
{
    public partial class OtpMessageLogService : IOtpMessageLogService
    {
        private readonly ILogger<OtpMessageLogService> _logger;
        private readonly IOtpMessageLogRepository _repository;
        public OtpMessageLogService(ILogger<OtpMessageLogService> logger, IOtpMessageLogRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<OtpMessageLogDto>> InsertAsync(OtpMessageLogDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(OtpMessageLogDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, OtpMessageLogDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<OtpMessageLogDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<OtpMessageLogDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int otpMessageLogsId)
        {
            var returnValue = await _repository.GetByIdAsync(otpMessageLogsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByOtpMessageResponseDto>>> GetByOtpMessageAsync(int otpMessageLogsOtpMessageId)
        {
            var returnValue = await _repository.GetByOtpMessageAsync(otpMessageLogsOtpMessageId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetBySenderResponseDto>>> GetBySenderAsync(int? otpMessageLogsSenderId)
        {
            var returnValue = await _repository.GetBySenderAsync(otpMessageLogsSenderId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(MessageStatus otpMessageLogsStatus)
        {
            var returnValue = await _repository.GetByStatusAsync(otpMessageLogsStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpLogsWithMessageResponseDto>>> GetOtpLogsWithMessageAsync(MessageStatus otpMessageLogsStatus)
        {
            var returnValue = await _repository.GetOtpLogsWithMessageAsync(otpMessageLogsStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int otpMessageLogsId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(otpMessageLogsId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}