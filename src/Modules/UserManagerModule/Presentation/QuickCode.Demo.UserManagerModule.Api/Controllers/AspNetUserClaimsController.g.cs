﻿using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserClaim;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserClaim;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class AspNetUserClaimsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<AspNetUserClaimsController> logger;
        private readonly IServiceProvider serviceProvider;
        public AspNetUserClaimsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<AspNetUserClaimsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUserClaimDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListAspNetUserClaimQuery(page, size));
            if (HandleResponseError(response, logger, "AspNetUserClaim", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountAspNetUserClaimQuery());
            if (HandleResponseError(response, logger, "AspNetUserClaim") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUserClaimDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new GetItemAspNetUserClaimQuery(id));
            if (HandleResponseError(response, logger, "AspNetUserClaim", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AspNetUserClaimDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(AspNetUserClaimDto model)
        {
            var response = await mediator.Send(new InsertAspNetUserClaimCommand(model));
            if (HandleResponseError(response, logger, "AspNetUserClaim") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, AspNetUserClaimDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateAspNetUserClaimCommand(id, model));
            if (HandleResponseError(response, logger, "AspNetUserClaim", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new DeleteItemAspNetUserClaimCommand(id));
            if (HandleResponseError(response, logger, "AspNetUserClaim", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}