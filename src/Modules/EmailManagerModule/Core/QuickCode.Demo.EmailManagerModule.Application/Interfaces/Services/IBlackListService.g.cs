using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.BlackList;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.BlackList
{
    public partial interface IBlackListService
    {
        Task<Response<BlackListDto>> InsertAsync(BlackListDto request);
        Task<Response<bool>> DeleteAsync(BlackListDto request);
        Task<Response<bool>> UpdateAsync(int id, BlackListDto request);
        Task<Response<List<BlackListDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<BlackListDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByIdResponseDto>> GetByIdAsync(int blackListsId);
        Task<Response<GetByRecipientResponseDto>> GetByRecipientAsync(string blackListsRecipient);
        Task<Response<bool>> ExistsByRecipientAsync(string blackListsRecipient);
        Task<Response<long>> GetBlacklistCountAsync(BlacklistReasonType blackListsReasonType);
        Task<Response<int>> AddToBlacklistAsync(string blackListsRecipient, AddToBlacklistRequestDto updateRequest);
        Task<Response<int>> RemoveFromBlacklistAsync(int blackListsId);
    }
}