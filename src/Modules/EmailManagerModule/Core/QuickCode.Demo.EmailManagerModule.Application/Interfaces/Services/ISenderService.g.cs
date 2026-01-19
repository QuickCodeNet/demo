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
    public partial interface ISenderService
    {
        Task<Response<SenderDto>> InsertAsync(SenderDto request);
        Task<Response<bool>> DeleteAsync(SenderDto request);
        Task<Response<bool>> UpdateAsync(int id, SenderDto request);
        Task<Response<List<SenderDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SenderDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int sendersId);
        Task<Response<List<GetActiveSendersResponseDto>>> GetActiveSendersAsync(bool sendersIsActive);
        Task<Response<GetByNameResponseDto>> GetByNameAsync(string sendersName);
        Task<Response<bool>> ExistsByEmailAsync(string sendersEmailAddress);
        Task<Response<List<GetMessageLogsForSendersResponseDto>>> GetMessageLogsForSendersAsync(int sendersId);
        Task<Response<GetMessageLogsForSendersResponseDto>> GetMessageLogsForSendersDetailsAsync(int sendersId, int messageLogsId);
        Task<Response<List<GetOtpMessageLogsForSendersResponseDto>>> GetOtpMessageLogsForSendersAsync(int sendersId);
        Task<Response<GetOtpMessageLogsForSendersResponseDto>> GetOtpMessageLogsForSendersDetailsAsync(int sendersId, int otpMessageLogsId);
        Task<Response<List<GetMessageQueuesForSendersResponseDto>>> GetMessageQueuesForSendersAsync(int sendersId);
        Task<Response<GetMessageQueuesForSendersResponseDto>> GetMessageQueuesForSendersDetailsAsync(int sendersId, int messageQueuesId);
        Task<Response<List<GetOtpMessageQueuesForSendersResponseDto>>> GetOtpMessageQueuesForSendersAsync(int sendersId);
        Task<Response<GetOtpMessageQueuesForSendersResponseDto>> GetOtpMessageQueuesForSendersDetailsAsync(int sendersId, int otpMessageQueuesId);
        Task<Response<int>> UpdateStatusAsync(int sendersId, UpdateStatusRequestDto updateRequest);
        Task<Response<int>> UpdatePriorityAsync(int sendersId, UpdatePriorityRequestDto updateRequest);
        Task<Response<int>> UpdateDailyLimitAsync(int sendersId, UpdateDailyLimitRequestDto updateRequest);
    }
}