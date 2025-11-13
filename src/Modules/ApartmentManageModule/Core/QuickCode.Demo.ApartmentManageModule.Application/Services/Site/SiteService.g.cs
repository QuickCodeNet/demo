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
    public partial class SiteService : ISiteService
    {
        private readonly ILogger<SiteService> _logger;
        private readonly ISiteRepository _repository;
        public SiteService(ILogger<SiteService> logger, ISiteRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SiteDto>> InsertAsync(SiteDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SiteDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SiteDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SiteDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SiteDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveSitesResponseDto>>> GetActiveSitesAsync(bool sitesIsActive)
        {
            var returnValue = await _repository.GetActiveSitesAsync(sitesIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetSiteByIdResponseDto>> GetSiteByIdAsync(int sitesId)
        {
            var returnValue = await _repository.GetSiteByIdAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetFlatsCountBySiteAsync(int sitesId, bool sitesIsActive)
        {
            var returnValue = await _repository.GetFlatsCountBySiteAsync(sitesId, sitesIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetOwnersCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            var returnValue = await _repository.GetOwnersCountBySiteAsync(sitesId, sitesIsActive, flatContactsRelationshipType);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetTenantsCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            var returnValue = await _repository.GetTenantsCountBySiteAsync(sitesId, sitesIsActive, flatContactsRelationshipType);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetTotalPaymentsBySiteResponseDto>> GetTotalPaymentsBySiteAsync(int sitesId, bool flatPaymentsPaid)
        {
            var returnValue = await _repository.GetTotalPaymentsBySiteAsync(sitesId, flatPaymentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetApartmentsForSitesResponseDto>>> GetApartmentsForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetApartmentsForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetApartmentsForSitesResponseDto>> GetApartmentsForSitesDetailsAsync(int sitesId, int apartmentsId)
        {
            var returnValue = await _repository.GetApartmentsForSitesDetailsAsync(sitesId, apartmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatsForSitesResponseDto>>> GetFlatsForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetFlatsForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatsForSitesResponseDto>> GetFlatsForSitesDetailsAsync(int sitesId, int flatsId)
        {
            var returnValue = await _repository.GetFlatsForSitesDetailsAsync(sitesId, flatsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetSiteManagersForSitesResponseDto>>> GetSiteManagersForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetSiteManagersForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetSiteManagersForSitesResponseDto>> GetSiteManagersForSitesDetailsAsync(int sitesId, int siteManagersId)
        {
            var returnValue = await _repository.GetSiteManagersForSitesDetailsAsync(sitesId, siteManagersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForSitesResponseDto>>> GetFlatPaymentsForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetFlatPaymentsForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForSitesResponseDto>> GetFlatPaymentsForSitesDetailsAsync(int sitesId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForSitesDetailsAsync(sitesId, flatPaymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCommonExpensesForSitesResponseDto>>> GetCommonExpensesForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetCommonExpensesForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCommonExpensesForSitesResponseDto>> GetCommonExpensesForSitesDetailsAsync(int sitesId, int commonExpensesId)
        {
            var returnValue = await _repository.GetCommonExpensesForSitesDetailsAsync(sitesId, commonExpensesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetApartmentFeePlansForSitesResponseDto>>> GetApartmentFeePlansForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetApartmentFeePlansForSitesResponseDto>> GetApartmentFeePlansForSitesDetailsAsync(int sitesId, int apartmentFeePlansId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForSitesDetailsAsync(sitesId, apartmentFeePlansId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetExpenseInstallmentsForSitesResponseDto>>> GetExpenseInstallmentsForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetExpenseInstallmentsForSitesResponseDto>> GetExpenseInstallmentsForSitesDetailsAsync(int sitesId, int expenseInstallmentsId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForSitesDetailsAsync(sitesId, expenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatExpenseInstallmentsForSitesResponseDto>>> GetFlatExpenseInstallmentsForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatExpenseInstallmentsForSitesResponseDto>> GetFlatExpenseInstallmentsForSitesDetailsAsync(int sitesId, int flatExpenseInstallmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForSitesDetailsAsync(sitesId, flatExpenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUserSiteAccessesForSitesResponseDto>>> GetUserSiteAccessesForSitesAsync(int sitesId)
        {
            var returnValue = await _repository.GetUserSiteAccessesForSitesAsync(sitesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetUserSiteAccessesForSitesResponseDto>> GetUserSiteAccessesForSitesDetailsAsync(int sitesId, int userSiteAccessesId)
        {
            var returnValue = await _repository.GetUserSiteAccessesForSitesDetailsAsync(sitesId, userSiteAccessesId);
            return returnValue.ToResponse();
        }
    }
}