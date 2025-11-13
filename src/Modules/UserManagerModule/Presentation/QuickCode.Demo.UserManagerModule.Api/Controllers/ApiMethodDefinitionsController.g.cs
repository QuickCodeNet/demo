using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiMethodDefinition;
using QuickCode.Demo.UserManagerModule.Application.Features.ApiMethodDefinition;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class ApiMethodDefinitionsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<ApiMethodDefinitionsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ApiMethodDefinitionsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<ApiMethodDefinitionsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiMethodDefinitionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListApiMethodDefinitionQuery(page, size));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountApiMethodDefinitionQuery());
            if (HandleResponseError(response, logger, "ApiMethodDefinition") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMethodDefinitionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string key)
        {
            var response = await mediator.Send(new GetItemApiMethodDefinitionQuery(key));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiMethodDefinitionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApiMethodDefinitionDto model)
        {
            var response = await mediator.Send(new InsertApiMethodDefinitionCommand(model));
            if (HandleResponseError(response, logger, "ApiMethodDefinition") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { key = response.Value.Key }, response.Value);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string key, ApiMethodDefinitionDto model)
        {
            if (!(model.Key == key))
            {
                return BadRequest($"Key: '{key}' must be equal to model.Key: '{model.Key}'");
            }

            var response = await mediator.Send(new UpdateApiMethodDefinitionCommand(key, model));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var response = await mediator.Send(new DeleteItemApiMethodDefinitionCommand(key));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apiMethodDefinitionKey}/kafka-event")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetKafkaEventsForApiMethodDefinitionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetKafkaEventsForApiMethodDefinitionsAsync(string apiMethodDefinitionsKey)
        {
            var response = await mediator.Send(new GetKafkaEventsForApiMethodDefinitionsQuery(apiMethodDefinitionsKey));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apiMethodDefinitionKey}/kafka-event/{kafkaEventTopicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetKafkaEventsForApiMethodDefinitionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetKafkaEventsForApiMethodDefinitionsDetailsAsync(string apiMethodDefinitionsKey, string kafkaEventsTopicName)
        {
            var response = await mediator.Send(new GetKafkaEventsForApiMethodDefinitionsDetailsQuery(apiMethodDefinitionsKey, kafkaEventsTopicName));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}', KafkaEventsTopicName: '{kafkaEventsTopicName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apiMethodDefinitionKey}/api-permission-group")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApiPermissionGroupsForApiMethodDefinitionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForApiMethodDefinitionsAsync(string apiMethodDefinitionsKey)
        {
            var response = await mediator.Send(new GetApiPermissionGroupsForApiMethodDefinitionsQuery(apiMethodDefinitionsKey));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apiMethodDefinitionKey}/api-permission-group/{apiPermissionGroupPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApiPermissionGroupsForApiMethodDefinitionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForApiMethodDefinitionsDetailsAsync(string apiMethodDefinitionsKey, string apiPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new GetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery(apiMethodDefinitionsKey, apiPermissionGroupsPermissionGroupName));
            if (HandleResponseError(response, logger, "ApiMethodDefinition", $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}', ApiPermissionGroupsPermissionGroupName: '{apiPermissionGroupsPermissionGroupName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}