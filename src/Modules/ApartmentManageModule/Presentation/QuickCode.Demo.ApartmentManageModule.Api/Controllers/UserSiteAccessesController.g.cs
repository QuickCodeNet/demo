﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.UserSiteAccess;
using QuickCode.Demo.ApartmentManageModule.Application.Services.UserSiteAccess;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class UserSiteAccessesController : QuickCodeBaseApiController
    {
        private readonly IUserSiteAccessService service;
        private readonly ILogger<UserSiteAccessesController> logger;
        private readonly IServiceProvider serviceProvider;
        public UserSiteAccessesController(IUserSiteAccessService service, IServiceProvider serviceProvider, ILogger<UserSiteAccessesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserSiteAccessDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "UserSiteAccess", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "UserSiteAccess") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSiteAccessDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "UserSiteAccess", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserSiteAccessDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(UserSiteAccessDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "UserSiteAccess") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, UserSiteAccessDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "UserSiteAccess", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "UserSiteAccess", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user-sites/{userSiteAccessesUserId:int}/{userSiteAccessesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserSitesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSitesAsync(int userSiteAccessesUserId, bool userSiteAccessesIsActive)
        {
            var response = await service.GetUserSitesAsync(userSiteAccessesUserId, userSiteAccessesIsActive);
            if (HandleResponseError(response, logger, "UserSiteAccess", $"UserSiteAccessesUserId: '{userSiteAccessesUserId}', UserSiteAccessesIsActive: '{userSiteAccessesIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-site-users/{userSiteAccessesSiteId:int}/{userSiteAccessesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSiteUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteUsersAsync(int userSiteAccessesSiteId, bool userSiteAccessesIsActive)
        {
            var response = await service.GetSiteUsersAsync(userSiteAccessesSiteId, userSiteAccessesIsActive);
            if (HandleResponseError(response, logger, "UserSiteAccess", $"UserSiteAccessesSiteId: '{userSiteAccessesSiteId}', UserSiteAccessesIsActive: '{userSiteAccessesIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user-flats/{userSiteAccessesUserId:int}/{userSiteAccessesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserFlatsAsync(int userSiteAccessesUserId, bool userSiteAccessesIsActive)
        {
            var response = await service.GetUserFlatsAsync(userSiteAccessesUserId, userSiteAccessesIsActive);
            if (HandleResponseError(response, logger, "UserSiteAccess", $"UserSiteAccessesUserId: '{userSiteAccessesUserId}', UserSiteAccessesIsActive: '{userSiteAccessesIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}