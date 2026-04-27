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
    public partial class TransactionLedgerService : ITransactionLedgerService
    {
        private readonly ILogger<TransactionLedgerService> _logger;
        private readonly ITransactionLedgerRepository _repository;
        public TransactionLedgerService(ILogger<TransactionLedgerService> logger, ITransactionLedgerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<TransactionLedgerDto>> InsertAsync(TransactionLedgerDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(TransactionLedgerDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, TransactionLedgerDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<TransactionLedgerDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<TransactionLedgerDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int transactionLedgerSellerId, int? page, int? size)
        {
            var returnValue = await _repository.GetBySellerIdAsync(transactionLedgerSellerId, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetSellerBalanceResponseDto>> GetSellerBalanceAsync(int transactionLedgerSellerId)
        {
            var returnValue = await _repository.GetSellerBalanceAsync(transactionLedgerSellerId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTransactionsByTypeAndDateResponseDto>>> GetTransactionsByTypeAndDateAsync(int transactionLedgerSellerId, TransactionType transactionLedgerTransactionType, DateTime transactionLedgerTransactionDateFrom, DateTime transactionLedgerTransactionDateTo, int? page, int? size)
        {
            var returnValue = await _repository.GetTransactionsByTypeAndDateAsync(transactionLedgerSellerId, transactionLedgerTransactionType, transactionLedgerTransactionDateFrom, transactionLedgerTransactionDateTo, page, size);
            return returnValue.ToResponse();
        }
    }
}