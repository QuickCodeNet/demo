using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.Campaign;
using QuickCode.Demo.EmailManagerModule.Application.Services.Campaign;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Api.Controllers
{
    public partial class CampaignsController : QuickCodeBaseApiController
    {
        private readonly ICampaignService service;
        private readonly ILogger<CampaignsController> logger;
        private readonly IServiceProvider serviceProvider;
        public CampaignsController(ICampaignService service, IServiceProvider serviceProvider, ILogger<CampaignsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CampaignDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Campaign", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Campaign") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CampaignDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Campaign", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CampaignDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(CampaignDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Campaign") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, CampaignDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Campaign", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Campaign", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{campaignsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int campaignsId)
        {
            var response = await service.GetByIdAsync(campaignsId);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsId: '{campaignsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-campaigns/{campaignsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveCampaignsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveCampaignsAsync(bool campaignsIsActive)
        {
            var response = await service.GetActiveCampaignsAsync(campaignsIsActive);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsIsActive: '{campaignsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-name/{campaignsName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByNameResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByNameAsync(string campaignsName)
        {
            var response = await service.GetByNameAsync(campaignsName);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsName: '{campaignsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-name/{campaignsName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByNameAsync(string campaignsName)
        {
            var response = await service.ExistsByNameAsync(campaignsName);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsName: '{campaignsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-campaigns-count/{campaignsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCampaignsCountAsync(bool campaignsIsActive)
        {
            var response = await service.GetCampaignsCountAsync(campaignsIsActive);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsIsActive: '{campaignsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{campaignId}/message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessagesForCampaignsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessagesForCampaignsAsync(int campaignsId)
        {
            var response = await service.GetMessagesForCampaignsAsync(campaignsId);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsId: '{campaignsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{campaignId}/message/{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessagesForCampaignsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessagesForCampaignsDetailsAsync(int campaignsId, int messagesId)
        {
            var response = await service.GetMessagesForCampaignsDetailsAsync(campaignsId, messagesId);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsId: '{campaignsId}', MessagesId: '{messagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{campaignId}/message-queue")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetMessageQueuesForCampaignsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageQueuesForCampaignsAsync(int campaignsId)
        {
            var response = await service.GetMessageQueuesForCampaignsAsync(campaignsId);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsId: '{campaignsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{campaignId}/message-queue/{messageQueueId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMessageQueuesForCampaignsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetMessageQueuesForCampaignsDetailsAsync(int campaignsId, int messageQueuesId)
        {
            var response = await service.GetMessageQueuesForCampaignsDetailsAsync(campaignsId, messageQueuesId);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsId: '{campaignsId}', MessageQueuesId: '{messageQueuesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-status/{campaignsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateStatusAsync(int campaignsId, [FromBody] UpdateStatusRequestDto updateRequest)
        {
            var response = await service.UpdateStatusAsync(campaignsId, updateRequest);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsId: '{campaignsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-priority/{campaignsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePriorityAsync(int campaignsId, [FromBody] UpdatePriorityRequestDto updateRequest)
        {
            var response = await service.UpdatePriorityAsync(campaignsId, updateRequest);
            if (HandleResponseError(response, logger, "Campaign", $"CampaignsId: '{campaignsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}