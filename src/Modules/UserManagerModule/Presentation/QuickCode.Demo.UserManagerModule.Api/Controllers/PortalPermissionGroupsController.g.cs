using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Features;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalPermissionGroupsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new PortalPermissionGroupsListQuery(page, size));
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
            var response = await mediator.Send(new PortalPermissionGroupsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{portalPermissionName}/{permissionGroupName}/{portalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPermissionGroupsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId)
        {
            var response = await mediator.Send(new PortalPermissionGroupsGetItemQuery(portalPermissionName, permissionGroupName, portalPermissionTypeId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}' not found in PortalPermissionGroups Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortalPermissionGroupsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PortalPermissionGroupsDto model)
        {
            var response = await mediator.Send(new PortalPermissionGroupsInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { portalPermissionName = response.Value.PortalPermissionName, permissionGroupName = response.Value.PermissionGroupName, portalPermissionTypeId = response.Value.PortalPermissionTypeId }, response.Value);
        }

        [HttpPut("{portalPermissionName}/{permissionGroupName}/{portalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId, PortalPermissionGroupsDto model)
        {
            if (!(model.PortalPermissionName == portalPermissionName && model.PermissionGroupName == permissionGroupName && model.PortalPermissionTypeId == portalPermissionTypeId))
            {
                return BadRequest($"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}' must be equal to model.PortalPermissionName: '{model.PortalPermissionName}', model.PermissionGroupName: '{model.PermissionGroupName}', model.PortalPermissionTypeId: '{model.PortalPermissionTypeId}'");
            }

            var response = await mediator.Send(new PortalPermissionGroupsUpdateCommand(portalPermissionName, permissionGroupName, portalPermissionTypeId, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}' not found in PortalPermissionGroups Table";
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

        [HttpDelete("{portalPermissionName}/{permissionGroupName}/{portalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId)
        {
            var response = await mediator.Send(new PortalPermissionGroupsDeleteItemCommand(portalPermissionName, permissionGroupName, portalPermissionTypeId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PortalPermissionName: '{portalPermissionName}', PermissionGroupName: '{permissionGroupName}', PortalPermissionTypeId: '{portalPermissionTypeId}' not found in PortalPermissionGroups Table";
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

        [HttpGet("get-portal-permission-groups/{portalPermissionGroupsPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalPermissionGroupsGetPortalPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsAsync(string portalPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new PortalPermissionGroupsGetPortalPermissionGroupsQuery(portalPermissionGroupsPermissionGroupName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PortalPermissionGroupsPermissionGroupName: '{portalPermissionGroupsPermissionGroupName}' not found in PortalPermissionGroups Table";
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

        [HttpGet("get-portal-permission-group/{portalPermissionGroupsPortalPermissionName}/{portalPermissionGroupsPermissionGroupName}/{portalPermissionGroupsPortalPermissionTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalPermissionGroupsGetPortalPermissionGroupResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupAsync(string portalPermissionGroupsPortalPermissionName, string portalPermissionGroupsPermissionGroupName, int portalPermissionGroupsPortalPermissionTypeId)
        {
            var response = await mediator.Send(new PortalPermissionGroupsGetPortalPermissionGroupQuery(portalPermissionGroupsPortalPermissionName, portalPermissionGroupsPermissionGroupName, portalPermissionGroupsPortalPermissionTypeId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PortalPermissionGroupsPortalPermissionName: '{portalPermissionGroupsPortalPermissionName}', PortalPermissionGroupsPermissionGroupName: '{portalPermissionGroupsPermissionGroupName}', PortalPermissionGroupsPortalPermissionTypeId: '{portalPermissionGroupsPortalPermissionTypeId}' not found in PortalPermissionGroups Table";
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