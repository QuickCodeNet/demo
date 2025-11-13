using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermission;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermission;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalPermissionsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PortalPermissionsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PortalPermissionsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PortalPermissionsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalPermissionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListPortalPermissionQuery(page, size));
            if (HandleResponseError(response, logger, "PortalPermission", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountPortalPermissionQuery());
            if (HandleResponseError(response, logger, "PortalPermission") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPermissionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string name)
        {
            var response = await mediator.Send(new GetItemPortalPermissionQuery(name));
            if (HandleResponseError(response, logger, "PortalPermission", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortalPermissionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PortalPermissionDto model)
        {
            var response = await mediator.Send(new InsertPortalPermissionCommand(model));
            if (HandleResponseError(response, logger, "PortalPermission") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { name = response.Value.Name }, response.Value);
        }

        [HttpPut("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string name, PortalPermissionDto model)
        {
            if (!(model.Name == name))
            {
                return BadRequest($"Name: '{name}' must be equal to model.Name: '{model.Name}'");
            }

            var response = await mediator.Send(new UpdatePortalPermissionCommand(name, model));
            if (HandleResponseError(response, logger, "PortalPermission", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var response = await mediator.Send(new DeleteItemPortalPermissionCommand(name));
            if (HandleResponseError(response, logger, "PortalPermission", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{portalPermissionName}/portal-permission-group")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPermissionGroupsForPortalPermissionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPortalPermissionsAsync(string portalPermissionsName)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupsForPortalPermissionsQuery(portalPermissionsName));
            if (HandleResponseError(response, logger, "PortalPermission", $"PortalPermissionsName: '{portalPermissionsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{portalPermissionName}/portal-permission-group/{portalPermissionGroupPortalPermissionName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPortalPermissionGroupsForPortalPermissionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissionGroupsForPortalPermissionsDetailsAsync(string portalPermissionsName, string portalPermissionGroupsPortalPermissionName)
        {
            var response = await mediator.Send(new GetPortalPermissionGroupsForPortalPermissionsDetailsQuery(portalPermissionsName, portalPermissionGroupsPortalPermissionName));
            if (HandleResponseError(response, logger, "PortalPermission", $"PortalPermissionsName: '{portalPermissionsName}', PortalPermissionGroupsPortalPermissionName: '{portalPermissionGroupsPortalPermissionName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}