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
    public partial interface IOtpMessageQueueService
    {
        Task<Response<OtpMessageQueueDto>> InsertAsync(OtpMessageQueueDto request);
        Task<Response<bool>> DeleteAsync(OtpMessageQueueDto request);
        Task<Response<bool>> UpdateAsync(int id, OtpMessageQueueDto request);
        Task<Response<List<OtpMessageQueueDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<OtpMessageQueueDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int otpMessageQueuesId);
        Task<Response<List<GetByOtpMessageResponseDto>>> GetByOtpMessageAsync(int otpMessageQueuesOtpMessageId);
        Task<Response<List<GetPendingQueueResponseDto>>> GetPendingQueueAsync(MessageStatus otpMessageQueuesStatus);
        Task<Response<int>> UpdateStatusAsync(int otpMessageQueuesId, UpdateStatusRequestDto updateRequest);
        Task<Response<int>> UpdatePriorityAsync(int otpMessageQueuesId, UpdatePriorityRequestDto updateRequest);
    }
}