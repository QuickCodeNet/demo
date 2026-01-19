using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPageDefinition;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPageDefinition;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalPageDefinitionsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PortalPageDefinitionsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PortalPageDefinitionsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PortalPageDefinitionsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalPageDefinitionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListPortalPageDefinitionQuery(page, size));
            if (HandleResponseError(response, logger, "PortalPageDefinition", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountPortalPageDefinitionQuery());
            if (HandleResponseError(response, logger, "PortalPageDefinition") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPageDefinitionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string key)
        {
            var response = await mediator.Send(new GetItemPortalPageDefinitionQuery(key));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortalPageDefinitionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PortalPageDefinitionDto model)
        {
            var response = await mediator.Send(new InsertPortalPageDefinitionCommand(model));
            if (HandleResponseError(response, logger, "PortalPageDefinition") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { key = response.Value.Key }, response.Value);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string key, PortalPageDefinitionDto model)
        {
            if (!(model.Key == key))
            {
                return BadRequest($"Key: '{key}' must be equal to model.Key: '{model.Key}'");
            }

            var response = await mediator.Send(new UpdatePortalPageDefinitionCommand(key, model));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var response = await mediator.Send(new DeleteItemPortalPageDefinitionCommand(key));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-portal-page-definitions-with-module-name/{portalPageDefinitionsModuleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPageDefinitionsWithModuleNameResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageDefinitionsWithModuleNameAsync(string portalPageDefinitionsModuleName)
        {
            var response = await mediator.Send(new GetPortalPageDefinitionsWithModuleNameQuery(portalPageDefinitionsModuleName));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsModuleName: '{portalPageDefinitionsModuleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-portal-page-definitions-with-model-name/{portalPageDefinitionsModelName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPageDefinitionsWithModelNameResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageDefinitionsWithModelNameAsync(string portalPageDefinitionsModelName)
        {
            var response = await mediator.Send(new GetPortalPageDefinitionsWithModelNameQuery(portalPageDefinitionsModelName));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsModelName: '{portalPageDefinitionsModelName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-portal-page-definitions-with-module-name/{portalPageDefinitionsModuleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsPortalPageDefinitionsWithModuleNameAsync(string portalPageDefinitionsModuleName)
        {
            var response = await mediator.Send(new ExistsPortalPageDefinitionsWithModuleNameQuery(portalPageDefinitionsModuleName));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsModuleName: '{portalPageDefinitionsModuleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-portal-page-definitions-with-model-name/{portalPageDefinitionsModelName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsPortalPageDefinitionsWithModelNameAsync(string portalPageDefinitionsModelName)
        {
            var response = await mediator.Send(new ExistsPortalPageDefinitionsWithModelNameQuery(portalPageDefinitionsModelName));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsModelName: '{portalPageDefinitionsModelName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{portalPageDefinitionKey}/portal-page-access-grant")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPageAccessGrantsForPortalPageDefinitionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageAccessGrantsForPortalPageDefinitionsAsync(string portalPageDefinitionsKey)
        {
            var response = await mediator.Send(new GetPortalPageAccessGrantsForPortalPageDefinitionsQuery(portalPageDefinitionsKey));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsKey: '{portalPageDefinitionsKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{portalPageDefinitionKey}/portal-page-access-grant/{portalPageAccessGrantPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPortalPageAccessGrantsForPortalPageDefinitionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageAccessGrantsForPortalPageDefinitionsDetailsAsync(string portalPageDefinitionsKey, string portalPageAccessGrantsPermissionGroupName)
        {
            var response = await mediator.Send(new GetPortalPageAccessGrantsForPortalPageDefinitionsDetailsQuery(portalPageDefinitionsKey, portalPageAccessGrantsPermissionGroupName));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsKey: '{portalPageDefinitionsKey}', PortalPageAccessGrantsPermissionGroupName: '{portalPageAccessGrantsPermissionGroupName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("delete-portal-page-definitions-with-module-name/{portalPageDefinitionsModuleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeletePortalPageDefinitionsWithModuleNameAsync(string portalPageDefinitionsModuleName)
        {
            var response = await mediator.Send(new DeletePortalPageDefinitionsWithModuleNameCommand(portalPageDefinitionsModuleName));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsModuleName: '{portalPageDefinitionsModuleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("delete-portal-page-definitions-with-model-name/{portalPageDefinitionsModelName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeletePortalPageDefinitionsWithModelNameAsync(string portalPageDefinitionsModelName)
        {
            var response = await mediator.Send(new DeletePortalPageDefinitionsWithModelNameCommand(portalPageDefinitionsModelName));
            if (HandleResponseError(response, logger, "PortalPageDefinition", $"PortalPageDefinitionsModelName: '{portalPageDefinitionsModelName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}