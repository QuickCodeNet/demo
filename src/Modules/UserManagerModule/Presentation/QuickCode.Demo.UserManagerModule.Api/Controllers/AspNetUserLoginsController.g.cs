﻿using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserLogin;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserLogin;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class AspNetUserLoginsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<AspNetUserLoginsController> logger;
        private readonly IServiceProvider serviceProvider;
        public AspNetUserLoginsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<AspNetUserLoginsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUserLoginDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListAspNetUserLoginQuery(page, size));
            if (HandleResponseError(response, logger, "AspNetUserLogin", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountAspNetUserLoginQuery());
            if (HandleResponseError(response, logger, "AspNetUserLogin") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{loginProvider}/{providerKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUserLoginDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string loginProvider, string providerKey)
        {
            var response = await mediator.Send(new GetItemAspNetUserLoginQuery(loginProvider, providerKey));
            if (HandleResponseError(response, logger, "AspNetUserLogin", $"LoginProvider: '{loginProvider}', ProviderKey: '{providerKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AspNetUserLoginDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(AspNetUserLoginDto model)
        {
            var response = await mediator.Send(new InsertAspNetUserLoginCommand(model));
            if (HandleResponseError(response, logger, "AspNetUserLogin") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { loginProvider = response.Value.LoginProvider, providerKey = response.Value.ProviderKey }, response.Value);
        }

        [HttpPut("{loginProvider}/{providerKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string loginProvider, string providerKey, AspNetUserLoginDto model)
        {
            if (!(model.LoginProvider == loginProvider && model.ProviderKey == providerKey))
            {
                return BadRequest($"LoginProvider: '{loginProvider}', ProviderKey: '{providerKey}' must be equal to model.LoginProvider: '{model.LoginProvider}', model.ProviderKey: '{model.ProviderKey}'");
            }

            var response = await mediator.Send(new UpdateAspNetUserLoginCommand(loginProvider, providerKey, model));
            if (HandleResponseError(response, logger, "AspNetUserLogin", $"LoginProvider: '{loginProvider}', ProviderKey: '{providerKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{loginProvider}/{providerKey}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string loginProvider, string providerKey)
        {
            var response = await mediator.Send(new DeleteItemAspNetUserLoginCommand(loginProvider, providerKey));
            if (HandleResponseError(response, logger, "AspNetUserLogin", $"LoginProvider: '{loginProvider}', ProviderKey: '{providerKey}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}