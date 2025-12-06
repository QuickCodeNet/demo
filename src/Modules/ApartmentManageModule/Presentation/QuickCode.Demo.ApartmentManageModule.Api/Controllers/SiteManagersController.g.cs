using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.SiteManager;
using QuickCode.Demo.ApartmentManageModule.Application.Services.SiteManager;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class SiteManagersController : QuickCodeBaseApiController
    {
        private readonly ISiteManagerService service;
        private readonly ILogger<SiteManagersController> logger;
        private readonly IServiceProvider serviceProvider;
        public SiteManagersController(ISiteManagerService service, IServiceProvider serviceProvider, ILogger<SiteManagersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SiteManagerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "SiteManager", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "SiteManager") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SiteManagerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "SiteManager", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SiteManagerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SiteManagerDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "SiteManager") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SiteManagerDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "SiteManager", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "SiteManager", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-site-managers/{siteManagersSiteId:int}/{siteManagersIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSiteManagersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagersAsync(int siteManagersSiteId, bool siteManagersIsActive)
        {
            var response = await service.GetSiteManagersAsync(siteManagersSiteId, siteManagersIsActive);
            if (HandleResponseError(response, logger, "SiteManager", $"SiteManagersSiteId: '{siteManagersSiteId}', SiteManagersIsActive: '{siteManagersIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-manager-by-site/{siteManagersSiteId:int}/{siteManagersIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActiveManagerBySiteResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveManagerBySiteAsync(int siteManagersSiteId, bool siteManagersIsActive)
        {
            var response = await service.GetActiveManagerBySiteAsync(siteManagersSiteId, siteManagersIsActive);
            if (HandleResponseError(response, logger, "SiteManager", $"SiteManagersSiteId: '{siteManagersSiteId}', SiteManagersIsActive: '{siteManagersIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-site-manager-with-contact/{siteManagersContactId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSiteManagerWithContactResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagerWithContactAsync(int siteManagersContactId)
        {
            var response = await service.GetSiteManagerWithContactAsync(siteManagersContactId);
            if (HandleResponseError(response, logger, "SiteManager", $"SiteManagersContactId: '{siteManagersContactId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("check-site-has-manager/{siteManagersSiteId:int}/{siteManagersIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CheckSiteHasManagerAsync(int siteManagersSiteId, bool siteManagersIsActive)
        {
            var response = await service.CheckSiteHasManagerAsync(siteManagersSiteId, siteManagersIsActive);
            if (HandleResponseError(response, logger, "SiteManager", $"SiteManagersSiteId: '{siteManagersSiteId}', SiteManagersIsActive: '{siteManagersIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}