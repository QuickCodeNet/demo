﻿using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.KafkaEvent;
using QuickCode.Demo.UserManagerModule.Application.Features.KafkaEvent;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class KafkaEventsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<KafkaEventsController> logger;
        private readonly IServiceProvider serviceProvider;
        public KafkaEventsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<KafkaEventsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<KafkaEventDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListKafkaEventQuery(page, size));
            if (HandleResponseError(response, logger, "KafkaEvent", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountKafkaEventQuery());
            if (HandleResponseError(response, logger, "KafkaEvent") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{topicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KafkaEventDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string topicName)
        {
            var response = await mediator.Send(new GetItemKafkaEventQuery(topicName));
            if (HandleResponseError(response, logger, "KafkaEvent", $"TopicName: '{topicName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(KafkaEventDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(KafkaEventDto model)
        {
            var response = await mediator.Send(new InsertKafkaEventCommand(model));
            if (HandleResponseError(response, logger, "KafkaEvent") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { topicName = response.Value.TopicName }, response.Value);
        }

        [HttpPut("{topicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string topicName, KafkaEventDto model)
        {
            if (!(model.TopicName == topicName))
            {
                return BadRequest($"TopicName: '{topicName}' must be equal to model.TopicName: '{model.TopicName}'");
            }

            var response = await mediator.Send(new UpdateKafkaEventCommand(topicName, model));
            if (HandleResponseError(response, logger, "KafkaEvent", $"TopicName: '{topicName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{topicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string topicName)
        {
            var response = await mediator.Send(new DeleteItemKafkaEventCommand(topicName));
            if (HandleResponseError(response, logger, "KafkaEvent", $"TopicName: '{topicName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-kafka-events")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetKafkaEventsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetKafkaEventsAsync()
        {
            var response = await mediator.Send(new GetKafkaEventsQuery());
            if (HandleResponseError(response, logger, "KafkaEvent", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-kafka-events")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveKafkaEventsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveKafkaEventsAsync()
        {
            var response = await mediator.Send(new GetActiveKafkaEventsQuery());
            if (HandleResponseError(response, logger, "KafkaEvent", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-topic-workflows/{kafkaEventsTopicName}/{apiMethodDefinitionsHttpMethod}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTopicWorkflowsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTopicWorkflowsAsync(string kafkaEventsTopicName, HttpMethodType apiMethodDefinitionsHttpMethod)
        {
            var response = await mediator.Send(new GetTopicWorkflowsQuery(kafkaEventsTopicName, apiMethodDefinitionsHttpMethod));
            if (HandleResponseError(response, logger, "KafkaEvent", $"KafkaEventsTopicName: '{kafkaEventsTopicName}', ApiMethodDefinitionsHttpMethod: '{apiMethodDefinitionsHttpMethod}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{kafkaEventTopicName}/topic-workflow")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTopicWorkflowsForKafkaEventsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTopicWorkflowsForKafkaEventsAsync(string kafkaEventsTopicName)
        {
            var response = await mediator.Send(new GetTopicWorkflowsForKafkaEventsQuery(kafkaEventsTopicName));
            if (HandleResponseError(response, logger, "KafkaEvent", $"KafkaEventsTopicName: '{kafkaEventsTopicName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{kafkaEventTopicName}/topic-workflow/{topicWorkflowId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTopicWorkflowsForKafkaEventsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTopicWorkflowsForKafkaEventsDetailsAsync(string kafkaEventsTopicName, int topicWorkflowsId)
        {
            var response = await mediator.Send(new GetTopicWorkflowsForKafkaEventsDetailsQuery(kafkaEventsTopicName, topicWorkflowsId));
            if (HandleResponseError(response, logger, "KafkaEvent", $"KafkaEventsTopicName: '{kafkaEventsTopicName}', TopicWorkflowsId: '{topicWorkflowsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}