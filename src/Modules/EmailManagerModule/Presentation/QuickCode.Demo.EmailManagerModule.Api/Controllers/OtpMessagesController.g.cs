using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.EmailManagerModule.Application.Services.OtpMessage;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Api.Controllers
{
    public partial class OtpMessagesController : QuickCodeBaseApiController
    {
        private readonly IOtpMessageService service;
        private readonly ILogger<OtpMessagesController> logger;
        private readonly IServiceProvider serviceProvider;
        public OtpMessagesController(IOtpMessageService service, IServiceProvider serviceProvider, ILogger<OtpMessagesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OtpMessageDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "OtpMessage", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "OtpMessage") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OtpMessageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "OtpMessage", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OtpMessageDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(OtpMessageDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "OtpMessage") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, OtpMessageDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "OtpMessage", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "OtpMessage", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{otpMessagesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int otpMessagesId)
        {
            var response = await service.GetByIdAsync(otpMessagesId);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-recipient/{otpMessagesRecipient}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByRecipientResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByRecipientAsync(string otpMessagesRecipient)
        {
            var response = await service.GetByRecipientAsync(otpMessagesRecipient);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesRecipient: '{otpMessagesRecipient}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-hash/{otpMessagesHashCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByHashAsync(string otpMessagesHashCode)
        {
            var response = await service.ExistsByHashAsync(otpMessagesHashCode);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesHashCode: '{otpMessagesHashCode}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{otpMessageId}/otp-message-queue")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessageQueuesForOtpMessagesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageQueuesForOtpMessagesAsync(int otpMessagesId)
        {
            var response = await service.GetOtpMessageQueuesForOtpMessagesAsync(otpMessagesId);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{otpMessageId}/otp-message-queue/{otpMessageQueueId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessageQueuesForOtpMessagesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageQueuesForOtpMessagesDetailsAsync(int otpMessagesId, int otpMessageQueuesId)
        {
            var response = await service.GetOtpMessageQueuesForOtpMessagesDetailsAsync(otpMessagesId, otpMessageQueuesId);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesId: '{otpMessagesId}', OtpMessageQueuesId: '{otpMessageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{otpMessageId}/otp-message-log")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessageLogsForOtpMessagesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageLogsForOtpMessagesAsync(int otpMessagesId)
        {
            var response = await service.GetOtpMessageLogsForOtpMessagesAsync(otpMessagesId);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{otpMessageId}/otp-message-log/{otpMessageLogId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessageLogsForOtpMessagesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageLogsForOtpMessagesDetailsAsync(int otpMessagesId, int otpMessageLogsId)
        {
            var response = await service.GetOtpMessageLogsForOtpMessagesDetailsAsync(otpMessagesId, otpMessageLogsId);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesId: '{otpMessagesId}', OtpMessageLogsId: '{otpMessageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{otpMessagesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int otpMessagesId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await service.UpdateStatusAsync(otpMessagesId, updateRequest);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("increment-attempt/{otpMessagesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> IncrementAttemptAsync(int otpMessagesId, [FromBody] IncrementAttemptRequestDto updateRequest)
        {
            var response = await service.IncrementAttemptAsync(otpMessagesId, updateRequest);
            if (HandleResponseError(response, logger, "OtpMessage", $"OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}