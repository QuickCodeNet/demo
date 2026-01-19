using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.Module;
using QuickCode.Demo.UserManagerModule.Application.Features.Module;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class ModulesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<ModulesController> logger;
        private readonly IServiceProvider serviceProvider;
        public ModulesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<ModulesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModuleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListModuleQuery(page, size));
            if (HandleResponseError(response, logger, "Module", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountModuleQuery());
            if (HandleResponseError(response, logger, "Module") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string name)
        {
            var response = await mediator.Send(new GetItemModuleQuery(name));
            if (HandleResponseError(response, logger, "Module", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ModuleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ModuleDto model)
        {
            var response = await mediator.Send(new InsertModuleCommand(model));
            if (HandleResponseError(response, logger, "Module") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { name = response.Value.Name }, response.Value);
        }

        [HttpPut("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string name, ModuleDto model)
        {
            if (!(model.Name == name))
            {
                return BadRequest($"Name: '{name}' must be equal to model.Name: '{model.Name}'");
            }

            var response = await mediator.Send(new UpdateModuleCommand(name, model));
            if (HandleResponseError(response, logger, "Module", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var response = await mediator.Send(new DeleteItemModuleCommand(name));
            if (HandleResponseError(response, logger, "Module", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("module-name-is-exists/{modulesName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ModuleNameIsExistsAsync(string modulesName)
        {
            var response = await mediator.Send(new ModuleNameIsExistsQuery(modulesName));
            if (HandleResponseError(response, logger, "Module", $"ModulesName: '{modulesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{moduleName}/model")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetModelsForModulesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetModelsForModulesAsync(string modulesName)
        {
            var response = await mediator.Send(new GetModelsForModulesQuery(modulesName));
            if (HandleResponseError(response, logger, "Module", $"ModulesName: '{modulesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{moduleName}/model/{modelName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetModelsForModulesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetModelsForModulesDetailsAsync(string modulesName, string modelsName)
        {
            var response = await mediator.Send(new GetModelsForModulesDetailsQuery(modulesName, modelsName));
            if (HandleResponseError(response, logger, "Module", $"ModulesName: '{modulesName}', ModelsName: '{modelsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{moduleName}/api-method-definition")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApiMethodDefinitionsForModulesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiMethodDefinitionsForModulesAsync(string modulesName)
        {
            var response = await mediator.Send(new GetApiMethodDefinitionsForModulesQuery(modulesName));
            if (HandleResponseError(response, logger, "Module", $"ModulesName: '{modulesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{moduleName}/api-method-definition/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApiMethodDefinitionsForModulesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiMethodDefinitionsForModulesDetailsAsync(string modulesName, string apiMethodDefinitionsKey)
        {
            var response = await mediator.Send(new GetApiMethodDefinitionsForModulesDetailsQuery(modulesName, apiMethodDefinitionsKey));
            if (HandleResponseError(response, logger, "Module", $"ModulesName: '{modulesName}', ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{moduleName}/portal-page-definition")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPageDefinitionsForModulesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageDefinitionsForModulesAsync(string modulesName)
        {
            var response = await mediator.Send(new GetPortalPageDefinitionsForModulesQuery(modulesName));
            if (HandleResponseError(response, logger, "Module", $"ModulesName: '{modulesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{moduleName}/portal-page-definition/{portalPageDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPortalPageDefinitionsForModulesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageDefinitionsForModulesDetailsAsync(string modulesName, string portalPageDefinitionsKey)
        {
            var response = await mediator.Send(new GetPortalPageDefinitionsForModulesDetailsQuery(modulesName, portalPageDefinitionsKey));
            if (HandleResponseError(response, logger, "Module", $"ModulesName: '{modulesName}', PortalPageDefinitionsKey: '{portalPageDefinitionsKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}