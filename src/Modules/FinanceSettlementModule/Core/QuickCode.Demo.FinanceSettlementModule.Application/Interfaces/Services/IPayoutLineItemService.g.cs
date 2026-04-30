using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.FinanceSettlementModule.Domain.Entities;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.PayoutLineItem;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Services.PayoutLineItem
{
    public partial interface IPayoutLineItemService
    {
        Task<Response<PayoutLineItemDto>> InsertAsync(PayoutLineItemDto request);
        Task<Response<bool>> DeleteAsync(PayoutLineItemDto request);
        Task<Response<bool>> UpdateAsync(int id, PayoutLineItemDto request);
        Task<Response<List<PayoutLineItemDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<PayoutLineItemDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByPayoutIdResponseDto>>> GetByPayoutIdAsync(int payoutLineItemPayoutId, int? pageNumber, int? pageSize);
    }
}