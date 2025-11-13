using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalPermissionGroupsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PortalPermissionGroupsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PortalPermissionGroupsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PortalPermissionGroupsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalPermissionGroupDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListPortalPermissionGroupQuery(page, size));
            if (HandleResponseError(response, logger, "PortalPermissionGroup", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountPortalPermissionGroupQuery());
            if (HandleResponseError(response, logger, "PortalPermissionGroup") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{portalPermissionName}/{permissionGroupName}/{portalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPermissionGroupDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId)
        {
            var response = await mediator.Send(new GetItemPortalPermissionGroupQuery(portalPermissionName, permissionGroupName, portalPermissionTypeId));
            if (HandleResponseError(response, logger, "PortalPermissionGroup", $"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortalPermissionGroupDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PortalPermissionGroupDto model)
        {
            var response = await mediator.Send(new InsertPortalPermissionGroupCommand(model));
            if (HandleResponseError(response, logger, "PortalPermissionGroup") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { portalPermissionName = response.Value.PortalPermissionName, permissionGroupName = response.Value.PermissionGroupName, portalPermissionTypeId = response.Value.PortalPermissionTypeId }, response.Value);
        }

        [HttpPut("{portalPermissionName}/{permissionGroupName}/{portalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId, PortalPermissionGroupDto model)
        {
            if (!(model.PortalPermissionName == portalPermissionName && model.PermissionGroupName == permissionGroupName && model.PortalPermissionTypeId == portalPermissionTypeId))
            {
                return BadRequest($"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}' must be equal to model.PortalPermissionName: '{model.PortalPermissionName}', model.PermissionGroupName: '{model.PermissionGroupName}', model.PortalPermissionTypeId: '{model.PortalPermissionTypeId}'");
            }

            var response = await mediator.Send(new UpdatePortalPermissionGroupCommand(portalPermissionName, permissionGroupName, portalPermissionTypeId, model));
            if (HandleResponseError(response, logger, "PortalPermissionGroup", $"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{portalPermissionName}/{permissionGroupName}/{portalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId)
        {
            var response = await mediator.Send(new DeleteItemPortalPermissionGroupCommand(portalPermissionName, permissionGroupName, portalPermissionTypeId));
            if (HandleResponseError(response, logger, "PortalPermissionGroup", $"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-portal-permission-groups/{portalPermissionGroupsPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsAsync(string portalPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupsQuery(portalPermissionGroupsPermissionGroupName));
            if (HandleResponseError(response, logger, "PortalPermissionGroup", $"PortalPermissionGroupsPermissionGroupName: '{portalPermissionGroupsPermissionGroupName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-portal-permission-group/{portalPermissionGroupsPortalPermissionName}/{portalPermissionGroupsPermissionGroupName}/{portalPermissionGroupsPortalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPermissionGroupResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupAsync(string portalPermissionGroupsPortalPermissionName, string portalPermissionGroupsPermissionGroupName, int portalPermissionGroupsPortalPermissionTypeId)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupQuery(portalPermissionGroupsPortalPermissionName, portalPermissionGroupsPermissionGroupName, portalPermissionGroupsPortalPermissionTypeId));
            if (HandleResponseError(response, logger, "PortalPermissionGroup", $"PortalPermissionGroupsPortalPermissionName: '{portalPermissionGroupsPortalPermissionName}', PortalPermissionGroupsPermissionGroupName: '{portalPermissionGroupsPermissionGroupName}', PortalPermissionGroupsPortalPermissionTypeId: '{portalPermissionGroupsPortalPermissionTypeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}