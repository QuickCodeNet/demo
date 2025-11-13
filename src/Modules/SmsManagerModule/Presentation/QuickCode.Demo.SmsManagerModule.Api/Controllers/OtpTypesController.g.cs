using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpType;
using QuickCode.Demo.SmsManagerModule.Application.Services.OtpType;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class OtpTypesController : QuickCodeBaseApiController
    {
        private readonly IOtpTypeService service;
        private readonly ILogger<OtpTypesController> logger;
        private readonly IServiceProvider serviceProvider;
        public OtpTypesController(IOtpTypeService service, IServiceProvider serviceProvider, ILogger<OtpTypesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OtpTypeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "OtpType", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "OtpType") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OtpTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "OtpType", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OtpTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(OtpTypeDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "OtpType") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, OtpTypeDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "OtpType", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "OtpType", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{otpTypeId}/otp-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessagesForOtpTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForOtpTypesAsync(int otpTypesId)
        {
            var response = await service.GetOtpMessagesForOtpTypesAsync(otpTypesId);
            if (HandleResponseError(response, logger, "OtpType", $"OtpTypesId: '{otpTypesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{otpTypeId}/otp-message/{otpMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessagesForOtpTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForOtpTypesDetailsAsync(int otpTypesId, int otpMessagesId)
        {
            var response = await service.GetOtpMessagesForOtpTypesDetailsAsync(otpTypesId, otpMessagesId);
            if (HandleResponseError(response, logger, "OtpType", $"OtpTypesId: '{otpTypesId}', OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}