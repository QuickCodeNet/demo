using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Application.Features.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class MessageQueuesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<MessageQueuesController> logger;
        private readonly IServiceProvider serviceProvider;
        public MessageQueuesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<MessageQueuesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MessageQueueDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListMessageQueueQuery(page, size));
            if (HandleResponseError(response, logger, "MessageQueue", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountMessageQueueQuery());
            if (HandleResponseError(response, logger, "MessageQueue") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageQueueDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemMessageQueueQuery(id));
            if (HandleResponseError(response, logger, "MessageQueue", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageQueueDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(MessageQueueDto model)
        {
            var response = await mediator.Send(new InsertMessageQueueCommand(model));
            if (HandleResponseError(response, logger, "MessageQueue") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, MessageQueueDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateMessageQueueCommand(id, model));
            if (HandleResponseError(response, logger, "MessageQueue", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemMessageQueueCommand(id));
            if (HandleResponseError(response, logger, "MessageQueue", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{messageQueuesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int messageQueuesId)
        {
            var response = await mediator.Send(new GetByIdQuery(messageQueuesId));
            if (HandleResponseError(response, logger, "MessageQueue", $"MessageQueuesId: '{messageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-campaign/{messageQueuesCampaignId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByCampaignResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByCampaignAsync(int messageQueuesCampaignId)
        {
            var response = await mediator.Send(new GetByCampaignQuery(messageQueuesCampaignId));
            if (HandleResponseError(response, logger, "MessageQueue", $"MessageQueuesCampaignId: '{messageQueuesCampaignId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-queue/{messageQueuesStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPendingQueueResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingQueueAsync(MessageStatus messageQueuesStatus)
        {
            var response = await mediator.Send(new GetPendingQueueQuery(messageQueuesStatus));
            if (HandleResponseError(response, logger, "MessageQueue", $"MessageQueuesStatus: '{messageQueuesStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-queue-count/{messageQueuesCampaignId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetQueueCountAsync(int messageQueuesCampaignId)
        {
            var response = await mediator.Send(new GetQueueCountQuery(messageQueuesCampaignId));
            if (HandleResponseError(response, logger, "MessageQueue", $"MessageQueuesCampaignId: '{messageQueuesCampaignId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-queue-details/{messageQueuesStatus}/{campaignsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetQueueDetailsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetQueueDetailsAsync(MessageStatus messageQueuesStatus, bool campaignsIsActive)
        {
            var response = await mediator.Send(new GetQueueDetailsQuery(messageQueuesStatus, campaignsIsActive));
            if (HandleResponseError(response, logger, "MessageQueue", $"MessageQueuesStatus: '{messageQueuesStatus}', CampaignsIsActive: '{campaignsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{messageQueuesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int messageQueuesId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdateStatusCommand(messageQueuesId, updateRequest));
            if (HandleResponseError(response, logger, "MessageQueue", $"MessageQueuesId: '{messageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-priority/{messageQueuesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePriorityAsync(int messageQueuesId, [FromBody] UpdatePriorityRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdatePriorityCommand(messageQueuesId, updateRequest));
            if (HandleResponseError(response, logger, "MessageQueue", $"MessageQueuesId: '{messageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}