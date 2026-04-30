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
    public partial class SellerPayoutService : ISellerPayoutService
    {
        private readonly ILogger<SellerPayoutService> _logger;
        private readonly ISellerPayoutRepository _repository;
        public SellerPayoutService(ILogger<SellerPayoutService> logger, ISellerPayoutRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SellerPayoutDto>> InsertAsync(SellerPayoutDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SellerPayoutDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SellerPayoutDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SellerPayoutDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SellerPayoutDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerPayoutSellerId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetBySellerIdAsync(sellerPayoutSellerId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(PayoutStatus sellerPayoutStatus, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetByStatusAsync(sellerPayoutStatus, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByPeriodResponseDto>>> GetByPeriodAsync(int sellerPayoutPayoutPeriodId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetByPeriodAsync(sellerPayoutPayoutPeriodId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetPendingPayoutsSummaryResponseDto>> GetPendingPayoutsSummaryAsync()
        {
            var returnValue = await _repository.GetPendingPayoutsSummaryAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> ApproveAsync(int sellerPayoutId, ApproveRequestDto updateRequest)
        {
            var returnValue = await _repository.ApproveAsync(sellerPayoutId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> MarkAsPaidAsync(int sellerPayoutId, MarkAsPaidRequestDto updateRequest)
        {
            var returnValue = await _repository.MarkAsPaidAsync(sellerPayoutId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> MarkAsFailedAsync(int sellerPayoutId, MarkAsFailedRequestDto updateRequest)
        {
            var returnValue = await _repository.MarkAsFailedAsync(sellerPayoutId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}