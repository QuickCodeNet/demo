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
    public partial class ApiPermissionGroupsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<ApiPermissionGroupsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ApiPermissionGroupsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<ApiPermissionGroupsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiPermissionGroupsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new ApiPermissionGroupsListQuery(page, size));
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
            var response = await mediator.Send(new ApiPermissionGroupsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPermissionGroupsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string permissionGroupName, string apiMethodDefinitionKey)
        {
            var response = await mediator.Send(new ApiPermissionGroupsGetItemQuery(permissionGroupName, apiMethodDefinitionKey));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}' not found in ApiPermissionGroups Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiPermissionGroupsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApiPermissionGroupsDto model)
        {
            var response = await mediator.Send(new ApiPermissionGroupsInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { permissionGroupName = response.Value.PermissionGroupName, apiMethodDefinitionKey = response.Value.ApiMethodDefinitionKey }, response.Value);
        }

        [HttpPut("{permissionGroupName}/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string permissionGroupName, string apiMethodDefinitionKey, ApiPermissionGroupsDto model)
        {
            if (!(model.PermissionGroupName == permissionGroupName && model.ApiMethodDefinitionKey == apiMethodDefinitionKey))
            {
                return BadRequest($"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}' must be equal to model.PermissionGroupName: '{model.PermissionGroupName}', model.ApiMethodDefinitionKey: '{model.ApiMethodDefinitionKey}'");
            }

            var response = await mediator.Send(new ApiPermissionGroupsUpdateCommand(permissionGroupName, apiMethodDefinitionKey, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}' not found in ApiPermissionGroups Table";
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

        [HttpDelete("{permissionGroupName}/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string permissionGroupName, string apiMethodDefinitionKey)
        {
            var response = await mediator.Send(new ApiPermissionGroupsDeleteItemCommand(permissionGroupName, apiMethodDefinitionKey));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}' not found in ApiPermissionGroups Table";
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

        [HttpGet("get-api-permission-groups/{apiPermissionGroupsPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiPermissionGroupsGetApiPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsAsync(string apiPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new ApiPermissionGroupsGetApiPermissionGroupsQuery(apiPermissionGroupsPermissionGroupName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiPermissionGroupsPermissionGroupName: '{apiPermissionGroupsPermissionGroupName}' not found in ApiPermissionGroups Table";
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

        [HttpGet("get-api-permission-group/{apiPermissionGroupsPermissionGroupName}/{apiPermissionGroupsApiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiPermissionGroupsGetApiPermissionGroupResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupAsync(string apiPermissionGroupsPermissionGroupName, string apiPermissionGroupsApiMethodDefinitionKey)
        {
            var response = await mediator.Send(new ApiPermissionGroupsGetApiPermissionGroupQuery(apiPermissionGroupsPermissionGroupName, apiPermissionGroupsApiMethodDefinitionKey));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiPermissionGroupsPermissionGroupName: '{apiPermissionGroupsPermissionGroupName}', ApiPermissionGroupsApiMethodDefinitionKey: '{apiPermissionGroupsApiMethodDefinitionKey}' not found in ApiPermissionGroups Table";
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