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
    public partial class FinancialAdjustmentService : IFinancialAdjustmentService
    {
        private readonly ILogger<FinancialAdjustmentService> _logger;
        private readonly IFinancialAdjustmentRepository _repository;
        public FinancialAdjustmentService(ILogger<FinancialAdjustmentService> logger, IFinancialAdjustmentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<FinancialAdjustmentDto>> InsertAsync(FinancialAdjustmentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(FinancialAdjustmentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, FinancialAdjustmentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<FinancialAdjustmentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<FinancialAdjustmentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int financialAdjustmentSellerId, int? page, int? size)
        {
            var returnValue = await _repository.GetBySellerIdAsync(financialAdjustmentSellerId, page, size);
            return returnValue.ToResponse();
        }
    }
}