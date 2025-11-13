using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.EmailSender;
using QuickCode.Demo.EmailManagerModule.Application.Services.EmailSender;

namespace QuickCode.Demo.EmailManagerModule.Api.Controllers
{
    public partial class EmailSendersController : QuickCodeBaseApiController
    {
        private readonly IEmailSenderService service;
        private readonly ILogger<EmailSendersController> logger;
        private readonly IServiceProvider serviceProvider;
        public EmailSendersController(IEmailSenderService service, IServiceProvider serviceProvider, ILogger<EmailSendersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmailSenderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "EmailSender", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "EmailSender") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmailSenderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "EmailSender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmailSenderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(EmailSenderDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "EmailSender") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, EmailSenderDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "EmailSender", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "EmailSender", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{emailSenderId}/info-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetInfoMessagesForEmailSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetInfoMessagesForEmailSendersAsync(int emailSendersId)
        {
            var response = await service.GetInfoMessagesForEmailSendersAsync(emailSendersId);
            if (HandleResponseError(response, logger, "EmailSender", $"EmailSendersId: '{emailSendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{emailSenderId}/info-message/{infoMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetInfoMessagesForEmailSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetInfoMessagesForEmailSendersDetailsAsync(int emailSendersId, int infoMessagesId)
        {
            var response = await service.GetInfoMessagesForEmailSendersDetailsAsync(emailSendersId, infoMessagesId);
            if (HandleResponseError(response, logger, "EmailSender", $"EmailSendersId: '{emailSendersId}', InfoMessagesId: '{infoMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{emailSenderId}/otp-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessagesForEmailSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForEmailSendersAsync(int emailSendersId)
        {
            var response = await service.GetOtpMessagesForEmailSendersAsync(emailSendersId);
            if (HandleResponseError(response, logger, "EmailSender", $"EmailSendersId: '{emailSendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{emailSenderId}/otp-message/{otpMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessagesForEmailSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForEmailSendersDetailsAsync(int emailSendersId, int otpMessagesId)
        {
            var response = await service.GetOtpMessagesForEmailSendersDetailsAsync(emailSendersId, otpMessagesId);
            if (HandleResponseError(response, logger, "EmailSender", $"EmailSendersId: '{emailSendersId}', OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{emailSenderId}/campaign-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCampaignMessagesForEmailSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignMessagesForEmailSendersAsync(int emailSendersId)
        {
            var response = await service.GetCampaignMessagesForEmailSendersAsync(emailSendersId);
            if (HandleResponseError(response, logger, "EmailSender", $"EmailSendersId: '{emailSendersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{emailSenderId}/campaign-message/{campaignMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCampaignMessagesForEmailSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignMessagesForEmailSendersDetailsAsync(int emailSendersId, int campaignMessagesId)
        {
            var response = await service.GetCampaignMessagesForEmailSendersDetailsAsync(emailSendersId, campaignMessagesId);
            if (HandleResponseError(response, logger, "EmailSender", $"EmailSendersId: '{emailSendersId}', CampaignMessagesId: '{campaignMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}