using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.SmsManagerModule.Application.Features.Sender;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class SendersController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<SendersController> logger;
        private readonly IServiceProvider serviceProvider;
        public SendersController(IMediator mediator, IServiceProvider serviceProvider, ILogger<SendersController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SenderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListSenderQuery(page, size));
            if (HandleResponseError(response, logger, "Sender", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountSenderQuery());
            if (HandleResponseError(response, logger, "Sender") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SenderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemSenderQuery(id));
            if (HandleResponseError(response, logger, "Sender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SenderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SenderDto model)
        {
            var response = await mediator.Send(new InsertSenderCommand(model));
            if (HandleResponseError(response, logger, "Sender") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SenderDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateSenderCommand(id, model));
            if (HandleResponseError(response, logger, "Sender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemSenderCommand(id));
            if (HandleResponseError(response, logger, "Sender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{sendersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int sendersId)
        {
            var response = await mediator.Send(new GetByIdQuery(sendersId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-senders/{sendersIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveSendersAsync(bool sendersIsActive)
        {
            var response = await mediator.Send(new GetActiveSendersQuery(sendersIsActive));
            if (HandleResponseError(response, logger, "Sender", $"SendersIsActive: '{sendersIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-name/{sendersName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByNameResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByNameAsync(string sendersName)
        {
            var response = await mediator.Send(new GetByNameQuery(sendersName));
            if (HandleResponseError(response, logger, "Sender", $"SendersName: '{sendersName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-phone/{sendersPhoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByPhoneAsync(string sendersPhoneNumber)
        {
            var response = await mediator.Send(new ExistsByPhoneQuery(sendersPhoneNumber));
            if (HandleResponseError(response, logger, "Sender", $"SendersPhoneNumber: '{sendersPhoneNumber}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/message-log")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessageLogsForSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageLogsForSendersAsync(int sendersId)
        {
            var response = await mediator.Send(new GetMessageLogsForSendersQuery(sendersId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/message-log/{messageLogId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessageLogsForSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageLogsForSendersDetailsAsync(int sendersId, int messageLogsId)
        {
            var response = await mediator.Send(new GetMessageLogsForSendersDetailsQuery(sendersId, messageLogsId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}', MessageLogsId: '{messageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/otp-message-log")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessageLogsForSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageLogsForSendersAsync(int sendersId)
        {
            var response = await mediator.Send(new GetOtpMessageLogsForSendersQuery(sendersId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/otp-message-log/{otpMessageLogId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessageLogsForSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageLogsForSendersDetailsAsync(int sendersId, int otpMessageLogsId)
        {
            var response = await mediator.Send(new GetOtpMessageLogsForSendersDetailsQuery(sendersId, otpMessageLogsId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}', OtpMessageLogsId: '{otpMessageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/message-queue")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessageQueuesForSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageQueuesForSendersAsync(int sendersId)
        {
            var response = await mediator.Send(new GetMessageQueuesForSendersQuery(sendersId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/message-queue/{messageQueueId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessageQueuesForSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageQueuesForSendersDetailsAsync(int sendersId, int messageQueuesId)
        {
            var response = await mediator.Send(new GetMessageQueuesForSendersDetailsQuery(sendersId, messageQueuesId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}', MessageQueuesId: '{messageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/otp-message-queue")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessageQueuesForSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageQueuesForSendersAsync(int sendersId)
        {
            var response = await mediator.Send(new GetOtpMessageQueuesForSendersQuery(sendersId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/otp-message-queue/{otpMessageQueueId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessageQueuesForSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageQueuesForSendersDetailsAsync(int sendersId, int otpMessageQueuesId)
        {
            var response = await mediator.Send(new GetOtpMessageQueuesForSendersDetailsQuery(sendersId, otpMessageQueuesId));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}', OtpMessageQueuesId: '{otpMessageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{sendersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int sendersId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdateStatusCommand(sendersId, updateRequest));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-priority/{sendersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePriorityAsync(int sendersId, [FromBody] UpdatePriorityRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdatePriorityCommand(sendersId, updateRequest));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-daily-limit/{sendersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateDailyLimitAsync(int sendersId, [FromBody] UpdateDailyLimitRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdateDailyLimitCommand(sendersId, updateRequest));
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}