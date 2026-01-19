using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Application.Features.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class MessageTemplatesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<MessageTemplatesController> logger;
        private readonly IServiceProvider serviceProvider;
        public MessageTemplatesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<MessageTemplatesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MessageTemplateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListMessageTemplateQuery(page, size));
            if (HandleResponseError(response, logger, "MessageTemplate", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountMessageTemplateQuery());
            if (HandleResponseError(response, logger, "MessageTemplate") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageTemplateDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string name)
        {
            var response = await mediator.Send(new GetItemMessageTemplateQuery(name));
            if (HandleResponseError(response, logger, "MessageTemplate", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageTemplateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(MessageTemplateDto model)
        {
            var response = await mediator.Send(new InsertMessageTemplateCommand(model));
            if (HandleResponseError(response, logger, "MessageTemplate") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { name = response.Value.Name }, response.Value);
        }

        [HttpPut("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string name, MessageTemplateDto model)
        {
            if (!(model.Name == name))
            {
                return BadRequest($"Name: '{name}' must be equal to model.Name: '{model.Name}'");
            }

            var response = await mediator.Send(new UpdateMessageTemplateCommand(name, model));
            if (HandleResponseError(response, logger, "MessageTemplate", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var response = await mediator.Send(new DeleteItemMessageTemplateCommand(name));
            if (HandleResponseError(response, logger, "MessageTemplate", $"Name: '{name}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-name/{messageTemplatesName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByNameResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByNameAsync(string messageTemplatesName)
        {
            var response = await mediator.Send(new GetByNameQuery(messageTemplatesName));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-type/{messageTemplatesType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByTypeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByTypeAsync(TemplateTypes messageTemplatesType)
        {
            var response = await mediator.Send(new GetByTypeQuery(messageTemplatesType));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesType: '{messageTemplatesType}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-name/{messageTemplatesName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByNameAsync(string messageTemplatesName)
        {
            var response = await mediator.Send(new ExistsByNameQuery(messageTemplatesName));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/campaign")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCampaignsForMessageTemplatesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignsForMessageTemplatesAsync(string messageTemplatesName)
        {
            var response = await mediator.Send(new GetCampaignsForMessageTemplatesQuery(messageTemplatesName));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/campaign/{campaignId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCampaignsForMessageTemplatesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignsForMessageTemplatesDetailsAsync(string messageTemplatesName, int campaignsId)
        {
            var response = await mediator.Send(new GetCampaignsForMessageTemplatesDetailsQuery(messageTemplatesName, campaignsId));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}', CampaignsId: '{campaignsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessagesForMessageTemplatesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessagesForMessageTemplatesAsync(string messageTemplatesName)
        {
            var response = await mediator.Send(new GetMessagesForMessageTemplatesQuery(messageTemplatesName));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/message/{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessagesForMessageTemplatesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessagesForMessageTemplatesDetailsAsync(string messageTemplatesName, int messagesId)
        {
            var response = await mediator.Send(new GetMessagesForMessageTemplatesDetailsQuery(messageTemplatesName, messagesId));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}', MessagesId: '{messagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/otp-message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessagesForMessageTemplatesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForMessageTemplatesAsync(string messageTemplatesName)
        {
            var response = await mediator.Send(new GetOtpMessagesForMessageTemplatesQuery(messageTemplatesName));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/otp-message/{otpMessageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessagesForMessageTemplatesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessagesForMessageTemplatesDetailsAsync(string messageTemplatesName, int otpMessagesId)
        {
            var response = await mediator.Send(new GetOtpMessagesForMessageTemplatesDetailsQuery(messageTemplatesName, otpMessagesId));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}', OtpMessagesId: '{otpMessagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/otp-message-log")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOtpMessageLogsForMessageTemplatesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageLogsForMessageTemplatesAsync(string messageTemplatesName)
        {
            var response = await mediator.Send(new GetOtpMessageLogsForMessageTemplatesQuery(messageTemplatesName));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/otp-message-log/{otpMessageLogId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOtpMessageLogsForMessageTemplatesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOtpMessageLogsForMessageTemplatesDetailsAsync(string messageTemplatesName, int otpMessageLogsId)
        {
            var response = await mediator.Send(new GetOtpMessageLogsForMessageTemplatesDetailsQuery(messageTemplatesName, otpMessageLogsId));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}', OtpMessageLogsId: '{otpMessageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/message-log")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessageLogsForMessageTemplatesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageLogsForMessageTemplatesAsync(string messageTemplatesName)
        {
            var response = await mediator.Send(new GetMessageLogsForMessageTemplatesQuery(messageTemplatesName));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageTemplateName}/message-log/{messageLogId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessageLogsForMessageTemplatesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageLogsForMessageTemplatesDetailsAsync(string messageTemplatesName, int messageLogsId)
        {
            var response = await mediator.Send(new GetMessageLogsForMessageTemplatesDetailsQuery(messageTemplatesName, messageLogsId));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}', MessageLogsId: '{messageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-content/{messageTemplatesName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateContentAsync(string messageTemplatesName, [FromBody] UpdateContentRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdateContentCommand(messageTemplatesName, updateRequest));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-type/{messageTemplatesName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateTypeAsync(string messageTemplatesName, [FromBody] UpdateTypeRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdateTypeCommand(messageTemplatesName, updateRequest));
            if (HandleResponseError(response, logger, "MessageTemplate", $"MessageTemplatesName: '{messageTemplatesName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}