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
    public partial interface IPaymentMonthService
    {
        Task<Response<PaymentMonthDto>> InsertAsync(PaymentMonthDto request);
        Task<Response<bool>> DeleteAsync(PaymentMonthDto request);
        Task<Response<bool>> UpdateAsync(int id, PaymentMonthDto request);
        Task<Response<List<PaymentMonthDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<PaymentMonthDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetAllMonthsResponseDto>>> GetAllMonthsAsync();
        Task<Response<List<GetApartmentFeePlansForPaymentMonthsResponseDto>>> GetApartmentFeePlansForPaymentMonthsAsync(int paymentMonthsId);
        Task<Response<GetApartmentFeePlansForPaymentMonthsResponseDto>> GetApartmentFeePlansForPaymentMonthsDetailsAsync(int paymentMonthsId, int apartmentFeePlansId);
        Task<Response<List<GetFlatPaymentsForPaymentMonthsResponseDto>>> GetFlatPaymentsForPaymentMonthsAsync(int paymentMonthsId);
        Task<Response<GetFlatPaymentsForPaymentMonthsResponseDto>> GetFlatPaymentsForPaymentMonthsDetailsAsync(int paymentMonthsId, int flatPaymentsId);
        Task<Response<List<GetCommonExpensesForPaymentMonthsResponseDto>>> GetCommonExpensesForPaymentMonthsAsync(int paymentMonthsId);
        Task<Response<GetCommonExpensesForPaymentMonthsResponseDto>> GetCommonExpensesForPaymentMonthsDetailsAsync(int paymentMonthsId, int commonExpensesId);
    }
}