using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.MessageLog;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.MessageLog
{
    public partial interface IMessageLogService
    {
        Task<Response<MessageLogDto>> InsertAsync(MessageLogDto request);
        Task<Response<bool>> DeleteAsync(MessageLogDto request);
        Task<Response<bool>> UpdateAsync(int id, MessageLogDto request);
        Task<Response<List<MessageLogDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<MessageLogDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int messageLogsId);
        Task<Response<List<GetByMessageResponseDto>>> GetByMessageAsync(int messageLogsMessageId);
        Task<Response<List<GetByCampaignResponseDto>>> GetByCampaignAsync(int messageLogsCampaignId);
        Task<Response<List<GetBySenderResponseDto>>> GetBySenderAsync(int? messageLogsSenderId);
        Task<Response<long>> GetLogsCountAsync(int messageLogsCampaignId);
        Task<Response<List<GetLogsWithSenderResponseDto>>> GetLogsWithSenderAsync(bool sendersIsActive);
    }
}