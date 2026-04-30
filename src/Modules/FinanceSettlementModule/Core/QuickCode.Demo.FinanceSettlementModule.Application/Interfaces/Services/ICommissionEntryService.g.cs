using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.FinanceSettlementModule.Domain.Entities;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.CommissionEntry;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Services.CommissionEntry
{
    public partial interface ICommissionEntryService
    {
        Task<Response<CommissionEntryDto>> InsertAsync(CommissionEntryDto request);
        Task<Response<bool>> DeleteAsync(CommissionEntryDto request);
        Task<Response<bool>> UpdateAsync(int id, CommissionEntryDto request);
        Task<Response<List<CommissionEntryDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CommissionEntryDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByOrderIdResponseDto>>> GetByOrderIdAsync(int commissionEntryOrderId, int? pageNumber, int? pageSize);
        Task<Response<List<GetCommissionsBySellerForPeriodResponseDto>>> GetCommissionsBySellerForPeriodAsync(int commissionEntriesSellerId, int? pageNumber, int? pageSize);
    }
}