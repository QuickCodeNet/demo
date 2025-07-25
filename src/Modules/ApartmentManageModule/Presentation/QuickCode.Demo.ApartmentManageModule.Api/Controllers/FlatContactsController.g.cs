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
    public partial class FlatContactsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<FlatContactsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatContactsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<FlatContactsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatContactsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new FlatContactsListQuery(page, size));
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
            var response = await mediator.Send(new FlatContactsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatContactsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new FlatContactsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatContacts Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatContactsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatContactsDto model)
        {
            var response = await mediator.Send(new FlatContactsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, FlatContactsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new FlatContactsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatContacts Table";
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
            var response = await mediator.Send(new FlatContactsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatContacts Table";
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

        [HttpGet("get-flat-owners/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatContactsGetFlatOwnersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatOwnersAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsGetFlatOwnersQuery(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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

        [HttpGet("get-flat-tenants/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatContactsGetFlatTenantsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatTenantsAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsGetFlatTenantsQuery(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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

        [HttpGet("get-contact-flats/{flatContactsContactId:int}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatContactsGetContactFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactFlatsAsync(int flatContactsContactId, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsGetContactFlatsQuery(flatContactsContactId, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsContactId: '{flatContactsContactId}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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

        [HttpGet("get-contact-owned-flats/{flatContactsContactId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatContactsGetContactOwnedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactOwnedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsGetContactOwnedFlatsQuery(flatContactsContactId, flatContactsRelationshipType, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsContactId: '{flatContactsContactId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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

        [HttpGet("get-contact-rented-flats/{flatContactsContactId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatContactsGetContactRentedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactRentedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsGetContactRentedFlatsQuery(flatContactsContactId, flatContactsRelationshipType, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsContactId: '{flatContactsContactId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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

        [HttpGet("check-flat-has-owner/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CheckFlatHasOwnerAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsCheckFlatHasOwnerQuery(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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

        [HttpGet("check-flat-has-tenant/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CheckFlatHasTenantAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsCheckFlatHasTenantQuery(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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

        [HttpGet("get-flat-contacts-count/{flatContactsFlatId:int}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsCountAsync(int flatContactsFlatId, bool flatContactsIsActive)
        {
            var response = await mediator.Send(new FlatContactsGetFlatContactsCountQuery(flatContactsFlatId, flatContactsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsIsActive: '{flatContactsIsActive}' not found in FlatContacts Table";
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