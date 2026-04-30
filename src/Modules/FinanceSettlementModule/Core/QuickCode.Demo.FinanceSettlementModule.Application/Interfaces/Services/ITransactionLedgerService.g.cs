using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.FinanceSettlementModule.Domain.Entities;
using QuickCode.Demo.FinanceSettlementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.TransactionLedger;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Application.Services.TransactionLedger
{
    public partial interface ITransactionLedgerService
    {
        Task<Response<TransactionLedgerDto>> InsertAsync(TransactionLedgerDto request);
        Task<Response<bool>> DeleteAsync(TransactionLedgerDto request);
        Task<Response<bool>> UpdateAsync(int id, TransactionLedgerDto request);
        Task<Response<List<TransactionLedgerDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<TransactionLedgerDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int transactionLedgerSellerId, int? pageNumber, int? pageSize);
        Task<Response<GetSellerBalanceResponseDto>> GetSellerBalanceAsync(int transactionLedgerSellerId);
        Task<Response<List<GetTransactionsByTypeAndDateResponseDto>>> GetTransactionsByTypeAndDateAsync(int transactionLedgerSellerId, TransactionType transactionLedgerTransactionType, DateTime transactionLedgerTransactionDateFrom, DateTime transactionLedgerTransactionDateTo, int? pageNumber, int? pageSize);
    }
}