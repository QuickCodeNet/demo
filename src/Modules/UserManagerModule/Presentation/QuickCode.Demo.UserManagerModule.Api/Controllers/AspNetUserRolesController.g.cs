﻿using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserRole;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserRole;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class AspNetUserRolesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<AspNetUserRolesController> logger;
        private readonly IServiceProvider serviceProvider;
        public AspNetUserRolesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<AspNetUserRolesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUserRoleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListAspNetUserRoleQuery(page, size));
            if (HandleResponseError(response, logger, "AspNetUserRole", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountAspNetUserRoleQuery());
            if (HandleResponseError(response, logger, "AspNetUserRole") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUserRoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string userId, string roleId)
        {
            var response = await mediator.Send(new GetItemAspNetUserRoleQuery(userId, roleId));
            if (HandleResponseError(response, logger, "AspNetUserRole", $"UserId: '{userId}', RoleId: '{roleId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AspNetUserRoleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(AspNetUserRoleDto model)
        {
            var response = await mediator.Send(new InsertAspNetUserRoleCommand(model));
            if (HandleResponseError(response, logger, "AspNetUserRole") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { userId = response.Value.UserId, roleId = response.Value.RoleId }, response.Value);
        }

        [HttpPut("{userId}/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string userId, string roleId, AspNetUserRoleDto model)
        {
            if (!(model.UserId == userId && model.RoleId == roleId))
            {
                return BadRequest($"UserId: '{userId}', RoleId: '{roleId}' must be equal to model.UserId: '{model.UserId}', model.RoleId: '{model.RoleId}'");
            }

            var response = await mediator.Send(new UpdateAspNetUserRoleCommand(userId, roleId, model));
            if (HandleResponseError(response, logger, "AspNetUserRole", $"UserId: '{userId}', RoleId: '{roleId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{userId}/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string userId, string roleId)
        {
            var response = await mediator.Send(new DeleteItemAspNetUserRoleCommand(userId, roleId));
            if (HandleResponseError(response, logger, "AspNetUserRole", $"UserId: '{userId}', RoleId: '{roleId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}