using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Company;
using QuickCode.Demo.OnlineShopModule.Application.Services.Company;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
{
    public partial class CompaniesController : QuickCodeBaseApiController
    {
        private readonly ICompanyService service;
        private readonly ILogger<CompaniesController> logger;
        private readonly IServiceProvider serviceProvider;
        public CompaniesController(ICompanyService service, IServiceProvider serviceProvider, ILogger<CompaniesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanyDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Company", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Company") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Company", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(CompanyDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Company") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, CompanyDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Company", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Company", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-companies/{companiesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCompaniesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCompaniesAsync(int companiesId)
        {
            var response = await service.GetCompaniesAsync(companiesId);
            if (HandleResponseError(response, logger, "Company", $"CompaniesId: '{companiesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{companyId}/user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUsersForCompaniesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUsersForCompaniesAsync(int companiesId)
        {
            var response = await service.GetUsersForCompaniesAsync(companiesId);
            if (HandleResponseError(response, logger, "Company", $"CompaniesId: '{companiesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{companyId}/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersForCompaniesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUsersForCompaniesDetailsAsync(int companiesId, int usersId)
        {
            var response = await service.GetUsersForCompaniesDetailsAsync(companiesId, usersId);
            if (HandleResponseError(response, logger, "Company", $"CompaniesId: '{companiesId}', UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}