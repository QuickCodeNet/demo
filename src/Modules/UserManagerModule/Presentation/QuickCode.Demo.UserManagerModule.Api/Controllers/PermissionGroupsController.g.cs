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
    public partial class PermissionGroupsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PermissionGroupsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PermissionGroupsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PermissionGroupsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new PermissionGroupsListQuery(page, size));
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
            var response = await mediator.Send(new PermissionGroupsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string name)
        {
            var response = await mediator.Send(new PermissionGroupsGetItemQuery(name));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Name: '{name}' not found in PermissionGroups Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PermissionGroupsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PermissionGroupsDto model)
        {
            var response = await mediator.Send(new PermissionGroupsInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { name = response.Value.Name }, response.Value);
        }

        [HttpPut("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string name, PermissionGroupsDto model)
        {
            if (!(model.Name == name))
            {
                return BadRequest($"Name: '{name}' must be equal to model.Name: '{model.Name}'");
            }

            var response = await mediator.Send(new PermissionGroupsUpdateCommand(name, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Name: '{name}' not found in PermissionGroups Table";
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

        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var response = await mediator.Send(new PermissionGroupsDeleteItemCommand(name));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Name: '{name}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsName}/asp-net-users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUsersForPermissionGroupsAsync(string permissionGroupsName)
        {
            var response = await mediator.Send(new PermissionGroupsGetAspNetUsersForPermissionGroupsQuery(permissionGroupsName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsName: '{permissionGroupsName}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsName}/asp-net-users/{aspNetUsersId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUsersForPermissionGroupsDetailsAsync(string permissionGroupsName, string aspNetUsersId)
        {
            var response = await mediator.Send(new PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsQuery(permissionGroupsName, aspNetUsersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsName: '{permissionGroupsName}', AspNetUsersId: '{aspNetUsersId}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsName}/portal-permission-groups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPermissionGroupsAsync(string permissionGroupsName)
        {
            var response = await mediator.Send(new PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsQuery(permissionGroupsName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsName: '{permissionGroupsName}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsName}/portal-permission-groups/{portalPermissionGroupsPortalPermissionName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPermissionGroupsDetailsAsync(string permissionGroupsName, string portalPermissionGroupsPortalPermissionName)
        {
            var response = await mediator.Send(new PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsQuery(permissionGroupsName, portalPermissionGroupsPortalPermissionName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsName: '{permissionGroupsName}', PortalPermissionGroupsPortalPermissionName: '{portalPermissionGroupsPortalPermissionName}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsName}/api-permission-groups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForPermissionGroupsAsync(string permissionGroupsName)
        {
            var response = await mediator.Send(new PermissionGroupsGetApiPermissionGroupsForPermissionGroupsQuery(permissionGroupsName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsName: '{permissionGroupsName}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsName}/api-permission-groups/{apiPermissionGroupsPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForPermissionGroupsDetailsAsync(string permissionGroupsName, string apiPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery(permissionGroupsName, apiPermissionGroupsPermissionGroupName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsName: '{permissionGroupsName}', ApiPermissionGroupsPermissionGroupName: '{apiPermissionGroupsPermissionGroupName}' not found in PermissionGroups Table";
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