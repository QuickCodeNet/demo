using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Payment;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Payment
{
    public partial class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IPaymentRepository _repository;
        public PaymentService(ILogger<PaymentService> logger, IPaymentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<PaymentDto>> InsertAsync(PaymentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(PaymentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, PaymentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<PaymentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<PaymentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetPendingPaymentsResponseDto>>> GetPendingPaymentsAsync(PaymentStatus paymentsStatus)
        {
            var returnValue = await _repository.GetPendingPaymentsAsync(paymentsStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOrdersForPaymentsResponseDto>>> GetOrdersForPaymentsAsync(int paymentsId)
        {
            var returnValue = await _repository.GetOrdersForPaymentsAsync(paymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOrdersForPaymentsResponseDto>> GetOrdersForPaymentsDetailsAsync(int paymentsId, int ordersId)
        {
            var returnValue = await _repository.GetOrdersForPaymentsDetailsAsync(paymentsId, ordersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> MarkPaymentFailedAsync(PaymentStatus paymentsStatus, MarkPaymentFailedRequestDto updateRequest)
        {
            var returnValue = await _repository.MarkPaymentFailedAsync(paymentsStatus, updateRequest);
            return returnValue.ToResponse();
        }
    }
}