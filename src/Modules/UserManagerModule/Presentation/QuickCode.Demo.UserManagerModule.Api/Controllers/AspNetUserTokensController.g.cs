﻿using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserToken;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserToken;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class AspNetUserTokensController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<AspNetUserTokensController> logger;
        private readonly IServiceProvider serviceProvider;
        public AspNetUserTokensController(IMediator mediator, IServiceProvider serviceProvider, ILogger<AspNetUserTokensController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUserTokenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListAspNetUserTokenQuery(page, size));
            if (HandleResponseError(response, logger, "AspNetUserToken", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountAspNetUserTokenQuery());
            if (HandleResponseError(response, logger, "AspNetUserToken") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUserTokenDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string userId)
        {
            var response = await mediator.Send(new GetItemAspNetUserTokenQuery(userId));
            if (HandleResponseError(response, logger, "AspNetUserToken", $"UserId: '{userId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AspNetUserTokenDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(AspNetUserTokenDto model)
        {
            var response = await mediator.Send(new InsertAspNetUserTokenCommand(model));
            if (HandleResponseError(response, logger, "AspNetUserToken") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { userId = response.Value.UserId }, response.Value);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string userId, AspNetUserTokenDto model)
        {
            if (!(model.UserId == userId))
            {
                return BadRequest($"UserId: '{userId}' must be equal to model.UserId: '{model.UserId}'");
            }

            var response = await mediator.Send(new UpdateAspNetUserTokenCommand(userId, model));
            if (HandleResponseError(response, logger, "AspNetUserToken", $"UserId: '{userId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            var response = await mediator.Send(new DeleteItemAspNetUserTokenCommand(userId));
            if (HandleResponseError(response, logger, "AspNetUserToken", $"UserId: '{userId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}