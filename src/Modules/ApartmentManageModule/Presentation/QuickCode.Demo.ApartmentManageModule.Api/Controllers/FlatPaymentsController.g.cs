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
    public partial class FlatPaymentsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<FlatPaymentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatPaymentsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<FlatPaymentsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new FlatPaymentsListQuery(page, size));
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
            var response = await mediator.Send(new FlatPaymentsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatPaymentsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new FlatPaymentsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatPayments Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatPaymentsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatPaymentsDto model)
        {
            var response = await mediator.Send(new FlatPaymentsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, FlatPaymentsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new FlatPaymentsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatPayments Table";
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
            var response = await mediator.Send(new FlatPaymentsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatPayments Table";
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

        [HttpGet("get-payments-by-flat-year-month/{flatPaymentsSiteId:int}/{flatPaymentsFlatId:int}/{flatPaymentsYearId:int}/{flatPaymentsMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentsGetPaymentsByFlatYearMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            var response = await mediator.Send(new FlatPaymentsGetPaymentsByFlatYearMonthQuery(flatPaymentsSiteId, flatPaymentsFlatId, flatPaymentsYearId, flatPaymentsMonthId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsYearId: '{flatPaymentsYearId}', FlatPaymentsMonthId: '{flatPaymentsMonthId}' not found in FlatPayments Table";
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

        [HttpGet("get-unpaid-payments-by-flat/{flatPaymentsSiteId:int}/{flatPaymentsFlatId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentsGetUnpaidPaymentsByFlatResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidPaymentsByFlatAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            var response = await mediator.Send(new FlatPaymentsGetUnpaidPaymentsByFlatQuery(flatPaymentsSiteId, flatPaymentsFlatId, flatPaymentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsPaid: '{flatPaymentsPaid}' not found in FlatPayments Table";
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

        [HttpGet("get-unpaid-payments-by-site/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentsGetUnpaidPaymentsBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidPaymentsBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await mediator.Send(new FlatPaymentsGetUnpaidPaymentsBySiteQuery(flatPaymentsSiteId, flatPaymentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}' not found in FlatPayments Table";
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

        [HttpGet("get-total-cash-in-safe/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentsGetTotalCashInSafeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalCashInSafeAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await mediator.Send(new FlatPaymentsGetTotalCashInSafeQuery(flatPaymentsSiteId, flatPaymentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}' not found in FlatPayments Table";
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

        [HttpGet("get-pending-payments-by-flat-year-month/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentsGetPendingPaymentsByFlatYearMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await mediator.Send(new FlatPaymentsGetPendingPaymentsByFlatYearMonthQuery(flatPaymentsSiteId, flatPaymentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}' not found in FlatPayments Table";
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

        [HttpGet("get-flat-payments-by-month/{flatPaymentsFlatId:int}/{flatPaymentsYearId:int}/{flatPaymentsMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentsGetFlatPaymentsByMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsByMonthAsync(int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            var response = await mediator.Send(new FlatPaymentsGetFlatPaymentsByMonthQuery(flatPaymentsFlatId, flatPaymentsYearId, flatPaymentsMonthId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsYearId: '{flatPaymentsYearId}', FlatPaymentsMonthId: '{flatPaymentsMonthId}' not found in FlatPayments Table";
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

        [HttpGet("get-payments-count-by-flat/{flatPaymentsFlatId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPaymentsCountByFlatAsync(int flatPaymentsFlatId)
        {
            var response = await mediator.Send(new FlatPaymentsGetPaymentsCountByFlatQuery(flatPaymentsFlatId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsFlatId: '{flatPaymentsFlatId}' not found in FlatPayments Table";
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

        [HttpGet("get-total-paid-amount-by-flat/{flatPaymentsFlatId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatPaymentsGetTotalPaidAmountByFlatResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalPaidAmountByFlatAsync(int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            var response = await mediator.Send(new FlatPaymentsGetTotalPaidAmountByFlatQuery(flatPaymentsFlatId, flatPaymentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsPaid: '{flatPaymentsPaid}' not found in FlatPayments Table";
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

        [HttpGet("get-unpaid-payments-count-by-site/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidPaymentsCountBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await mediator.Send(new FlatPaymentsGetUnpaidPaymentsCountBySiteQuery(flatPaymentsSiteId, flatPaymentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}' not found in FlatPayments Table";
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

        [HttpPatch("mark-payment-as-paid/{flatPaymentsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkPaymentAsPaidAsync(int flatPaymentsId, [FromBody] FlatPaymentsMarkPaymentAsPaidRequestDto updateRequest)
        {
            var response = await mediator.Send(new FlatPaymentsMarkPaymentAsPaidCommand(flatPaymentsId, updateRequest));
            if (response.Code == 400)
            {
                return BadRequest($"Update Error: Response Code: {response.Code}, Message: {response.Message}");
            }
            else if (response.Code != 0)
            {
                return BadRequest($"Response Code: {response.Code}, Message: {response.Message}");
            }

            return Ok(response.Value);
        }
    }
}