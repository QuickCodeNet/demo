using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentType;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentType
{
    public partial class PaymentTypeService : IPaymentTypeService
    {
        private readonly ILogger<PaymentTypeService> _logger;
        private readonly IPaymentTypeRepository _repository;
        public PaymentTypeService(ILogger<PaymentTypeService> logger, IPaymentTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<PaymentTypeDto>> InsertAsync(PaymentTypeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(PaymentTypeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, PaymentTypeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<PaymentTypeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<PaymentTypeDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetActivePaymentTypesResponseDto>>> GetActivePaymentTypesAsync(bool paymentTypesIsActive)
        {
            var returnValue = await _repository.GetActivePaymentTypesAsync(paymentTypesIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForPaymentTypesResponseDto>>> GetFlatPaymentsForPaymentTypesAsync(int paymentTypesId)
        {
            var returnValue = await _repository.GetFlatPaymentsForPaymentTypesAsync(paymentTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForPaymentTypesResponseDto>> GetFlatPaymentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForPaymentTypesDetailsAsync(paymentTypesId, flatPaymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCommonExpensesForPaymentTypesResponseDto>>> GetCommonExpensesForPaymentTypesAsync(int paymentTypesId)
        {
            var returnValue = await _repository.GetCommonExpensesForPaymentTypesAsync(paymentTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCommonExpensesForPaymentTypesResponseDto>> GetCommonExpensesForPaymentTypesDetailsAsync(int paymentTypesId, int commonExpensesId)
        {
            var returnValue = await _repository.GetCommonExpensesForPaymentTypesDetailsAsync(paymentTypesId, commonExpensesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetExpenseInstallmentsForPaymentTypesResponseDto>>> GetExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForPaymentTypesAsync(paymentTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetExpenseInstallmentsForPaymentTypesResponseDto>> GetExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int expenseInstallmentsId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForPaymentTypesDetailsAsync(paymentTypesId, expenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatExpenseInstallmentsForPaymentTypesResponseDto>>> GetFlatExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForPaymentTypesAsync(paymentTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatExpenseInstallmentsForPaymentTypesResponseDto>> GetFlatExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatExpenseInstallmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForPaymentTypesDetailsAsync(paymentTypesId, flatExpenseInstallmentsId);
            return returnValue.ToResponse();
        }
    }
}