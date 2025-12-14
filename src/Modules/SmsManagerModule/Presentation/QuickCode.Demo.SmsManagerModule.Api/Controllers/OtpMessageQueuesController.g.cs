using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class OtpMessageQueuesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<OtpMessageQueuesController> logger;
        private readonly IServiceProvider serviceProvider;
        public OtpMessageQueuesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<OtpMessageQueuesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OtpMessageQueueDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListOtpMessageQueueQuery(page, size));
            if (HandleResponseError(response, logger, "OtpMessageQueue", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountOtpMessageQueueQuery());
            if (HandleResponseError(response, logger, "OtpMessageQueue") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OtpMessageQueueDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemOtpMessageQueueQuery(id));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OtpMessageQueueDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(OtpMessageQueueDto model)
        {
            var response = await mediator.Send(new InsertOtpMessageQueueCommand(model));
            if (HandleResponseError(response, logger, "OtpMessageQueue") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, OtpMessageQueueDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateOtpMessageQueueCommand(id, model));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemOtpMessageQueueCommand(id));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{otpMessageQueuesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int otpMessageQueuesId)
        {
            var response = await mediator.Send(new GetByIdQuery(otpMessageQueuesId));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"OtpMessageQueuesId: '{otpMessageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-otp-message/{otpMessageQueuesOtpMessageId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByOtpMessageResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByOtpMessageAsync(int otpMessageQueuesOtpMessageId)
        {
            var response = await mediator.Send(new GetByOtpMessageQuery(otpMessageQueuesOtpMessageId));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"OtpMessageQueuesOtpMessageId: '{otpMessageQueuesOtpMessageId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-queue/{otpMessageQueuesStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPendingQueueResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingQueueAsync(MessageStatus otpMessageQueuesStatus)
        {
            var response = await mediator.Send(new GetPendingQueueQuery(otpMessageQueuesStatus));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"OtpMessageQueuesStatus: '{otpMessageQueuesStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{otpMessageQueuesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int otpMessageQueuesId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdateStatusCommand(otpMessageQueuesId, updateRequest));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"OtpMessageQueuesId: '{otpMessageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-priority/{otpMessageQueuesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePriorityAsync(int otpMessageQueuesId, [FromBody] UpdatePriorityRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdatePriorityCommand(otpMessageQueuesId, updateRequest));
            if (HandleResponseError(response, logger, "OtpMessageQueue", $"OtpMessageQueuesId: '{otpMessageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}