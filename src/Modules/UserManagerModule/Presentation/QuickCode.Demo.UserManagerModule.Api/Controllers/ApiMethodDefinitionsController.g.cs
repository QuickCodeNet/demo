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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMethodDefinitionsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ApiMethodDefinitions Table";
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

            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ApiMethodDefinitionsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new ApiMethodDefinitionsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ApiMethodDefinitions Table";
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
            var response = await mediator.Send(new ApiMethodDefinitionsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsId}/kafka-events")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetKafkaEventsForApiMethodDefinitionsAsync(int apiMethodDefinitionsId)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery(apiMethodDefinitionsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsId: '{apiMethodDefinitionsId}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsId}/kafka-events/{kafkaEventsTopicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetKafkaEventsForApiMethodDefinitionsDetailsAsync(int apiMethodDefinitionsId, string kafkaEventsTopicName)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsQuery(apiMethodDefinitionsId, kafkaEventsTopicName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsId: '{apiMethodDefinitionsId}', KafkaEventsTopicName: '{kafkaEventsTopicName}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsId}/api-permission-groups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForApiMethodDefinitionsAsync(int apiMethodDefinitionsId)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsQuery(apiMethodDefinitionsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsId: '{apiMethodDefinitionsId}' not found in ApiMethodDefinitions Table";
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

        [HttpGet("{apiMethodDefinitionsId}/api-permission-groups/{apiPermissionGroupsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissionGroupsForApiMethodDefinitionsDetailsAsync(int apiMethodDefinitionsId, int apiPermissionGroupsId)
        {
            var response = await mediator.Send(new ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery(apiMethodDefinitionsId, apiPermissionGroupsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApiMethodDefinitionsId: '{apiMethodDefinitionsId}', ApiPermissionGroupsId: '{apiPermissionGroupsId}' not found in ApiMethodDefinitions Table";
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