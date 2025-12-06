using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatPayment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.FlatPayment
{
    public partial interface IFlatPaymentService
    {
        Task<Response<FlatPaymentDto>> InsertAsync(FlatPaymentDto request);
        Task<Response<bool>> DeleteAsync(FlatPaymentDto request);
        Task<Response<bool>> UpdateAsync(int id, FlatPaymentDto request);
        Task<Response<List<FlatPaymentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<FlatPaymentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetPaymentsByFlatYearMonthResponseDto>>> GetPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId);
        Task<Response<List<GetUnpaidPaymentsByFlatResponseDto>>> GetUnpaidPaymentsByFlatAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, bool flatPaymentsPaid);
        Task<Response<List<GetUnpaidPaymentsBySiteResponseDto>>> GetUnpaidPaymentsBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid);
        Task<Response<List<GetTotalCashInSafeResponseDto>>> GetTotalCashInSafeAsync(int flatPaymentsSiteId, bool flatPaymentsPaid);
        Task<Response<List<GetPendingPaymentsByFlatYearMonthResponseDto>>> GetPendingPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, bool flatPaymentsPaid);
        Task<Response<List<GetFlatPaymentsByMonthResponseDto>>> GetFlatPaymentsByMonthAsync(int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId);
        Task<Response<long>> GetPaymentsCountByFlatAsync(int flatPaymentsFlatId);
        Task<Response<GetTotalPaidAmountByFlatResponseDto>> GetTotalPaidAmountByFlatAsync(int flatPaymentsFlatId, bool flatPaymentsPaid);
        Task<Response<long>> GetUnpaidPaymentsCountBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid);
        Task<Response<int>> MarkPaymentAsPaidAsync(int flatPaymentsId, MarkPaymentAsPaidRequestDto updateRequest);
    }
}