using AutoMapper;
using MediatR;
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
    public partial class KafkaEventsController : QuickCodeBaseApiController
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ILogger<KafkaEventsController> logger;
        private readonly IServiceProvider serviceProvider;
        public KafkaEventsController(IMapper mapper, IMediator mediator, IServiceProvider serviceProvider, ILogger<KafkaEventsController> logger)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<KafkaEventsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync(int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new KafkaEventsListQuery(page, size));
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
        public async Task<IActionResult> TotalItemCountAsync()
        {
            var response = await mediator.Send(new KafkaEventsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{topicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KafkaEventsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string topicName)
        {
            var response = await mediator.Send(new KafkaEventsGetItemQuery(topicName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"TopicName: '{topicName}' not found in KafkaEvents Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(KafkaEventsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(KafkaEventsDto model)
        {
            var response = await mediator.Send(new KafkaEventsInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { topicName = response.Value.TopicName }, response.Value);
        }

        [HttpPut("{topicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string topicName, KafkaEventsDto model)
        {
            if (!(model.TopicName == topicName))
            {
                return BadRequest($"TopicName: '{topicName}' must be equal to model.TopicName: '{model.TopicName}'");
            }

            var response = await mediator.Send(new KafkaEventsUpdateCommand(topicName, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"TopicName: '{topicName}' not found in KafkaEvents Table";
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

        [HttpDelete("{topicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string topicName)
        {
            var response = await mediator.Send(new KafkaEventsDeleteItemCommand(topicName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"TopicName: '{topicName}' not found in KafkaEvents Table";
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

        [HttpGet("get-kafka-events")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<KafkaEventsGetKafkaEventsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> KafkaEventsGetKafkaEventsAsync()
        {
            var response = await mediator.Send(new KafkaEventsKafkaEventsGetKafkaEventsQuery());
            if (response.Code == 404)
            {
                var notFoundMessage = $" not found in KafkaEvents Table";
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

        [HttpGet("get-active-kafka-events")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<KafkaEventsGetActiveKafkaEventsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> KafkaEventsGetActiveKafkaEventsAsync()
        {
            var response = await mediator.Send(new KafkaEventsKafkaEventsGetActiveKafkaEventsQuery());
            if (response.Code == 404)
            {
                var notFoundMessage = $" not found in KafkaEvents Table";
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

        [HttpGet("get-topic-workflows/{kafkaEventsTopicName}/{apiMethodDefinitionsHttpMethod}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<KafkaEventsGetTopicWorkflowsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> KafkaEventsGetTopicWorkflowsAsync(string kafkaEventsTopicName, string apiMethodDefinitionsHttpMethod)
        {
            var response = await mediator.Send(new KafkaEventsKafkaEventsGetTopicWorkflowsQuery(kafkaEventsTopicName, apiMethodDefinitionsHttpMethod));
            if (response.Code == 404)
            {
                var notFoundMessage = $"KafkaEventsTopicName: '{kafkaEventsTopicName}', ApiMethodDefinitionsHttpMethod: '{apiMethodDefinitionsHttpMethod}' not found in KafkaEvents Table";
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

        [HttpGet("{kafkaEventsTopicName}/topic-workflows")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<KafkaEventsTopicWorkflows_RESTResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> KafkaEventsTopicWorkflows_RESTAsync(string kafkaEventsTopicName)
        {
            var response = await mediator.Send(new KafkaEventsKafkaEventsTopicWorkflows_RESTQuery(kafkaEventsTopicName));
            if (response.Code == 404)
            {
                var notFoundMessage = $"KafkaEventsTopicName: '{kafkaEventsTopicName}' not found in KafkaEvents Table";
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

        [HttpGet("{kafkaEventsTopicName}/topic-workflows/{topicWorkflowsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KafkaEventsTopicWorkflows_KEY_RESTResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> KafkaEventsTopicWorkflows_KEY_RESTAsync(string kafkaEventsTopicName, int topicWorkflowsId)
        {
            var response = await mediator.Send(new KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTQuery(kafkaEventsTopicName, topicWorkflowsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"KafkaEventsTopicName: '{kafkaEventsTopicName}', TopicWorkflowsId: '{topicWorkflowsId}' not found in KafkaEvents Table";
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