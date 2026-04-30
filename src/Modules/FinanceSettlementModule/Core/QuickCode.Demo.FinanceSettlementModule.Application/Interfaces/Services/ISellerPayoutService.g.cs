using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.FinanceSettlementModule.Domain.Entities;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.SellerPayout;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Services.SellerPayout
{
    public partial interface ISellerPayoutService
    {
        Task<Response<SellerPayoutDto>> InsertAsync(SellerPayoutDto request);
        Task<Response<bool>> DeleteAsync(SellerPayoutDto request);
        Task<Response<bool>> UpdateAsync(int id, SellerPayoutDto request);
        Task<Response<List<SellerPayoutDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SellerPayoutDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerPayoutSellerId, int? pageNumber, int? pageSize);
        Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(PayoutStatus sellerPayoutStatus, int? pageNumber, int? pageSize);
        Task<Response<List<GetByPeriodResponseDto>>> GetByPeriodAsync(int sellerPayoutPayoutPeriodId, int? pageNumber, int? pageSize);
        Task<Response<GetPendingPayoutsSummaryResponseDto>> GetPendingPayoutsSummaryAsync();
        Task<Response<int>> ApproveAsync(int sellerPayoutId, ApproveRequestDto updateRequest);
        Task<Response<int>> MarkAsPaidAsync(int sellerPayoutId, MarkAsPaidRequestDto updateRequest);
        Task<Response<int>> MarkAsFailedAsync(int sellerPayoutId, MarkAsFailedRequestDto updateRequest);
    }
}