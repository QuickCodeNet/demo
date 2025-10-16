﻿using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.TopicWorkflow;
using QuickCode.Demo.UserManagerModule.Application.Features.TopicWorkflow;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class TopicWorkflowsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<TopicWorkflowsController> logger;
        private readonly IServiceProvider serviceProvider;
        public TopicWorkflowsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<TopicWorkflowsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TopicWorkflowDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListTopicWorkflowQuery(page, size));
            if (HandleResponseError(response, logger, "TopicWorkflow", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountTopicWorkflowQuery());
            if (HandleResponseError(response, logger, "TopicWorkflow") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TopicWorkflowDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemTopicWorkflowQuery(id));
            if (HandleResponseError(response, logger, "TopicWorkflow", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TopicWorkflowDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(TopicWorkflowDto model)
        {
            var response = await mediator.Send(new InsertTopicWorkflowCommand(model));
            if (HandleResponseError(response, logger, "TopicWorkflow") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, TopicWorkflowDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateTopicWorkflowCommand(id, model));
            if (HandleResponseError(response, logger, "TopicWorkflow", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemTopicWorkflowCommand(id));
            if (HandleResponseError(response, logger, "TopicWorkflow", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-workflows/{topicWorkflowsKafkaEventsTopicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetWorkflowsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetWorkflowsAsync(string topicWorkflowsKafkaEventsTopicName)
        {
            var response = await mediator.Send(new GetWorkflowsQuery(topicWorkflowsKafkaEventsTopicName));
            if (HandleResponseError(response, logger, "TopicWorkflow", $"TopicWorkflowsKafkaEventsTopicName: '{topicWorkflowsKafkaEventsTopicName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-workflows-2/{topicWorkflowsKafkaEventsTopicName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetWORKFLOWS2ResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetWORKFLOWS2Async(string topicWorkflowsKafkaEventsTopicName)
        {
            var response = await mediator.Send(new GetWORKFLOWS2Query(topicWorkflowsKafkaEventsTopicName));
            if (HandleResponseError(response, logger, "TopicWorkflow", $"TopicWorkflowsKafkaEventsTopicName: '{topicWorkflowsKafkaEventsTopicName}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "TopicWorkflow", $"KafkaEventsTopicName: '{kafkaEventsTopicName}', ApiMethodDefinitionsHttpMethod: '{apiMethodDefinitionsHttpMethod}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}