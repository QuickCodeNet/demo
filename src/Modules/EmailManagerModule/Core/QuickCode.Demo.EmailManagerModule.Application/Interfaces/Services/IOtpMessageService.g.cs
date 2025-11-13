using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.OtpMessage;

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
    }
}