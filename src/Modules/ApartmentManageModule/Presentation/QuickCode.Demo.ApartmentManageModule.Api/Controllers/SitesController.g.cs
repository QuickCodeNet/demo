using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Application.Features;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class SitesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<SitesController> logger;
        private readonly IServiceProvider serviceProvider;
        public SitesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<SitesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new SitesListQuery(page, size));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new SitesTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new SitesGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SitesDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SitesDto model)
        {
            var response = await mediator.Send(new SitesInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SitesDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new SitesUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new SitesDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-active-sites/{sitesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetActiveSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveSitesAsync(bool sitesIsActive)
        {
            var response = await mediator.Send(new SitesGetActiveSitesQuery(sitesIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesIsActive: '{sitesIsActive}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-site-by-id/{sitesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetSiteByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteByIdAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetSiteByIdQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-flats-count-by-site/{sitesId:int}/{sitesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsCountBySiteAsync(int sitesId, bool sitesIsActive)
        {
            var response = await mediator.Send(new SitesGetFlatsCountBySiteQuery(sitesId, sitesIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', SitesIsActive: '{sitesIsActive}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-owners-count-by-site/{sitesId:int}/{sitesIsActive:bool}/{flatContactsRelationshipType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOwnersCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            var response = await mediator.Send(new SitesGetOwnersCountBySiteQuery(sitesId, sitesIsActive, flatContactsRelationshipType));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', SitesIsActive: '{sitesIsActive}', FlatContactsRelationshipType: '{flatContactsRelationshipType}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-tenants-count-by-site/{sitesId:int}/{sitesIsActive:bool}/{flatContactsRelationshipType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTenantsCountBySiteAsync(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            var response = await mediator.Send(new SitesGetTenantsCountBySiteQuery(sitesId, sitesIsActive, flatContactsRelationshipType));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', SitesIsActive: '{sitesIsActive}', FlatContactsRelationshipType: '{flatContactsRelationshipType}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-total-payments-by-site/{sitesId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetTotalPaymentsBySiteResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalPaymentsBySiteAsync(int sitesId, bool flatPaymentsPaid)
        {
            var response = await mediator.Send(new SitesGetTotalPaymentsBySiteQuery(sitesId, flatPaymentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', FlatPaymentsPaid: '{flatPaymentsPaid}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/apartments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetApartmentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentsForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetApartmentsForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/apartments/{apartmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetApartmentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentsForSitesDetailsAsync(int sitesId, int apartmentsId)
        {
            var response = await mediator.Send(new SitesGetApartmentsForSitesDetailsQuery(sitesId, apartmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', ApartmentsId: '{apartmentsId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/flats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetFlatsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetFlatsForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/flats/{flatsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetFlatsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForSitesDetailsAsync(int sitesId, int flatsId)
        {
            var response = await mediator.Send(new SitesGetFlatsForSitesDetailsQuery(sitesId, flatsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', FlatsId: '{flatsId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/site-managers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetSiteManagersForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagersForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetSiteManagersForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/site-managers/{siteManagersId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetSiteManagersForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagersForSitesDetailsAsync(int sitesId, int siteManagersId)
        {
            var response = await mediator.Send(new SitesGetSiteManagersForSitesDetailsQuery(sitesId, siteManagersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', SiteManagersId: '{siteManagersId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetFlatPaymentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetFlatPaymentsForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetFlatPaymentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForSitesDetailsAsync(int sitesId, int flatPaymentsId)
        {
            var response = await mediator.Send(new SitesGetFlatPaymentsForSitesDetailsQuery(sitesId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', FlatPaymentsId: '{flatPaymentsId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/common-expenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetCommonExpensesForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetCommonExpensesForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/common-expenses/{commonExpensesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetCommonExpensesForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForSitesDetailsAsync(int sitesId, int commonExpensesId)
        {
            var response = await mediator.Send(new SitesGetCommonExpensesForSitesDetailsQuery(sitesId, commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', CommonExpensesId: '{commonExpensesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/apartment-fee-plans")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetApartmentFeePlansForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetApartmentFeePlansForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/apartment-fee-plans/{apartmentFeePlansId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetApartmentFeePlansForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForSitesDetailsAsync(int sitesId, int apartmentFeePlansId)
        {
            var response = await mediator.Send(new SitesGetApartmentFeePlansForSitesDetailsQuery(sitesId, apartmentFeePlansId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', ApartmentFeePlansId: '{apartmentFeePlansId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetExpenseInstallmentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetExpenseInstallmentsForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/expense-installments/{expenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetExpenseInstallmentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForSitesDetailsAsync(int sitesId, int expenseInstallmentsId)
        {
            var response = await mediator.Send(new SitesGetExpenseInstallmentsForSitesDetailsQuery(sitesId, expenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', ExpenseInstallmentsId: '{expenseInstallmentsId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/flat-expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetFlatExpenseInstallmentsForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetFlatExpenseInstallmentsForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/flat-expense-installments/{flatExpenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetFlatExpenseInstallmentsForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForSitesDetailsAsync(int sitesId, int flatExpenseInstallmentsId)
        {
            var response = await mediator.Send(new SitesGetFlatExpenseInstallmentsForSitesDetailsQuery(sitesId, flatExpenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/user-site-accesses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SitesGetUserSiteAccessesForSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForSitesAsync(int sitesId)
        {
            var response = await mediator.Send(new SitesGetUserSiteAccessesForSitesQuery(sitesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{sitesId}/user-site-accesses/{userSiteAccessesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SitesGetUserSiteAccessesForSitesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForSitesDetailsAsync(int sitesId, int userSiteAccessesId)
        {
            var response = await mediator.Send(new SitesGetUserSiteAccessesForSitesDetailsQuery(sitesId, userSiteAccessesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SitesId: '{sitesId}', UserSiteAccessesId: '{userSiteAccessesId}' not found in Sites Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }
    }
}