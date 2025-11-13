using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;

namespace QuickCode.Demo.SmsManagerModule.Application.Services.OtpMessage
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
    }
}