using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.FinanceSettlementModule.Domain.Entities;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.PayoutPeriod;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Services.PayoutPeriod
{
    public partial interface IPayoutPeriodService
    {
        Task<Response<PayoutPeriodDto>> InsertAsync(PayoutPeriodDto request);
        Task<Response<bool>> DeleteAsync(PayoutPeriodDto request);
        Task<Response<bool>> UpdateAsync(int id, PayoutPeriodDto request);
        Task<Response<List<PayoutPeriodDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<PayoutPeriodDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetOpenPeriodResponseDto>> GetOpenPeriodAsync();
        Task<Response<int>> ClosePeriodAsync(int payoutPeriodId, ClosePeriodRequestDto updateRequest);
    }
}