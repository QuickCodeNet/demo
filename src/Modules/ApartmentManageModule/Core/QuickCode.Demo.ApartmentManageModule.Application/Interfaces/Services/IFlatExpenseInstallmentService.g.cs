using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.FlatExpenseInstallment
{
    public partial interface IFlatExpenseInstallmentService
    {
        Task<Response<FlatExpenseInstallmentDto>> InsertAsync(FlatExpenseInstallmentDto request);
        Task<Response<bool>> DeleteAsync(FlatExpenseInstallmentDto request);
        Task<Response<bool>> UpdateAsync(int id, FlatExpenseInstallmentDto request);
        Task<Response<List<FlatExpenseInstallmentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<FlatExpenseInstallmentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetFlatExpenseInstallmentsResponseDto>>> GetFlatExpenseInstallmentsAsync(int flatExpenseInstallmentsFlatId, int flatExpenseInstallmentsExpenseId);
        Task<Response<List<GetFlatUnpaidInstallmentsResponseDto>>> GetFlatUnpaidInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid);
        Task<Response<List<GetFlatOverdueInstallmentsResponseDto>>> GetFlatOverdueInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid);
        Task<Response<List<GetApartmentFlatInstallmentsResponseDto>>> GetApartmentFlatInstallmentsAsync(int flatExpenseInstallmentsSiteId, int flatExpenseInstallmentsApartmentId);
        Task<Response<List<GetFlatTotalDebtResponseDto>>> GetFlatTotalDebtAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid);
        Task<Response<int>> MarkFlatInstallmentAsPaidAsync(int flatExpenseInstallmentsId, MarkFlatInstallmentAsPaidRequestDto updateRequest);
    }
}