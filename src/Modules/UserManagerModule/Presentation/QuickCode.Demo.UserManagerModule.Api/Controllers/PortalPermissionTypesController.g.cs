using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalPermissionTypesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PortalPermissionTypesController> logger;
        private readonly IServiceProvider serviceProvider;
        public PortalPermissionTypesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PortalPermissionTypesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalPermissionTypeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListPortalPermissionTypeQuery(page, size));
            if (HandleResponseError(response, logger, "PortalPermissionType", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountPortalPermissionTypeQuery());
            if (HandleResponseError(response, logger, "PortalPermissionType") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPermissionTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemPortalPermissionTypeQuery(id));
            if (HandleResponseError(response, logger, "PortalPermissionType", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortalPermissionTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PortalPermissionTypeDto model)
        {
            var response = await mediator.Send(new InsertPortalPermissionTypeCommand(model));
            if (HandleResponseError(response, logger, "PortalPermissionType") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, PortalPermissionTypeDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdatePortalPermissionTypeCommand(id, model));
            if (HandleResponseError(response, logger, "PortalPermissionType", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemPortalPermissionTypeCommand(id));
            if (HandleResponseError(response, logger, "PortalPermissionType", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{portalPermissionTypeId}/portal-permission-group")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPermissionGroupsForPortalPermissionTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPortalPermissionTypesAsync(int portalPermissionTypesId)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupsForPortalPermissionTypesQuery(portalPermissionTypesId));
            if (HandleResponseError(response, logger, "PortalPermissionType", $"PortalPermissionTypesId: '{portalPermissionTypesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{portalPermissionTypeId}/portal-permission-group/{portalPermissionGroupPortalPermissionName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPortalPermissionGroupsForPortalPermissionTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPortalPermissionTypesDetailsAsync(int portalPermissionTypesId, string portalPermissionGroupsPortalPermissionName)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery(portalPermissionTypesId, portalPermissionGroupsPortalPermissionName));
            if (HandleResponseError(response, logger, "PortalPermissionType", $"PortalPermissionTypesId: '{portalPermissionTypesId}', PortalPermissionGroupsPortalPermissionName: '{portalPermissionGroupsPortalPermissionName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}