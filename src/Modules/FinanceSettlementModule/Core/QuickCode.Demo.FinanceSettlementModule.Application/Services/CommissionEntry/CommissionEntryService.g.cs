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
    public partial class CommissionEntryService : ICommissionEntryService
    {
        private readonly ILogger<CommissionEntryService> _logger;
        private readonly ICommissionEntryRepository _repository;
        public CommissionEntryService(ILogger<CommissionEntryService> logger, ICommissionEntryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CommissionEntryDto>> InsertAsync(CommissionEntryDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CommissionEntryDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CommissionEntryDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CommissionEntryDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CommissionEntryDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetByOrderIdResponseDto>>> GetByOrderIdAsync(int commissionEntryOrderId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetByOrderIdAsync(commissionEntryOrderId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCommissionsBySellerForPeriodResponseDto>>> GetCommissionsBySellerForPeriodAsync(int commissionEntriesSellerId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetCommissionsBySellerForPeriodAsync(commissionEntriesSellerId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }
    }
}