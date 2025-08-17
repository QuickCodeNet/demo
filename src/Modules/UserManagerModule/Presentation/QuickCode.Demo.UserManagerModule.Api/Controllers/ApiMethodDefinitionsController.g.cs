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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiMethodDefinitionsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new ApiMethodDefinitionsListQuery(page, size));
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
            var response = await mediator.Send(new ApiMethodDefinitionsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMethodDefinitionsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string key)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetItemQuery(key));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Key: '{key}' not found in ApiMethodDefinitions Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiMethodDefinitionsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApiMethodDefinitionsDto model)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { key = response.Value.Key }, response.Value);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string key, ApiMethodDefinitionsDto model)
        {
            if (!(model.Key == key))
            {
                return BadRequest($"Key: '{key}' must be equal to model.Key: '{model.Key}'");
            }

            var response = await mediator.Send(new ApiMethodDefinitionsUpdateCommand(key, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Key: '{key}' not found in ApiMethodDefinitions Table";
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

        [HttpDelete("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsDeleteItemCommand(key));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Key: '{key}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsKey}/kafka-events")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetKafkaEventsForApiMethodDefinitionsAsync(string apiMethodDefinitionsKey)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery(apiMethodDefinitionsKey));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsKey}/kafka-events/{kafkaEventsTopicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetKafkaEventsForApiMethodDefinitionsDetailsAsync(string apiMethodDefinitionsKey, string kafkaEventsTopicName)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsQuery(apiMethodDefinitionsKey, kafkaEventsTopicName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}', KafkaEventsTopicName: '{kafkaEventsTopicName}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsKey}/api-permission-groups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForApiMethodDefinitionsAsync(string apiMethodDefinitionsKey)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsQuery(apiMethodDefinitionsKey));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsKey}/api-permission-groups/{apiPermissionGroupsPermissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForApiMethodDefinitionsDetailsAsync(string apiMethodDefinitionsKey, string apiPermissionGroupsPermissionGroupName)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery(apiMethodDefinitionsKey, apiPermissionGroupsPermissionGroupName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsKey: '{apiMethodDefinitionsKey}', ApiPermissionGroupsPermissionGroupName: '{apiPermissionGroupsPermissionGroupName}' not found in ApiMethodDefinitions Table";
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