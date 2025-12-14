using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.OtpMessageLog;
using QuickCode.Demo.EmailManagerModule.Application.Services.OtpMessageLog;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Api.Controllers
{
    public partial class OtpMessageLogsController : QuickCodeBaseApiController
    {
        private readonly IOtpMessageLogService service;
        private readonly ILogger<OtpMessageLogsController> logger;
        private readonly IServiceProvider serviceProvider;
        public OtpMessageLogsController(IOtpMessageLogService service, IServiceProvider serviceProvider, ILogger<OtpMessageLogsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OtpMessageLogDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "OtpMessageLog", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "OtpMessageLog") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OtpMessageLogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OtpMessageLogDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(OtpMessageLogDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "OtpMessageLog") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, OtpMessageLogDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "OtpMessageLog", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{otpMessageLogsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int otpMessageLogsId)
        {
            var response = await service.GetByIdAsync(otpMessageLogsId);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"OtpMessageLogsId: '{otpMessageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-otp-message/{otpMessageLogsOtpMessageId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByOtpMessageResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByOtpMessageAsync(int otpMessageLogsOtpMessageId)
        {
            var response = await service.GetByOtpMessageAsync(otpMessageLogsOtpMessageId);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"OtpMessageLogsOtpMessageId: '{otpMessageLogsOtpMessageId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-sender/{otpMessageLogsSenderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetBySenderResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBySenderAsync(int otpMessageLogsSenderId)
        {
            var response = await service.GetBySenderAsync(otpMessageLogsSenderId);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"OtpMessageLogsSenderId: '{otpMessageLogsSenderId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-status/{otpMessageLogsStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByStatusResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByStatusAsync(MessageStatus otpMessageLogsStatus)
        {
            var response = await service.GetByStatusAsync(otpMessageLogsStatus);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"OtpMessageLogsStatus: '{otpMessageLogsStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-otp-logs-with-message/{otpMessageLogsStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpLogsWithMessageResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpLogsWithMessageAsync(MessageStatus otpMessageLogsStatus)
        {
            var response = await service.GetOtpLogsWithMessageAsync(otpMessageLogsStatus);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"OtpMessageLogsStatus: '{otpMessageLogsStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{otpMessageLogsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int otpMessageLogsId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await service.UpdateStatusAsync(otpMessageLogsId, updateRequest);
            if (HandleResponseError(response, logger, "OtpMessageLog", $"OtpMessageLogsId: '{otpMessageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}