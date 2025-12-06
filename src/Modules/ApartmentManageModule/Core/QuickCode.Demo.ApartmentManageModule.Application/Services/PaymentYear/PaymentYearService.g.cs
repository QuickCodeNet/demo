using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentYear;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentYear
{
    public partial class PaymentYearService : IPaymentYearService
    {
        private readonly ILogger<PaymentYearService> _logger;
        private readonly IPaymentYearRepository _repository;
        public PaymentYearService(ILogger<PaymentYearService> logger, IPaymentYearRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<PaymentYearDto>> InsertAsync(PaymentYearDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(PaymentYearDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, PaymentYearDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<PaymentYearDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<PaymentYearDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetAllYearsResponseDto>>> GetAllYearsAsync()
        {
            var returnValue = await _repository.GetAllYearsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetApartmentFeePlansForPaymentYearsResponseDto>>> GetApartmentFeePlansForPaymentYearsAsync(int paymentYearsId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForPaymentYearsAsync(paymentYearsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetApartmentFeePlansForPaymentYearsResponseDto>> GetApartmentFeePlansForPaymentYearsDetailsAsync(int paymentYearsId, int apartmentFeePlansId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForPaymentYearsDetailsAsync(paymentYearsId, apartmentFeePlansId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForPaymentYearsResponseDto>>> GetFlatPaymentsForPaymentYearsAsync(int paymentYearsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForPaymentYearsAsync(paymentYearsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForPaymentYearsResponseDto>> GetFlatPaymentsForPaymentYearsDetailsAsync(int paymentYearsId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForPaymentYearsDetailsAsync(paymentYearsId, flatPaymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCommonExpensesForPaymentYearsResponseDto>>> GetCommonExpensesForPaymentYearsAsync(int paymentYearsId)
        {
            var returnValue = await _repository.GetCommonExpensesForPaymentYearsAsync(paymentYearsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCommonExpensesForPaymentYearsResponseDto>> GetCommonExpensesForPaymentYearsDetailsAsync(int paymentYearsId, int commonExpensesId)
        {
            var returnValue = await _repository.GetCommonExpensesForPaymentYearsDetailsAsync(paymentYearsId, commonExpensesId);
            return returnValue.ToResponse();
        }
    }
}