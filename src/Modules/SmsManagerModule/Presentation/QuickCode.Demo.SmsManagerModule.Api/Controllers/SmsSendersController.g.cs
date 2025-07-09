using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;
using QuickCode.Demo.SmsManagerModule.Application.Features;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class SmsSendersController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<SmsSendersController> logger;
        private readonly IServiceProvider serviceProvider;
        public SmsSendersController(IMediator mediator, IServiceProvider serviceProvider, ILogger<SmsSendersController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SmsSendersDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new SmsSendersListQuery(page, size));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new SmsSendersTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SmsSendersDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new SmsSendersGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SmsSendersDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SmsSendersDto model)
        {
            var response = await mediator.Send(new SmsSendersInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SmsSendersDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new SmsSendersUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new SmsSendersDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{smsSendersId}/info-messages")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SmsSendersGetInfoMessagesForSmsSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetInfoMessagesForSmsSendersAsync(int smsSendersId)
        {
            var response = await mediator.Send(new SmsSendersGetInfoMessagesForSmsSendersQuery(smsSendersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SmsSendersId: '{smsSendersId}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{smsSendersId}/info-messages/{infoMessagesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SmsSendersGetInfoMessagesForSmsSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetInfoMessagesForSmsSendersDetailsAsync(int smsSendersId, int infoMessagesId)
        {
            var response = await mediator.Send(new SmsSendersGetInfoMessagesForSmsSendersDetailsQuery(smsSendersId, infoMessagesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SmsSendersId: '{smsSendersId}', InfoMessagesId: '{infoMessagesId}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{smsSendersId}/otp-messages")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SmsSendersGetOtpMessagesForSmsSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForSmsSendersAsync(int smsSendersId)
        {
            var response = await mediator.Send(new SmsSendersGetOtpMessagesForSmsSendersQuery(smsSendersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SmsSendersId: '{smsSendersId}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{smsSendersId}/otp-messages/{otpMessagesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SmsSendersGetOtpMessagesForSmsSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForSmsSendersDetailsAsync(int smsSendersId, int otpMessagesId)
        {
            var response = await mediator.Send(new SmsSendersGetOtpMessagesForSmsSendersDetailsQuery(smsSendersId, otpMessagesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SmsSendersId: '{smsSendersId}', OtpMessagesId: '{otpMessagesId}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{smsSendersId}/campaign-messages")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SmsSendersGetCampaignMessagesForSmsSendersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignMessagesForSmsSendersAsync(int smsSendersId)
        {
            var response = await mediator.Send(new SmsSendersGetCampaignMessagesForSmsSendersQuery(smsSendersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SmsSendersId: '{smsSendersId}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{smsSendersId}/campaign-messages/{campaignMessagesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SmsSendersGetCampaignMessagesForSmsSendersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignMessagesForSmsSendersDetailsAsync(int smsSendersId, int campaignMessagesId)
        {
            var response = await mediator.Send(new SmsSendersGetCampaignMessagesForSmsSendersDetailsQuery(smsSendersId, campaignMessagesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"SmsSendersId: '{smsSendersId}', CampaignMessagesId: '{campaignMessagesId}' not found in SmsSenders Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }
    }
}