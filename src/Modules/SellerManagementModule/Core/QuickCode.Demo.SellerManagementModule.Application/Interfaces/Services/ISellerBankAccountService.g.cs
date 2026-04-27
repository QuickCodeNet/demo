using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SellerManagementModule.Domain.Entities;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.SellerBankAccount;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Application.Services.SellerBankAccount
{
    public partial interface ISellerBankAccountService
    {
        Task<Response<SellerBankAccountDto>> InsertAsync(SellerBankAccountDto request);
        Task<Response<bool>> DeleteAsync(SellerBankAccountDto request);
        Task<Response<bool>> UpdateAsync(int id, SellerBankAccountDto request);
        Task<Response<List<SellerBankAccountDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SellerBankAccountDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerBankAccountSellerId, int? page, int? size);
        Task<Response<GetDefaultBySellerIdResponseDto>> GetDefaultBySellerIdAsync(int sellerBankAccountSellerId, bool sellerBankAccountIsDefault);
        Task<Response<int>> SetAsDefaultAsync(int sellerBankAccountSellerId, SetAsDefaultRequestDto updateRequest);
        Task<Response<int>> SetDefaultAccountAsync(int sellerBankAccountId, SetDefaultAccountRequestDto updateRequest);
    }
}