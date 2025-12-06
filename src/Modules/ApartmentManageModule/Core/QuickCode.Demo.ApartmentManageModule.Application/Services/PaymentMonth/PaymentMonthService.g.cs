using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentMonth;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentMonth
{
    public partial class PaymentMonthService : IPaymentMonthService
    {
        private readonly ILogger<PaymentMonthService> _logger;
        private readonly IPaymentMonthRepository _repository;
        public PaymentMonthService(ILogger<PaymentMonthService> logger, IPaymentMonthRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<PaymentMonthDto>> InsertAsync(PaymentMonthDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(PaymentMonthDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, PaymentMonthDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<PaymentMonthDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<PaymentMonthDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetAllMonthsResponseDto>>> GetAllMonthsAsync()
        {
            var returnValue = await _repository.GetAllMonthsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetApartmentFeePlansForPaymentMonthsResponseDto>>> GetApartmentFeePlansForPaymentMonthsAsync(int paymentMonthsId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForPaymentMonthsAsync(paymentMonthsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetApartmentFeePlansForPaymentMonthsResponseDto>> GetApartmentFeePlansForPaymentMonthsDetailsAsync(int paymentMonthsId, int apartmentFeePlansId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForPaymentMonthsDetailsAsync(paymentMonthsId, apartmentFeePlansId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForPaymentMonthsResponseDto>>> GetFlatPaymentsForPaymentMonthsAsync(int paymentMonthsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForPaymentMonthsAsync(paymentMonthsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForPaymentMonthsResponseDto>> GetFlatPaymentsForPaymentMonthsDetailsAsync(int paymentMonthsId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForPaymentMonthsDetailsAsync(paymentMonthsId, flatPaymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCommonExpensesForPaymentMonthsResponseDto>>> GetCommonExpensesForPaymentMonthsAsync(int paymentMonthsId)
        {
            var returnValue = await _repository.GetCommonExpensesForPaymentMonthsAsync(paymentMonthsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCommonExpensesForPaymentMonthsResponseDto>> GetCommonExpensesForPaymentMonthsDetailsAsync(int paymentMonthsId, int commonExpensesId)
        {
            var returnValue = await _repository.GetCommonExpensesForPaymentMonthsDetailsAsync(paymentMonthsId, commonExpensesId);
            return returnValue.ToResponse();
        }
    }
}