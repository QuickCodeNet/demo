using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.Message;
using QuickCode.Demo.EmailManagerModule.Application.Services.Message;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Api.Controllers
{
    public partial class MessagesController : QuickCodeBaseApiController
    {
        private readonly IMessageService service;
        private readonly ILogger<MessagesController> logger;
        private readonly IServiceProvider serviceProvider;
        public MessagesController(IMessageService service, IServiceProvider serviceProvider, ILogger<MessagesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MessageDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Message", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Message") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Message", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(MessageDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Message") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, MessageDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Message", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Message", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{messagesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int messagesId)
        {
            var response = await service.GetByIdAsync(messagesId);
            if (HandleResponseError(response, logger, "Message", $"MessagesId: '{messagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-campaign/{messagesCampaignId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByCampaignResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByCampaignAsync(int messagesCampaignId)
        {
            var response = await service.GetByCampaignAsync(messagesCampaignId);
            if (HandleResponseError(response, logger, "Message", $"MessagesCampaignId: '{messagesCampaignId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-status/{messagesStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByStatusResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByStatusAsync(MessageStatus messagesStatus)
        {
            var response = await service.GetByStatusAsync(messagesStatus);
            if (HandleResponseError(response, logger, "Message", $"MessagesStatus: '{messagesStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-messages/{messagesStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPendingMessagesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingMessagesAsync(MessageStatus messagesStatus)
        {
            var response = await service.GetPendingMessagesAsync(messagesStatus);
            if (HandleResponseError(response, logger, "Message", $"MessagesStatus: '{messagesStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-messages-count/{messagesCampaignId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessagesCountAsync(int messagesCampaignId)
        {
            var response = await service.GetMessagesCountAsync(messagesCampaignId);
            if (HandleResponseError(response, logger, "Message", $"MessagesCampaignId: '{messagesCampaignId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-messages-with-campaign/{campaignsIsActive:bool}/{messagesStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessagesWithCampaignResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessagesWithCampaignAsync(bool campaignsIsActive, MessageStatus messagesStatus)
        {
            var response = await service.GetMessagesWithCampaignAsync(campaignsIsActive, messagesStatus);
            if (HandleResponseError(response, logger, "Message", $"CampaignsIsActive: '{campaignsIsActive}', MessagesStatus: '{messagesStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageId}/message-queue")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessageQueuesForMessagesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageQueuesForMessagesAsync(int messagesId)
        {
            var response = await service.GetMessageQueuesForMessagesAsync(messagesId);
            if (HandleResponseError(response, logger, "Message", $"MessagesId: '{messagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageId}/message-queue/{messageQueueId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessageQueuesForMessagesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageQueuesForMessagesDetailsAsync(int messagesId, int messageQueuesId)
        {
            var response = await service.GetMessageQueuesForMessagesDetailsAsync(messagesId, messageQueuesId);
            if (HandleResponseError(response, logger, "Message", $"MessagesId: '{messagesId}', MessageQueuesId: '{messageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageId}/message-log")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessageLogsForMessagesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageLogsForMessagesAsync(int messagesId)
        {
            var response = await service.GetMessageLogsForMessagesAsync(messagesId);
            if (HandleResponseError(response, logger, "Message", $"MessagesId: '{messagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{messageId}/message-log/{messageLogId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessageLogsForMessagesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageLogsForMessagesDetailsAsync(int messagesId, int messageLogsId)
        {
            var response = await service.GetMessageLogsForMessagesDetailsAsync(messagesId, messageLogsId);
            if (HandleResponseError(response, logger, "Message", $"MessagesId: '{messagesId}', MessageLogsId: '{messageLogsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{messagesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int messagesId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await service.UpdateStatusAsync(messagesId, updateRequest);
            if (HandleResponseError(response, logger, "Message", $"MessagesId: '{messagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("increment-attempt/{messagesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> IncrementAttemptAsync(int messagesId, [FromBody] IncrementAttemptRequestDto updateRequest)
        {
            var response = await service.IncrementAttemptAsync(messagesId, updateRequest);
            if (HandleResponseError(response, logger, "Message", $"MessagesId: '{messagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}