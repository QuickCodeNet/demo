using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ExpenseType;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.ExpenseType
{
    public partial interface IExpenseTypeService
    {
        Task<Response<ExpenseTypeDto>> InsertAsync(ExpenseTypeDto request);
        Task<Response<bool>> DeleteAsync(ExpenseTypeDto request);
        Task<Response<bool>> UpdateAsync(int id, ExpenseTypeDto request);
        Task<Response<List<ExpenseTypeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ExpenseTypeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveExpenseTypesResponseDto>>> GetActiveExpenseTypesAsync(bool expenseTypesIsActive);
        Task<Response<List<GetCommonExpensesForExpenseTypesResponseDto>>> GetCommonExpensesForExpenseTypesAsync(int expenseTypesId);
        Task<Response<GetCommonExpensesForExpenseTypesResponseDto>> GetCommonExpensesForExpenseTypesDetailsAsync(int expenseTypesId, int commonExpensesId);
    }
}