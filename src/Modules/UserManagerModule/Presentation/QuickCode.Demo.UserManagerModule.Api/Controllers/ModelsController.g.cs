using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.Model;
using QuickCode.Demo.UserManagerModule.Application.Features.Model;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class ModelsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<ModelsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ModelsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<ModelsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ModelDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListModelQuery(page, size));
            if (HandleResponseError(response, logger, "Model", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountModelQuery());
            if (HandleResponseError(response, logger, "Model") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{name}/{moduleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string name, string moduleName)
        {
            var response = await mediator.Send(new GetItemModelQuery(name, moduleName));
            if (HandleResponseError(response, logger, "Model", $"Name: '{name}', ModuleName: '{moduleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ModelDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ModelDto model)
        {
            var response = await mediator.Send(new InsertModelCommand(model));
            if (HandleResponseError(response, logger, "Model") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { name = response.Value.Name, moduleName = response.Value.ModuleName }, response.Value);
        }

        [HttpPut("{name}/{moduleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string name, string moduleName, ModelDto model)
        {
            if (!(model.Name == name && model.ModuleName == moduleName))
            {
                return BadRequest($"Name: '{name}', ModuleName: '{moduleName}' must be equal to model.Name: '{model.Name}', model.ModuleName: '{model.ModuleName}'");
            }

            var response = await mediator.Send(new UpdateModelCommand(name, moduleName, model));
            if (HandleResponseError(response, logger, "Model", $"Name: '{name}', ModuleName: '{moduleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{name}/{moduleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string name, string moduleName)
        {
            var response = await mediator.Send(new DeleteItemModelCommand(name, moduleName));
            if (HandleResponseError(response, logger, "Model", $"Name: '{name}', ModuleName: '{moduleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("module-name-is-exists/{modelsModuleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ModuleNameIsExistsAsync(string modelsModuleName)
        {
            var response = await mediator.Send(new ModuleNameIsExistsQuery(modelsModuleName));
            if (HandleResponseError(response, logger, "Model", $"ModelsModuleName: '{modelsModuleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{modelName}/api-method-definition")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApiMethodDefinitionsForModelsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiMethodDefinitionsForModelsAsync(string modelsName)
        {
            var response = await mediator.Send(new GetApiMethodDefinitionsForModelsQuery(modelsName));
            if (HandleResponseError(response, logger, "Model", $"ModelsName: '{modelsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{modelName}/api-method-definition/{apiMethodDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApiMethodDefinitionsForModelsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiMethodDefinitionsForModelsDetailsAsync(string modelsName, string apiMethodDefinitionsKey)
        {
            var response = await mediator.Send(new GetApiMethodDefinitionsForModelsDetailsQuery(modelsName, apiMethodDefinitionsKey));
            if (HandleResponseError(response, logger, "Model", $"ModelsName: '{modelsName}', ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{modelName}/portal-page-definition")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPortalPageDefinitionsForModelsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageDefinitionsForModelsAsync(string modelsName)
        {
            var response = await mediator.Send(new GetPortalPageDefinitionsForModelsQuery(modelsName));
            if (HandleResponseError(response, logger, "Model", $"ModelsName: '{modelsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{modelName}/portal-page-definition/{portalPageDefinitionKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPortalPageDefinitionsForModelsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageDefinitionsForModelsDetailsAsync(string modelsName, string portalPageDefinitionsKey)
        {
            var response = await mediator.Send(new GetPortalPageDefinitionsForModelsDetailsQuery(modelsName, portalPageDefinitionsKey));
            if (HandleResponseError(response, logger, "Model", $"ModelsName: '{modelsName}', PortalPageDefinitionsKey: '{portalPageDefinitionsKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("delete-models-with-module-name/{modelsModuleName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteModelsWithModuleNameAsync(string modelsModuleName)
        {
            var response = await mediator.Send(new DeleteModelsWithModuleNameCommand(modelsModuleName));
            if (HandleResponseError(response, logger, "Model", $"ModelsModuleName: '{modelsModuleName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}