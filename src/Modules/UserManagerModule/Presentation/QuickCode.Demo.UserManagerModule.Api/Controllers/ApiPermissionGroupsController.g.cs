using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiPermissionGroupDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListApiPermissionGroupQuery(page, size));
            if (HandleResponseError(response, logger, "ApiPermissionGroup", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountApiPermissionGroupQuery());
            if (HandleResponseError(response, logger, "ApiPermissionGroup") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPermissionGroupDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string permissionGroupName, string apiMethodDefinitionKey)
        {
            var response = await mediator.Send(new GetItemApiPermissionGroupQuery(permissionGroupName, apiMethodDefinitionKey));
            if (HandleResponseError(response, logger, "ApiPermissionGroup", $"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiPermissionGroupDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApiPermissionGroupDto model)
        {
            var response = await mediator.Send(new InsertApiPermissionGroupCommand(model));
            if (HandleResponseError(response, logger, "ApiPermissionGroup") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { permissionGroupName = response.Value.PermissionGroupName, apiMethodDefinitionKey = response.Value.ApiMethodDefinitionKey }, response.Value);
        }

        [HttpPut("{permissionGroupName}/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string permissionGroupName, string apiMethodDefinitionKey, ApiPermissionGroupDto model)
        {
            if (!(model.PermissionGroupName == permissionGroupName && model.ApiMethodDefinitionKey == apiMethodDefinitionKey))
            {
                return BadRequest($"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}' must be equal to model.PermissionGroupName: '{model.PermissionGroupName}', model.ApiMethodDefinitionKey: '{model.ApiMethodDefinitionKey}'");
            }

            var response = await mediator.Send(new UpdateApiPermissionGroupCommand(permissionGroupName, apiMethodDefinitionKey, model));
            if (HandleResponseError(response, logger, "ApiPermissionGroup", $"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{permissionGroupName}/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string permissionGroupName, string apiMethodDefinitionKey)
        {
            var response = await mediator.Send(new DeleteItemApiPermissionGroupCommand(permissionGroupName, apiMethodDefinitionKey));
            if (HandleResponseError(response, logger, "ApiPermissionGroup", $"PermissionGroupName: '{permissionGroupName}', ApiMethodDefinitionKey: '{apiMethodDefinitionKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-api-permission-group-names/{apiPermissionGroupsPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApiPermissionGroupNamesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupNamesAsync(string apiPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new GetApiPermissionGroupNamesQuery(apiPermissionGroupsPermissionGroupName));
            if (HandleResponseError(response, logger, "ApiPermissionGroup", $"ApiPermissionGroupsPermissionGroupName: '{apiPermissionGroupsPermissionGroupName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}