using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatPayment;
using QuickCode.Demo.ApartmentManageModule.Application.Services.FlatPayment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class FlatPaymentsController : QuickCodeBaseApiController
    {
        private readonly IFlatPaymentService service;
        private readonly ILogger<FlatPaymentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatPaymentsController(IFlatPaymentService service, IServiceProvider serviceProvider, ILogger<FlatPaymentsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatPaymentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "FlatPayment", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "FlatPayment") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatPaymentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "FlatPayment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatPaymentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatPaymentDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "FlatPayment") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, FlatPaymentDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "FlatPayment", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "FlatPayment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-payments-by-flat-year-month/{flatPaymentsSiteId:int}/{flatPaymentsFlatId:int}/{flatPaymentsYearId:int}/{flatPaymentsMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPaymentsByFlatYearMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            var response = await service.GetPaymentsByFlatYearMonthAsync(flatPaymentsSiteId, flatPaymentsFlatId, flatPaymentsYearId, flatPaymentsMonthId);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsYearId: '{flatPaymentsYearId}', FlatPaymentsMonthId: '{flatPaymentsMonthId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-unpaid-payments-by-flat/{flatPaymentsSiteId:int}/{flatPaymentsFlatId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUnpaidPaymentsByFlatResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidPaymentsByFlatAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            var response = await service.GetUnpaidPaymentsByFlatAsync(flatPaymentsSiteId, flatPaymentsFlatId, flatPaymentsPaid);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsPaid: '{flatPaymentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-unpaid-payments-by-site/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUnpaidPaymentsBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidPaymentsBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await service.GetUnpaidPaymentsBySiteAsync(flatPaymentsSiteId, flatPaymentsPaid);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-total-cash-in-safe/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTotalCashInSafeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalCashInSafeAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await service.GetTotalCashInSafeAsync(flatPaymentsSiteId, flatPaymentsPaid);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-payments-by-flat-year-month/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPendingPaymentsByFlatYearMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await service.GetPendingPaymentsByFlatYearMonthAsync(flatPaymentsSiteId, flatPaymentsPaid);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-payments-by-month/{flatPaymentsFlatId:int}/{flatPaymentsYearId:int}/{flatPaymentsMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsByMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsByMonthAsync(int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            var response = await service.GetFlatPaymentsByMonthAsync(flatPaymentsFlatId, flatPaymentsYearId, flatPaymentsMonthId);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsYearId: '{flatPaymentsYearId}', FlatPaymentsMonthId: '{flatPaymentsMonthId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-payments-count-by-flat/{flatPaymentsFlatId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPaymentsCountByFlatAsync(int flatPaymentsFlatId)
        {
            var response = await service.GetPaymentsCountByFlatAsync(flatPaymentsFlatId);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsFlatId: '{flatPaymentsFlatId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-total-paid-amount-by-flat/{flatPaymentsFlatId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTotalPaidAmountByFlatResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalPaidAmountByFlatAsync(int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            var response = await service.GetTotalPaidAmountByFlatAsync(flatPaymentsFlatId, flatPaymentsPaid);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsFlatId: '{flatPaymentsFlatId}', FlatPaymentsPaid: '{flatPaymentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-unpaid-payments-count-by-site/{flatPaymentsSiteId:int}/{flatPaymentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidPaymentsCountBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var response = await service.GetUnpaidPaymentsCountBySiteAsync(flatPaymentsSiteId, flatPaymentsPaid);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsSiteId: '{flatPaymentsSiteId}', FlatPaymentsPaid: '{flatPaymentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("mark-payment-as-paid/{flatPaymentsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkPaymentAsPaidAsync(int flatPaymentsId, [FromBody] MarkPaymentAsPaidRequestDto updateRequest)
        {
            var response = await service.MarkPaymentAsPaidAsync(flatPaymentsId, updateRequest);
            if (HandleResponseError(response, logger, "FlatPayment", $"FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}