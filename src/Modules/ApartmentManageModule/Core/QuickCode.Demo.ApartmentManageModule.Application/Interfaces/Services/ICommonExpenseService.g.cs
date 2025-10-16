using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.CommonExpense;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.CommonExpense
{
    public partial interface ICommonExpenseService
    {
        Task<Response<CommonExpenseDto>> InsertAsync(CommonExpenseDto request);
        Task<Response<bool>> DeleteAsync(CommonExpenseDto request);
        Task<Response<bool>> UpdateAsync(int id, CommonExpenseDto request);
        Task<Response<List<CommonExpenseDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CommonExpenseDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetExpensesByApartmentMonthResponseDto>>> GetExpensesByApartmentMonthAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId, int commonExpensesMonthId);
        Task<Response<List<GetExpensesBySiteResponseDto>>> GetExpensesBySiteAsync(int commonExpensesSiteId);
        Task<Response<List<GetExpensesSummaryByYearResponseDto>>> GetExpensesSummaryByYearAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId);
        Task<Response<List<GetExpensesByTypeResponseDto>>> GetExpensesByTypeAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesExpenseTypeId, int commonExpensesYearId, int commonExpensesMonthId);
        Task<Response<List<GetUnpaidExpensesByApartmentResponseDto>>> GetUnpaidExpensesByApartmentAsync(int commonExpensesApartmentId, bool commonExpensesPaid);
        Task<Response<long>> GetExpensesCountByApartmentAsync(int commonExpensesApartmentId);
        Task<Response<GetTotalExpenseAmountByApartmentResponseDto>> GetTotalExpenseAmountByApartmentAsync(int commonExpensesApartmentId);
        Task<Response<List<GetFlatPaymentsForCommonExpensesResponseDto>>> GetFlatPaymentsForCommonExpensesAsync(int commonExpensesId);
        Task<Response<GetFlatPaymentsForCommonExpensesResponseDto>> GetFlatPaymentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatPaymentsId);
        Task<Response<List<GetExpenseInstallmentsForCommonExpensesResponseDto>>> GetExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId);
        Task<Response<GetExpenseInstallmentsForCommonExpensesResponseDto>> GetExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int expenseInstallmentsId);
        Task<Response<List<GetFlatExpenseInstallmentsForCommonExpensesResponseDto>>> GetFlatExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId);
        Task<Response<GetFlatExpenseInstallmentsForCommonExpensesResponseDto>> GetFlatExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatExpenseInstallmentsId);
        Task<Response<int>> MarkExpenseAsPaidAsync(int commonExpensesId, MarkExpenseAsPaidRequestDto updateRequest);
    }
}