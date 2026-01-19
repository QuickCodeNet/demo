using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.EmailManagerModule.Application.Services.Sender;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Api.Controllers
{
    public partial class SendersController : QuickCodeBaseApiController
    {
        private readonly ISenderService service;
        private readonly ILogger<SendersController> logger;
        private readonly IServiceProvider serviceProvider;
        public SendersController(ISenderService service, IServiceProvider serviceProvider, ILogger<SendersController> logger)
        {
            this.service = service;
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
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Sender", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
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
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Sender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SenderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SenderDto model)
        {
            var response = await service.InsertAsync(model);
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

            var response = await service.UpdateAsync(id, model);
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
            var response = await service.DeleteItemAsync(id);
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
            var response = await service.GetByIdAsync(sendersId);
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
            var response = await service.GetActiveSendersAsync(sendersIsActive);
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
            var response = await service.GetByNameAsync(sendersName);
            if (HandleResponseError(response, logger, "Sender", $"SendersName: '{sendersName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-email/{sendersEmailAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByEmailAsync(string sendersEmailAddress)
        {
            var response = await service.ExistsByEmailAsync(sendersEmailAddress);
            if (HandleResponseError(response, logger, "Sender", $"SendersEmailAddress: '{sendersEmailAddress}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{senderId}/message-log")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessageLogsForSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageLogsForSendersAsync(int sendersId)
        {
            var response = await service.GetMessageLogsForSendersAsync(sendersId);
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
            var response = await service.GetMessageLogsForSendersDetailsAsync(sendersId, messageLogsId);
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
            var response = await service.GetOtpMessageLogsForSendersAsync(sendersId);
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
            var response = await service.GetOtpMessageLogsForSendersDetailsAsync(sendersId, otpMessageLogsId);
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
            var response = await service.GetMessageQueuesForSendersAsync(sendersId);
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
            var response = await service.GetMessageQueuesForSendersDetailsAsync(sendersId, messageQueuesId);
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
            var response = await service.GetOtpMessageQueuesForSendersAsync(sendersId);
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
            var response = await service.GetOtpMessageQueuesForSendersDetailsAsync(sendersId, otpMessageQueuesId);
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}', OtpMessageQueuesId: '{otpMessageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{sendersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int sendersId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await service.UpdateStatusAsync(sendersId, updateRequest);
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-priority/{sendersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePriorityAsync(int sendersId, [FromBody] UpdatePriorityRequestDto updateRequest)
        {
            var response = await service.UpdatePriorityAsync(sendersId, updateRequest);
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-daily-limit/{sendersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateDailyLimitAsync(int sendersId, [FromBody] UpdateDailyLimitRequestDto updateRequest)
        {
            var response = await service.UpdateDailyLimitAsync(sendersId, updateRequest);
            if (HandleResponseError(response, logger, "Sender", $"SendersId: '{sendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}