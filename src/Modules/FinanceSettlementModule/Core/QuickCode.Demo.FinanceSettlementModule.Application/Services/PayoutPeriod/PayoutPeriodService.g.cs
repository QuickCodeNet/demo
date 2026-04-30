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
    public partial class PayoutPeriodService : IPayoutPeriodService
    {
        private readonly ILogger<PayoutPeriodService> _logger;
        private readonly IPayoutPeriodRepository _repository;
        public PayoutPeriodService(ILogger<PayoutPeriodService> logger, IPayoutPeriodRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<PayoutPeriodDto>> InsertAsync(PayoutPeriodDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(PayoutPeriodDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, PayoutPeriodDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<PayoutPeriodDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<PayoutPeriodDto>> GetItemAsync(int id)
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

        public async Task<Response<GetOpenPeriodResponseDto>> GetOpenPeriodAsync()
        {
            var returnValue = await _repository.GetOpenPeriodAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> ClosePeriodAsync(int payoutPeriodId, ClosePeriodRequestDto updateRequest)
        {
            var returnValue = await _repository.ClosePeriodAsync(payoutPeriodId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}