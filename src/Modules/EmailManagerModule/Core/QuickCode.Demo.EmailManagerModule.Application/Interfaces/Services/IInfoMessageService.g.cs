using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.InfoMessage;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.InfoMessage
{
    public partial interface IInfoMessageService
    {
        Task<Response<InfoMessageDto>> InsertAsync(InfoMessageDto request);
        Task<Response<bool>> DeleteAsync(InfoMessageDto request);
        Task<Response<bool>> UpdateAsync(int id, InfoMessageDto request);
        Task<Response<List<InfoMessageDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<InfoMessageDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
    }
}