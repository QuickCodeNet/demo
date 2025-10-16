using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Features.PermissionGroup;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionGroupDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListPermissionGroupQuery(page, size));
            if (HandleResponseError(response, logger, "PermissionGroup", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountPermissionGroupQuery());
            if (HandleResponseError(response, logger, "PermissionGroup") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionGroupDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string name)
        {
            var response = await mediator.Send(new GetItemPermissionGroupQuery(name));
            if (HandleResponseError(response, logger, "PermissionGroup", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PermissionGroupDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PermissionGroupDto model)
        {
            var response = await mediator.Send(new InsertPermissionGroupCommand(model));
            if (HandleResponseError(response, logger, "PermissionGroup") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { name = response.Value.Name }, response.Value);
        }

        [HttpPut("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string name, PermissionGroupDto model)
        {
            if (!(model.Name == name))
            {
                return BadRequest($"Name: '{name}' must be equal to model.Name: '{model.Name}'");
            }

            var response = await mediator.Send(new UpdatePermissionGroupCommand(name, model));
            if (HandleResponseError(response, logger, "PermissionGroup", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var response = await mediator.Send(new DeleteItemPermissionGroupCommand(name));
            if (HandleResponseError(response, logger, "PermissionGroup", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/asp-net-user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAspNetUsersForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUsersForPermissionGroupsAsync(string permissionGroupsName)
        {
            var response = await mediator.Send(new GetAspNetUsersForPermissionGroupsQuery(permissionGroupsName));
            if (HandleResponseError(response, logger, "PermissionGroup", $"PermissionGroupsName: '{permissionGroupsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/asp-net-user/{aspNetUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAspNetUsersForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUsersForPermissionGroupsDetailsAsync(string permissionGroupsName, string aspNetUsersId)
        {
            var response = await mediator.Send(new GetAspNetUsersForPermissionGroupsDetailsQuery(permissionGroupsName, aspNetUsersId));
            if (HandleResponseError(response, logger, "PermissionGroup", $"PermissionGroupsName: '{permissionGroupsName}', AspNetUsersId: '{aspNetUsersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/portal-permission-group")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPermissionGroupsForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPermissionGroupsAsync(string permissionGroupsName)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupsForPermissionGroupsQuery(permissionGroupsName));
            if (HandleResponseError(response, logger, "PermissionGroup", $"PermissionGroupsName: '{permissionGroupsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/portal-permission-group/{portalPermissionGroupPortalPermissionName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPortalPermissionGroupsForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPermissionGroupsDetailsAsync(string permissionGroupsName, string portalPermissionGroupsPortalPermissionName)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupsForPermissionGroupsDetailsQuery(permissionGroupsName, portalPermissionGroupsPortalPermissionName));
            if (HandleResponseError(response, logger, "PermissionGroup", $"PermissionGroupsName: '{permissionGroupsName}', PortalPermissionGroupsPortalPermissionName: '{portalPermissionGroupsPortalPermissionName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/api-permission-group")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApiPermissionGroupsForPermissionGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForPermissionGroupsAsync(string permissionGroupsName)
        {
            var response = await mediator.Send(new GetApiPermissionGroupsForPermissionGroupsQuery(permissionGroupsName));
            if (HandleResponseError(response, logger, "PermissionGroup", $"PermissionGroupsName: '{permissionGroupsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{permissionGroupName}/api-permission-group/{apiPermissionGroupPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApiPermissionGroupsForPermissionGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForPermissionGroupsDetailsAsync(string permissionGroupsName, string apiPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new GetApiPermissionGroupsForPermissionGroupsDetailsQuery(permissionGroupsName, apiPermissionGroupsPermissionGroupName));
            if (HandleResponseError(response, logger, "PermissionGroup", $"PermissionGroupsName: '{permissionGroupsName}', ApiPermissionGroupsPermissionGroupName: '{apiPermissionGroupsPermissionGroupName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}