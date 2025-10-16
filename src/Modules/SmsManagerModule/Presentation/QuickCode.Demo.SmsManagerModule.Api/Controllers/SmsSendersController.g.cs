using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.SmsSender;
using QuickCode.Demo.SmsManagerModule.Application.Services.SmsSender;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class SmsSendersController : QuickCodeBaseApiController
    {
        private readonly ISmsSenderService service;
        private readonly ILogger<SmsSendersController> logger;
        private readonly IServiceProvider serviceProvider;
        public SmsSendersController(ISmsSenderService service, IServiceProvider serviceProvider, ILogger<SmsSendersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SmsSenderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "SmsSender", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "SmsSender") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SmsSenderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "SmsSender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SmsSenderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SmsSenderDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "SmsSender") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SmsSenderDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "SmsSender", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "SmsSender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{smsSenderId}/info-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetInfoMessagesForSmsSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetInfoMessagesForSmsSendersAsync(int smsSendersId)
        {
            var response = await service.GetInfoMessagesForSmsSendersAsync(smsSendersId);
            if (HandleResponseError(response, logger, "SmsSender", $"SmsSendersId: '{smsSendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{smsSenderId}/info-message/{infoMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetInfoMessagesForSmsSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetInfoMessagesForSmsSendersDetailsAsync(int smsSendersId, int infoMessagesId)
        {
            var response = await service.GetInfoMessagesForSmsSendersDetailsAsync(smsSendersId, infoMessagesId);
            if (HandleResponseError(response, logger, "SmsSender", $"SmsSendersId: '{smsSendersId}', InfoMessagesId: '{infoMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{smsSenderId}/otp-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessagesForSmsSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForSmsSendersAsync(int smsSendersId)
        {
            var response = await service.GetOtpMessagesForSmsSendersAsync(smsSendersId);
            if (HandleResponseError(response, logger, "SmsSender", $"SmsSendersId: '{smsSendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{smsSenderId}/otp-message/{otpMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessagesForSmsSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForSmsSendersDetailsAsync(int smsSendersId, int otpMessagesId)
        {
            var response = await service.GetOtpMessagesForSmsSendersDetailsAsync(smsSendersId, otpMessagesId);
            if (HandleResponseError(response, logger, "SmsSender", $"SmsSendersId: '{smsSendersId}', OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{smsSenderId}/campaign-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCampaignMessagesForSmsSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignMessagesForSmsSendersAsync(int smsSendersId)
        {
            var response = await service.GetCampaignMessagesForSmsSendersAsync(smsSendersId);
            if (HandleResponseError(response, logger, "SmsSender", $"SmsSendersId: '{smsSendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{smsSenderId}/campaign-message/{campaignMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCampaignMessagesForSmsSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignMessagesForSmsSendersDetailsAsync(int smsSendersId, int campaignMessagesId)
        {
            var response = await service.GetCampaignMessagesForSmsSendersDetailsAsync(smsSendersId, campaignMessagesId);
            if (HandleResponseError(response, logger, "SmsSender", $"SmsSendersId: '{smsSendersId}', CampaignMessagesId: '{campaignMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}