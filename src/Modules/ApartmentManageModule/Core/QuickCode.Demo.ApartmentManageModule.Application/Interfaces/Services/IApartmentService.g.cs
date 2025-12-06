using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Apartment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.Apartment
{
    public partial interface IApartmentService
    {
        Task<Response<ApartmentDto>> InsertAsync(ApartmentDto request);
        Task<Response<bool>> DeleteAsync(ApartmentDto request);
        Task<Response<bool>> UpdateAsync(int id, ApartmentDto request);
        Task<Response<List<ApartmentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ApartmentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetApartmentsBySiteResponseDto>>> GetApartmentsBySiteAsync(int apartmentsSiteId, bool apartmentsIsActive);
        Task<Response<List<GetActiveApartmentsResponseDto>>> GetActiveApartmentsAsync(bool apartmentsIsActive);
        Task<Response<List<GetFlatsForApartmentsResponseDto>>> GetFlatsForApartmentsAsync(int apartmentsId);
        Task<Response<GetFlatsForApartmentsResponseDto>> GetFlatsForApartmentsDetailsAsync(int apartmentsId, int flatsId);
        Task<Response<List<GetFlatPaymentsForApartmentsResponseDto>>> GetFlatPaymentsForApartmentsAsync(int apartmentsId);
        Task<Response<GetFlatPaymentsForApartmentsResponseDto>> GetFlatPaymentsForApartmentsDetailsAsync(int apartmentsId, int flatPaymentsId);
        Task<Response<List<GetCommonExpensesForApartmentsResponseDto>>> GetCommonExpensesForApartmentsAsync(int apartmentsId);
        Task<Response<GetCommonExpensesForApartmentsResponseDto>> GetCommonExpensesForApartmentsDetailsAsync(int apartmentsId, int commonExpensesId);
        Task<Response<List<GetApartmentFeePlansForApartmentsResponseDto>>> GetApartmentFeePlansForApartmentsAsync(int apartmentsId);
        Task<Response<GetApartmentFeePlansForApartmentsResponseDto>> GetApartmentFeePlansForApartmentsDetailsAsync(int apartmentsId, int apartmentFeePlansId);
        Task<Response<List<GetExpenseInstallmentsForApartmentsResponseDto>>> GetExpenseInstallmentsForApartmentsAsync(int apartmentsId);
        Task<Response<GetExpenseInstallmentsForApartmentsResponseDto>> GetExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int expenseInstallmentsId);
        Task<Response<List<GetFlatExpenseInstallmentsForApartmentsResponseDto>>> GetFlatExpenseInstallmentsForApartmentsAsync(int apartmentsId);
        Task<Response<GetFlatExpenseInstallmentsForApartmentsResponseDto>> GetFlatExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int flatExpenseInstallmentsId);
    }
}