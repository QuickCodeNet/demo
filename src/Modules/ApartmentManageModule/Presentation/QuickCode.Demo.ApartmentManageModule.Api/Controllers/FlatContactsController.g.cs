using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatContact;
using QuickCode.Demo.ApartmentManageModule.Application.Services.FlatContact;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class FlatContactsController : QuickCodeBaseApiController
    {
        private readonly IFlatContactService service;
        private readonly ILogger<FlatContactsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatContactsController(IFlatContactService service, IServiceProvider serviceProvider, ILogger<FlatContactsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatContactDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "FlatContact", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "FlatContact") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatContactDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "FlatContact", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatContactDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatContactDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "FlatContact") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, FlatContactDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "FlatContact", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "FlatContact", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-owners/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatOwnersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatOwnersAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await service.GetFlatOwnersAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-tenants/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatTenantsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatTenantsAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await service.GetFlatTenantsAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contact-flats/{flatContactsContactId:int}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetContactFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactFlatsAsync(int flatContactsContactId, bool flatContactsIsActive)
        {
            var response = await service.GetContactFlatsAsync(flatContactsContactId, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsContactId: '{flatContactsContactId}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contact-owned-flats/{flatContactsContactId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetContactOwnedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactOwnedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await service.GetContactOwnedFlatsAsync(flatContactsContactId, flatContactsRelationshipType, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsContactId: '{flatContactsContactId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contact-rented-flats/{flatContactsContactId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetContactRentedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactRentedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await service.GetContactRentedFlatsAsync(flatContactsContactId, flatContactsRelationshipType, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsContactId: '{flatContactsContactId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("check-flat-has-owner/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CheckFlatHasOwnerAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await service.CheckFlatHasOwnerAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("check-flat-has-tenant/{flatContactsFlatId:int}/{flatContactsRelationshipType}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CheckFlatHasTenantAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var response = await service.CheckFlatHasTenantAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsRelationshipType: '{flatContactsRelationshipType}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-contacts-count/{flatContactsFlatId:int}/{flatContactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsCountAsync(int flatContactsFlatId, bool flatContactsIsActive)
        {
            var response = await service.GetFlatContactsCountAsync(flatContactsFlatId, flatContactsIsActive);
            if (HandleResponseError(response, logger, "FlatContact", $"FlatContactsFlatId: '{flatContactsFlatId}', FlatContactsIsActive: '{flatContactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}