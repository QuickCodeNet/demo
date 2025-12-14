using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.BlackList;
using QuickCode.Demo.EmailManagerModule.Application.Services.BlackList;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Api.Controllers
{
    public partial class BlackListsController : QuickCodeBaseApiController
    {
        private readonly IBlackListService service;
        private readonly ILogger<BlackListsController> logger;
        private readonly IServiceProvider serviceProvider;
        public BlackListsController(IBlackListService service, IServiceProvider serviceProvider, ILogger<BlackListsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BlackListDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "BlackList", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "BlackList") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlackListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "BlackList", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BlackListDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(BlackListDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "BlackList") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, BlackListDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "BlackList", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "BlackList", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-id/{blackListsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(int blackListsId)
        {
            var response = await service.GetByIdAsync(blackListsId);
            if (HandleResponseError(response, logger, "BlackList", $"BlackListsId: '{blackListsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-recipient/{blackListsRecipient}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByRecipientResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByRecipientAsync(string blackListsRecipient)
        {
            var response = await service.GetByRecipientAsync(blackListsRecipient);
            if (HandleResponseError(response, logger, "BlackList", $"BlackListsRecipient: '{blackListsRecipient}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-recipient/{blackListsRecipient}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByRecipientAsync(string blackListsRecipient)
        {
            var response = await service.ExistsByRecipientAsync(blackListsRecipient);
            if (HandleResponseError(response, logger, "BlackList", $"BlackListsRecipient: '{blackListsRecipient}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-blacklist-count/{blackListsReasonType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBlacklistCountAsync(BlacklistReasonType blackListsReasonType)
        {
            var response = await service.GetBlacklistCountAsync(blackListsReasonType);
            if (HandleResponseError(response, logger, "BlackList", $"BlackListsReasonType: '{blackListsReasonType}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("add-to-blacklist/{blackListsRecipient}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> AddToBlacklistAsync(string blackListsRecipient, [FromBody] AddToBlacklistRequestDto updateRequest)
        {
            var response = await service.AddToBlacklistAsync(blackListsRecipient, updateRequest);
            if (HandleResponseError(response, logger, "BlackList", $"BlackListsRecipient: '{blackListsRecipient}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("remove-from-blacklist/{blackListsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RemoveFromBlacklistAsync(int blackListsId)
        {
            var response = await service.RemoveFromBlacklistAsync(blackListsId);
            if (HandleResponseError(response, logger, "BlackList", $"BlackListsId: '{blackListsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}