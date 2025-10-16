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
    public partial interface IPaymentTypeService
    {
        Task<Response<PaymentTypeDto>> InsertAsync(PaymentTypeDto request);
        Task<Response<bool>> DeleteAsync(PaymentTypeDto request);
        Task<Response<bool>> UpdateAsync(int id, PaymentTypeDto request);
        Task<Response<List<PaymentTypeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<PaymentTypeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActivePaymentTypesResponseDto>>> GetActivePaymentTypesAsync(bool paymentTypesIsActive);
        Task<Response<List<GetFlatPaymentsForPaymentTypesResponseDto>>> GetFlatPaymentsForPaymentTypesAsync(int paymentTypesId);
        Task<Response<GetFlatPaymentsForPaymentTypesResponseDto>> GetFlatPaymentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatPaymentsId);
        Task<Response<List<GetCommonExpensesForPaymentTypesResponseDto>>> GetCommonExpensesForPaymentTypesAsync(int paymentTypesId);
        Task<Response<GetCommonExpensesForPaymentTypesResponseDto>> GetCommonExpensesForPaymentTypesDetailsAsync(int paymentTypesId, int commonExpensesId);
        Task<Response<List<GetExpenseInstallmentsForPaymentTypesResponseDto>>> GetExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId);
        Task<Response<GetExpenseInstallmentsForPaymentTypesResponseDto>> GetExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int expenseInstallmentsId);
        Task<Response<List<GetFlatExpenseInstallmentsForPaymentTypesResponseDto>>> GetFlatExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId);
        Task<Response<GetFlatExpenseInstallmentsForPaymentTypesResponseDto>> GetFlatExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatExpenseInstallmentsId);
    }
}