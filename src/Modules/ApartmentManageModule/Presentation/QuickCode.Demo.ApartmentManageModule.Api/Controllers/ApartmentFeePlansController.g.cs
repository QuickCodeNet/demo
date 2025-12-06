using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ApartmentFeePlan;
using QuickCode.Demo.ApartmentManageModule.Application.Services.ApartmentFeePlan;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class ApartmentFeePlansController : QuickCodeBaseApiController
    {
        private readonly IApartmentFeePlanService service;
        private readonly ILogger<ApartmentFeePlansController> logger;
        private readonly IServiceProvider serviceProvider;
        public ApartmentFeePlansController(IApartmentFeePlanService service, IServiceProvider serviceProvider, ILogger<ApartmentFeePlansController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentFeePlanDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "ApartmentFeePlan", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "ApartmentFeePlan") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentFeePlanDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "ApartmentFeePlan", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApartmentFeePlanDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApartmentFeePlanDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "ApartmentFeePlan") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ApartmentFeePlanDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "ApartmentFeePlan", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "ApartmentFeePlan", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-fee-plan-by-year-month/{apartmentFeePlansSiteId:int}/{apartmentFeePlansApartmentId:int}/{apartmentFeePlansYearId:int}/{apartmentFeePlansMonthId:int}/{apartmentFeePlansIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFeePlanByYearMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFeePlanByYearMonthAsync(int apartmentFeePlansSiteId, int apartmentFeePlansApartmentId, int apartmentFeePlansYearId, int apartmentFeePlansMonthId, bool apartmentFeePlansIsActive)
        {
            var response = await service.GetFeePlanByYearMonthAsync(apartmentFeePlansSiteId, apartmentFeePlansApartmentId, apartmentFeePlansYearId, apartmentFeePlansMonthId, apartmentFeePlansIsActive);
            if (HandleResponseError(response, logger, "ApartmentFeePlan", $"ApartmentFeePlansSiteId: '{apartmentFeePlansSiteId}', ApartmentFeePlansApartmentId: '{apartmentFeePlansApartmentId}', ApartmentFeePlansYearId: '{apartmentFeePlansYearId}', ApartmentFeePlansMonthId: '{apartmentFeePlansMonthId}', ApartmentFeePlansIsActive: '{apartmentFeePlansIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-fee-plans-by-site/{apartmentFeePlansSiteId:int}/{apartmentFeePlansIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFeePlansBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFeePlansBySiteAsync(int apartmentFeePlansSiteId, bool apartmentFeePlansIsActive)
        {
            var response = await service.GetFeePlansBySiteAsync(apartmentFeePlansSiteId, apartmentFeePlansIsActive);
            if (HandleResponseError(response, logger, "ApartmentFeePlan", $"ApartmentFeePlansSiteId: '{apartmentFeePlansSiteId}', ApartmentFeePlansIsActive: '{apartmentFeePlansIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentFeePlanId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForApartmentFeePlansResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentFeePlansAsync(int apartmentFeePlansId)
        {
            var response = await service.GetFlatPaymentsForApartmentFeePlansAsync(apartmentFeePlansId);
            if (HandleResponseError(response, logger, "ApartmentFeePlan", $"ApartmentFeePlansId: '{apartmentFeePlansId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentFeePlanId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForApartmentFeePlansResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentFeePlansDetailsAsync(int apartmentFeePlansId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForApartmentFeePlansDetailsAsync(apartmentFeePlansId, flatPaymentsId);
            if (HandleResponseError(response, logger, "ApartmentFeePlan", $"ApartmentFeePlansId: '{apartmentFeePlansId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}