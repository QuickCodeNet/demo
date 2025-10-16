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
    public partial interface IPaymentYearService
    {
        Task<Response<PaymentYearDto>> InsertAsync(PaymentYearDto request);
        Task<Response<bool>> DeleteAsync(PaymentYearDto request);
        Task<Response<bool>> UpdateAsync(int id, PaymentYearDto request);
        Task<Response<List<PaymentYearDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<PaymentYearDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetAllYearsResponseDto>>> GetAllYearsAsync();
        Task<Response<List<GetApartmentFeePlansForPaymentYearsResponseDto>>> GetApartmentFeePlansForPaymentYearsAsync(int paymentYearsId);
        Task<Response<GetApartmentFeePlansForPaymentYearsResponseDto>> GetApartmentFeePlansForPaymentYearsDetailsAsync(int paymentYearsId, int apartmentFeePlansId);
        Task<Response<List<GetFlatPaymentsForPaymentYearsResponseDto>>> GetFlatPaymentsForPaymentYearsAsync(int paymentYearsId);
        Task<Response<GetFlatPaymentsForPaymentYearsResponseDto>> GetFlatPaymentsForPaymentYearsDetailsAsync(int paymentYearsId, int flatPaymentsId);
        Task<Response<List<GetCommonExpensesForPaymentYearsResponseDto>>> GetCommonExpensesForPaymentYearsAsync(int paymentYearsId);
        Task<Response<GetCommonExpensesForPaymentYearsResponseDto>> GetCommonExpensesForPaymentYearsDetailsAsync(int paymentYearsId, int commonExpensesId);
    }
}