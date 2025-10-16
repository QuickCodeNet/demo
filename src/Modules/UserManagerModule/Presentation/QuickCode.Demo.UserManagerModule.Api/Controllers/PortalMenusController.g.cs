﻿using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalMenu;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalMenu;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalMenusController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PortalMenusController> logger;
        private readonly IServiceProvider serviceProvider;
        public PortalMenusController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PortalMenusController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PortalMenuDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListPortalMenuQuery(page, size));
            if (HandleResponseError(response, logger, "PortalMenu", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountPortalMenuQuery());
            if (HandleResponseError(response, logger, "PortalMenu") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalMenuDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string key)
        {
            var response = await mediator.Send(new GetItemPortalMenuQuery(key));
            if (HandleResponseError(response, logger, "PortalMenu", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortalMenuDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PortalMenuDto model)
        {
            var response = await mediator.Send(new InsertPortalMenuCommand(model));
            if (HandleResponseError(response, logger, "PortalMenu") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { key = response.Value.Key }, response.Value);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string key, PortalMenuDto model)
        {
            if (!(model.Key == key))
            {
                return BadRequest($"Key: '{key}' must be equal to model.Key: '{model.Key}'");
            }

            var response = await mediator.Send(new UpdatePortalMenuCommand(key, model));
            if (HandleResponseError(response, logger, "PortalMenu", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var response = await mediator.Send(new DeleteItemPortalMenuCommand(key));
            if (HandleResponseError(response, logger, "PortalMenu", $"Key: '{key}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}