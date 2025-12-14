using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageLog;
using QuickCode.Demo.SmsManagerModule.Application.Features.MessageLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Api.Controllers
{
    public partial class MessageLogsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<MessageLogsController> logger;
        private readonly IServiceProvider serviceProvider;
        public MessageLogsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<MessageLogsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MessageLogDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListMessageLogQuery(page, size));
            if (HandleResponseError(response, logger, "MessageLog", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountMessageLogQuery());
            if (HandleResponseError(response, logger, "MessageLog") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageLogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemMessageLogQuery(id));
            if (HandleResponseError(response, logger, "MessageLog", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageLogDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(MessageLogDto model)
        {
            var response = await mediator.Send(new InsertMessageLogCommand(model));
            if (HandleResponseError(response, logger, "MessageLog") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, MessageLogDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateMessageLogCommand(id, model));
            if (HandleResponseError(response, logger, "MessageLog", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemMessageLogCommand(id));
            if (HandleResponseError(response, logger, "MessageLog", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{messageLogsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int messageLogsId)
        {
            var response = await mediator.Send(new GetByIdQuery(messageLogsId));
            if (HandleResponseError(response, logger, "MessageLog", $"MessageLogsId: '{messageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-message/{messageLogsMessageId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByMessageResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByMessageAsync(int messageLogsMessageId)
        {
            var response = await mediator.Send(new GetByMessageQuery(messageLogsMessageId));
            if (HandleResponseError(response, logger, "MessageLog", $"MessageLogsMessageId: '{messageLogsMessageId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-campaign/{messageLogsCampaignId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByCampaignResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByCampaignAsync(int messageLogsCampaignId)
        {
            var response = await mediator.Send(new GetByCampaignQuery(messageLogsCampaignId));
            if (HandleResponseError(response, logger, "MessageLog", $"MessageLogsCampaignId: '{messageLogsCampaignId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-sender/{messageLogsSenderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetBySenderResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBySenderAsync(int messageLogsSenderId)
        {
            var response = await mediator.Send(new GetBySenderQuery(messageLogsSenderId));
            if (HandleResponseError(response, logger, "MessageLog", $"MessageLogsSenderId: '{messageLogsSenderId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-logs-count/{messageLogsCampaignId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetLogsCountAsync(int messageLogsCampaignId)
        {
            var response = await mediator.Send(new GetLogsCountQuery(messageLogsCampaignId));
            if (HandleResponseError(response, logger, "MessageLog", $"MessageLogsCampaignId: '{messageLogsCampaignId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-logs-with-sender/{sendersIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetLogsWithSenderResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetLogsWithSenderAsync(bool sendersIsActive)
        {
            var response = await mediator.Send(new GetLogsWithSenderQuery(sendersIsActive));
            if (HandleResponseError(response, logger, "MessageLog", $"SendersIsActive: '{sendersIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}