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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new PermissionGroupsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PermissionGroups Table";
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

            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, PermissionGroupsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new PermissionGroupsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PermissionGroups Table";
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
            var response = await mediator.Send(new PermissionGroupsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsId}/asp-net-users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUsersForPermissionGroupsAsync(int permissionGroupsId)
        {
            var response = await mediator.Send(new PermissionGroupsGetAspNetUsersForPermissionGroupsQuery(permissionGroupsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsId: '{permissionGroupsId}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsId}/asp-net-users/{aspNetUsersId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUsersForPermissionGroupsDetailsAsync(int permissionGroupsId, string aspNetUsersId)
        {
            var response = await mediator.Send(new PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsQuery(permissionGroupsId, aspNetUsersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsId: '{permissionGroupsId}', AspNetUsersId: '{aspNetUsersId}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsId}/portal-permission-groups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPermissionGroupsAsync(int permissionGroupsId)
        {
            var response = await mediator.Send(new PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsQuery(permissionGroupsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsId: '{permissionGroupsId}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsId}/portal-permission-groups/{portalPermissionGroupsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPermissionGroupsDetailsAsync(int permissionGroupsId, int portalPermissionGroupsId)
        {
            var response = await mediator.Send(new PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsQuery(permissionGroupsId, portalPermissionGroupsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsId: '{permissionGroupsId}', PortalPermissionGroupsId: '{portalPermissionGroupsId}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsId}/api-permission-groups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForPermissionGroupsAsync(int permissionGroupsId)
        {
            var response = await mediator.Send(new PermissionGroupsGetApiPermissionGroupsForPermissionGroupsQuery(permissionGroupsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsId: '{permissionGroupsId}' not found in PermissionGroups Table";
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

        [HttpGet("{permissionGroupsId}/api-permission-groups/{apiPermissionGroupsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForPermissionGroupsDetailsAsync(int permissionGroupsId, int apiPermissionGroupsId)
        {
            var response = await mediator.Send(new PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery(permissionGroupsId, apiPermissionGroupsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupsId: '{permissionGroupsId}', ApiPermissionGroupsId: '{apiPermissionGroupsId}' not found in PermissionGroups Table";
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