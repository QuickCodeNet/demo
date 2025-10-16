using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ApartmentFeePlan;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.ApartmentFeePlan
{
    public partial interface IApartmentFeePlanService
    {
        Task<Response<ApartmentFeePlanDto>> InsertAsync(ApartmentFeePlanDto request);
        Task<Response<bool>> DeleteAsync(ApartmentFeePlanDto request);
        Task<Response<bool>> UpdateAsync(int id, ApartmentFeePlanDto request);
        Task<Response<List<ApartmentFeePlanDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ApartmentFeePlanDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetFeePlanByYearMonthResponseDto>>> GetFeePlanByYearMonthAsync(int apartmentFeePlansSiteId, int apartmentFeePlansApartmentId, int apartmentFeePlansYearId, int apartmentFeePlansMonthId, bool apartmentFeePlansIsActive);
        Task<Response<List<GetFeePlansBySiteResponseDto>>> GetFeePlansBySiteAsync(int apartmentFeePlansSiteId, bool apartmentFeePlansIsActive);
        Task<Response<List<GetFlatPaymentsForApartmentFeePlansResponseDto>>> GetFlatPaymentsForApartmentFeePlansAsync(int apartmentFeePlansId);
        Task<Response<GetFlatPaymentsForApartmentFeePlansResponseDto>> GetFlatPaymentsForApartmentFeePlansDetailsAsync(int apartmentFeePlansId, int flatPaymentsId);
    }
}