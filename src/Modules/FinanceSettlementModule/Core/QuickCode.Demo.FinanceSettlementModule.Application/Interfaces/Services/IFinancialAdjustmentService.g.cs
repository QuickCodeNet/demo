using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.FinanceSettlementModule.Domain.Entities;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.FinancialAdjustment;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Services.FinancialAdjustment
{
    public partial interface IFinancialAdjustmentService
    {
        Task<Response<FinancialAdjustmentDto>> InsertAsync(FinancialAdjustmentDto request);
        Task<Response<bool>> DeleteAsync(FinancialAdjustmentDto request);
        Task<Response<bool>> UpdateAsync(int id, FinancialAdjustmentDto request);
        Task<Response<List<FinancialAdjustmentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<FinancialAdjustmentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int financialAdjustmentSellerId, int? pageNumber, int? pageSize);
    }
}