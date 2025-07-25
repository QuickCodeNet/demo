using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Application.Features;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class ApartmentFeePlansController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<ApartmentFeePlansController> logger;
        private readonly IServiceProvider serviceProvider;
        public ApartmentFeePlansController(IMediator mediator, IServiceProvider serviceProvider, ILogger<ApartmentFeePlansController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentFeePlansDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new ApartmentFeePlansListQuery(page, size));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new ApartmentFeePlansTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentFeePlansDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new ApartmentFeePlansGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ApartmentFeePlans Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApartmentFeePlansDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApartmentFeePlansDto model)
        {
            var response = await mediator.Send(new ApartmentFeePlansInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ApartmentFeePlansDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new ApartmentFeePlansUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ApartmentFeePlans Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await mediator.Send(new ApartmentFeePlansDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ApartmentFeePlans Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-fee-plan-by-year-month/{apartmentFeePlansSiteId:int}/{apartmentFeePlansApartmentId:int}/{apartmentFeePlansYearId:int}/{apartmentFeePlansMonthId:int}/{apartmentFeePlansIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentFeePlansGetFeePlanByYearMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFeePlanByYearMonthAsync(int apartmentFeePlansSiteId, int apartmentFeePlansApartmentId, int apartmentFeePlansYearId, int apartmentFeePlansMonthId, bool apartmentFeePlansIsActive)
        {
            var response = await mediator.Send(new ApartmentFeePlansGetFeePlanByYearMonthQuery(apartmentFeePlansSiteId, apartmentFeePlansApartmentId, apartmentFeePlansYearId, apartmentFeePlansMonthId, apartmentFeePlansIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentFeePlansSiteId: '{apartmentFeePlansSiteId}', ApartmentFeePlansApartmentId: '{apartmentFeePlansApartmentId}', ApartmentFeePlansYearId: '{apartmentFeePlansYearId}', ApartmentFeePlansMonthId: '{apartmentFeePlansMonthId}', ApartmentFeePlansIsActive: '{apartmentFeePlansIsActive}' not found in ApartmentFeePlans Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-fee-plans-by-site/{apartmentFeePlansSiteId:int}/{apartmentFeePlansIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentFeePlansGetFeePlansBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFeePlansBySiteAsync(int apartmentFeePlansSiteId, bool apartmentFeePlansIsActive)
        {
            var response = await mediator.Send(new ApartmentFeePlansGetFeePlansBySiteQuery(apartmentFeePlansSiteId, apartmentFeePlansIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentFeePlansSiteId: '{apartmentFeePlansSiteId}', ApartmentFeePlansIsActive: '{apartmentFeePlansIsActive}' not found in ApartmentFeePlans Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{apartmentFeePlansId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentFeePlansAsync(int apartmentFeePlansId)
        {
            var response = await mediator.Send(new ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansQuery(apartmentFeePlansId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentFeePlansId: '{apartmentFeePlansId}' not found in ApartmentFeePlans Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{apartmentFeePlansId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentFeePlansDetailsAsync(int apartmentFeePlansId, int flatPaymentsId)
        {
            var response = await mediator.Send(new ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsQuery(apartmentFeePlansId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentFeePlansId: '{apartmentFeePlansId}', FlatPaymentsId: '{flatPaymentsId}' not found in ApartmentFeePlans Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }
    }
}