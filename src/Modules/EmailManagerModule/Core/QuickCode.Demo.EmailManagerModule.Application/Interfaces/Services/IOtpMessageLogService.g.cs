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
    public partial interface IOtpMessageLogService
    {
        Task<Response<OtpMessageLogDto>> InsertAsync(OtpMessageLogDto request);
        Task<Response<bool>> DeleteAsync(OtpMessageLogDto request);
        Task<Response<bool>> UpdateAsync(int id, OtpMessageLogDto request);
        Task<Response<List<OtpMessageLogDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<OtpMessageLogDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int otpMessageLogsId);
        Task<Response<List<GetByOtpMessageResponseDto>>> GetByOtpMessageAsync(int otpMessageLogsOtpMessageId);
        Task<Response<List<GetBySenderResponseDto>>> GetBySenderAsync(int? otpMessageLogsSenderId);
        Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(MessageStatus otpMessageLogsStatus);
        Task<Response<List<GetOtpLogsWithMessageResponseDto>>> GetOtpLogsWithMessageAsync(MessageStatus otpMessageLogsStatus);
        Task<Response<int>> UpdateStatusAsync(int otpMessageLogsId, UpdateStatusRequestDto updateRequest);
    }
}