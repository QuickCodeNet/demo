using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.ExpenseInstallment
{
    public partial interface IExpenseInstallmentService
    {
        Task<Response<ExpenseInstallmentDto>> InsertAsync(ExpenseInstallmentDto request);
        Task<Response<bool>> DeleteAsync(ExpenseInstallmentDto request);
        Task<Response<bool>> UpdateAsync(int id, ExpenseInstallmentDto request);
        Task<Response<List<ExpenseInstallmentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ExpenseInstallmentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetExpenseInstallmentsResponseDto>>> GetExpenseInstallmentsAsync(int expenseInstallmentsExpenseId);
        Task<Response<List<GetUnpaidInstallmentsResponseDto>>> GetUnpaidInstallmentsAsync(int expenseInstallmentsExpenseId, bool expenseInstallmentsPaid);
        Task<Response<List<GetOverdueInstallmentsResponseDto>>> GetOverdueInstallmentsAsync(bool expenseInstallmentsPaid);
        Task<Response<List<GetApartmentInstallmentsResponseDto>>> GetApartmentInstallmentsAsync(int expenseInstallmentsSiteId, int expenseInstallmentsApartmentId);
        Task<Response<List<GetSiteInstallmentsResponseDto>>> GetSiteInstallmentsAsync(int expenseInstallmentsSiteId);
        Task<Response<List<GetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>>> GetFlatExpenseInstallmentsForExpenseInstallmentsAsync(int expenseInstallmentsId);
        Task<Response<GetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>> GetFlatExpenseInstallmentsForExpenseInstallmentsDetailsAsync(int expenseInstallmentsId, int flatExpenseInstallmentsId);
        Task<Response<int>> MarkInstallmentAsPaidAsync(int expenseInstallmentsId, MarkInstallmentAsPaidRequestDto updateRequest);
    }
}