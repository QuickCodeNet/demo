using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Site;
using QuickCode.Demo.ApartmentManageModule.Application.Services.Site;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class SitesController : QuickCodeBaseApiController
    {
        private readonly ISiteService service;
        private readonly ILogger<SitesController> logger;
        private readonly IServiceProvider serviceProvider;
        public SitesController(ISiteService service, IServiceProvider serviceProvider, ILogger<SitesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SiteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Site", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Site") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SiteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Site", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SiteDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SiteDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Site") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SiteDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Site", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await service.DeleteItemAsync(id);
            if (HandleResponseError(response, logger, "Site", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-sites/{sitesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveSitesAsync(bool sitesIsActive)
        {
            var response = await service.GetActiveSitesAsync(sitesIsActive);
            if (HandleResponseError(response, logger, "Site", $"SitesIsActive: '{sitesIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-site-by-id/{sitesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSiteByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteByIdAsync(int sitesId)
        {
            var response = await service.GetSiteByIdAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flats-count-by-site/{sitesId:int}/{sitesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsCountBySiteAsync(int sitesId, bool sitesIsActive)
        {
            var response = await service.GetFlatsCountBySiteAsync(sitesId, sitesIsActive);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', SitesIsActive: '{sitesIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-owners-count-by-site/{sitesId:int}/{sitesIsActive:bool}/{flatContactsRelationshipType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOwnersCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            var response = await service.GetOwnersCountBySiteAsync(sitesId, sitesIsActive, flatContactsRelationshipType);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', SitesIsActive: '{sitesIsActive}', FlatContactsRelationshipType: '{flatContactsRelationshipType}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-tenants-count-by-site/{sitesId:int}/{sitesIsActive:bool}/{flatContactsRelationshipType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTenantsCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            var response = await service.GetTenantsCountBySiteAsync(sitesId, sitesIsActive, flatContactsRelationshipType);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', SitesIsActive: '{sitesIsActive}', FlatContactsRelationshipType: '{flatContactsRelationshipType}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-total-payments-by-site/{sitesId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTotalPaymentsBySiteResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalPaymentsBySiteAsync(int sitesId, bool flatPaymentsPaid)
        {
            var response = await service.GetTotalPaymentsBySiteAsync(sitesId, flatPaymentsPaid);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', FlatPaymentsPaid: '{flatPaymentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/apartment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentsForSitesAsync(int sitesId)
        {
            var response = await service.GetApartmentsForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/apartment/{apartmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApartmentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentsForSitesDetailsAsync(int sitesId, int apartmentsId)
        {
            var response = await service.GetApartmentsForSitesDetailsAsync(sitesId, apartmentsId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', ApartmentsId: '{apartmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/flat")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForSitesAsync(int sitesId)
        {
            var response = await service.GetFlatsForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/flat/{flatId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForSitesDetailsAsync(int sitesId, int flatsId)
        {
            var response = await service.GetFlatsForSitesDetailsAsync(sitesId, flatsId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', FlatsId: '{flatsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/site-manager")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSiteManagersForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagersForSitesAsync(int sitesId)
        {
            var response = await service.GetSiteManagersForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/site-manager/{siteManagerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSiteManagersForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagersForSitesDetailsAsync(int sitesId, int siteManagersId)
        {
            var response = await service.GetSiteManagersForSitesDetailsAsync(sitesId, siteManagersId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', SiteManagersId: '{siteManagersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForSitesAsync(int sitesId)
        {
            var response = await service.GetFlatPaymentsForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForSitesDetailsAsync(int sitesId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForSitesDetailsAsync(sitesId, flatPaymentsId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/common-expense")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCommonExpensesForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForSitesAsync(int sitesId)
        {
            var response = await service.GetCommonExpensesForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/common-expense/{commonExpenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCommonExpensesForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForSitesDetailsAsync(int sitesId, int commonExpensesId)
        {
            var response = await service.GetCommonExpensesForSitesDetailsAsync(sitesId, commonExpensesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/apartment-fee-plan")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentFeePlansForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForSitesAsync(int sitesId)
        {
            var response = await service.GetApartmentFeePlansForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/apartment-fee-plan/{apartmentFeePlanId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApartmentFeePlansForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForSitesDetailsAsync(int sitesId, int apartmentFeePlansId)
        {
            var response = await service.GetApartmentFeePlansForSitesDetailsAsync(sitesId, apartmentFeePlansId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', ApartmentFeePlansId: '{apartmentFeePlansId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpenseInstallmentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForSitesAsync(int sitesId)
        {
            var response = await service.GetExpenseInstallmentsForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/expense-installment/{expenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExpenseInstallmentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForSitesDetailsAsync(int sitesId, int expenseInstallmentsId)
        {
            var response = await service.GetExpenseInstallmentsForSitesDetailsAsync(sitesId, expenseInstallmentsId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', ExpenseInstallmentsId: '{expenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/flat-expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatExpenseInstallmentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForSitesAsync(int sitesId)
        {
            var response = await service.GetFlatExpenseInstallmentsForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/flat-expense-installment/{flatExpenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatExpenseInstallmentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForSitesDetailsAsync(int sitesId, int flatExpenseInstallmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForSitesDetailsAsync(sitesId, flatExpenseInstallmentsId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/user-site-access")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserSiteAccessesForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForSitesAsync(int sitesId)
        {
            var response = await service.GetUserSiteAccessesForSitesAsync(sitesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{siteId}/user-site-access/{userSiteAccessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserSiteAccessesForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForSitesDetailsAsync(int sitesId, int userSiteAccessesId)
        {
            var response = await service.GetUserSiteAccessesForSitesDetailsAsync(sitesId, userSiteAccessesId);
            if (HandleResponseError(response, logger, "Site", $"SitesId: '{sitesId}', UserSiteAccessesId: '{userSiteAccessesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}