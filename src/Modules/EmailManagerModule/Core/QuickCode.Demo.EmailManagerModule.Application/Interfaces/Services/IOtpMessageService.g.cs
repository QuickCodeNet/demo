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
    public partial interface IOtpMessageService
    {
        Task<Response<OtpMessageDto>> InsertAsync(OtpMessageDto request);
        Task<Response<bool>> DeleteAsync(OtpMessageDto request);
        Task<Response<bool>> UpdateAsync(int id, OtpMessageDto request);
        Task<Response<List<OtpMessageDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<OtpMessageDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int otpMessagesId);
        Task<Response<List<GetByRecipientResponseDto>>> GetByRecipientAsync(string otpMessagesRecipient);
        Task<Response<bool>> ExistsByHashAsync(string otpMessagesHashCode);
        Task<Response<List<GetOtpMessageQueuesForOtpMessagesResponseDto>>> GetOtpMessageQueuesForOtpMessagesAsync(int otpMessagesId);
        Task<Response<GetOtpMessageQueuesForOtpMessagesResponseDto>> GetOtpMessageQueuesForOtpMessagesDetailsAsync(int otpMessagesId, int otpMessageQueuesId);
        Task<Response<List<GetOtpMessageLogsForOtpMessagesResponseDto>>> GetOtpMessageLogsForOtpMessagesAsync(int otpMessagesId);
        Task<Response<GetOtpMessageLogsForOtpMessagesResponseDto>> GetOtpMessageLogsForOtpMessagesDetailsAsync(int otpMessagesId, int otpMessageLogsId);
        Task<Response<int>> UpdateStatusAsync(int otpMessagesId, UpdateStatusRequestDto updateRequest);
        Task<Response<int>> IncrementAttemptAsync(int otpMessagesId, IncrementAttemptRequestDto updateRequest);
    }
}