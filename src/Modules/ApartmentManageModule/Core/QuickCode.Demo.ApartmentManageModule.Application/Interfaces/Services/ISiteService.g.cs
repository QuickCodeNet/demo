using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Site;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.Site
{
    public partial interface ISiteService
    {
        Task<Response<SiteDto>> InsertAsync(SiteDto request);
        Task<Response<bool>> DeleteAsync(SiteDto request);
        Task<Response<bool>> UpdateAsync(int id, SiteDto request);
        Task<Response<List<SiteDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SiteDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveSitesResponseDto>>> GetActiveSitesAsync(bool sitesIsActive);
        Task<Response<GetSiteByIdResponseDto>> GetSiteByIdAsync(int sitesId);
        Task<Response<long>> GetFlatsCountBySiteAsync(int sitesId, bool sitesIsActive);
        Task<Response<long>> GetOwnersCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType);
        Task<Response<long>> GetTenantsCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType);
        Task<Response<GetTotalPaymentsBySiteResponseDto>> GetTotalPaymentsBySiteAsync(int sitesId, bool flatPaymentsPaid);
        Task<Response<List<GetApartmentsForSitesResponseDto>>> GetApartmentsForSitesAsync(int sitesId);
        Task<Response<GetApartmentsForSitesResponseDto>> GetApartmentsForSitesDetailsAsync(int sitesId, int apartmentsId);
        Task<Response<List<GetFlatsForSitesResponseDto>>> GetFlatsForSitesAsync(int sitesId);
        Task<Response<GetFlatsForSitesResponseDto>> GetFlatsForSitesDetailsAsync(int sitesId, int flatsId);
        Task<Response<List<GetSiteManagersForSitesResponseDto>>> GetSiteManagersForSitesAsync(int sitesId);
        Task<Response<GetSiteManagersForSitesResponseDto>> GetSiteManagersForSitesDetailsAsync(int sitesId, int siteManagersId);
        Task<Response<List<GetFlatPaymentsForSitesResponseDto>>> GetFlatPaymentsForSitesAsync(int sitesId);
        Task<Response<GetFlatPaymentsForSitesResponseDto>> GetFlatPaymentsForSitesDetailsAsync(int sitesId, int flatPaymentsId);
        Task<Response<List<GetCommonExpensesForSitesResponseDto>>> GetCommonExpensesForSitesAsync(int sitesId);
        Task<Response<GetCommonExpensesForSitesResponseDto>> GetCommonExpensesForSitesDetailsAsync(int sitesId, int commonExpensesId);
        Task<Response<List<GetApartmentFeePlansForSitesResponseDto>>> GetApartmentFeePlansForSitesAsync(int sitesId);
        Task<Response<GetApartmentFeePlansForSitesResponseDto>> GetApartmentFeePlansForSitesDetailsAsync(int sitesId, int apartmentFeePlansId);
        Task<Response<List<GetExpenseInstallmentsForSitesResponseDto>>> GetExpenseInstallmentsForSitesAsync(int sitesId);
        Task<Response<GetExpenseInstallmentsForSitesResponseDto>> GetExpenseInstallmentsForSitesDetailsAsync(int sitesId, int expenseInstallmentsId);
        Task<Response<List<GetFlatExpenseInstallmentsForSitesResponseDto>>> GetFlatExpenseInstallmentsForSitesAsync(int sitesId);
        Task<Response<GetFlatExpenseInstallmentsForSitesResponseDto>> GetFlatExpenseInstallmentsForSitesDetailsAsync(int sitesId, int flatExpenseInstallmentsId);
        Task<Response<List<GetUserSiteAccessesForSitesResponseDto>>> GetUserSiteAccessesForSitesAsync(int sitesId);
        Task<Response<GetUserSiteAccessesForSitesResponseDto>> GetUserSiteAccessesForSitesDetailsAsync(int sitesId, int userSiteAccessesId);
    }
}