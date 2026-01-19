using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.RefreshToken;
using QuickCode.Demo.UserManagerModule.Application.Features.RefreshToken;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class RefreshTokensController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<RefreshTokensController> logger;
        private readonly IServiceProvider serviceProvider;
        public RefreshTokensController(IMediator mediator, IServiceProvider serviceProvider, ILogger<RefreshTokensController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RefreshTokenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListRefreshTokenQuery(page, size));
            if (HandleResponseError(response, logger, "RefreshToken", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountRefreshTokenQuery());
            if (HandleResponseError(response, logger, "RefreshToken") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemRefreshTokenQuery(id));
            if (HandleResponseError(response, logger, "RefreshToken", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RefreshTokenDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(RefreshTokenDto model)
        {
            var response = await mediator.Send(new InsertRefreshTokenCommand(model));
            if (HandleResponseError(response, logger, "RefreshToken") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, RefreshTokenDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateRefreshTokenCommand(id, model));
            if (HandleResponseError(response, logger, "RefreshToken", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemRefreshTokenCommand(id));
            if (HandleResponseError(response, logger, "RefreshToken", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-refresh-token/{refreshTokensToken}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRefreshTokenResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetRefreshTokenAsync(string refreshTokensToken)
        {
            var response = await mediator.Send(new GetRefreshTokenQuery(refreshTokensToken));
            if (HandleResponseError(response, logger, "RefreshToken", $"RefreshTokensToken: '{refreshTokensToken}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-refresh-tokens/{refreshTokensToken}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateRefreshTokensAsync(string refreshTokensToken, [FromBody] UpdateRefreshTokensRequestDto updateRequest)
        {
            var response = await mediator.Send(new UpdateRefreshTokensCommand(refreshTokensToken, updateRequest));
            if (HandleResponseError(response, logger, "RefreshToken", $"RefreshTokensToken: '{refreshTokensToken}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}