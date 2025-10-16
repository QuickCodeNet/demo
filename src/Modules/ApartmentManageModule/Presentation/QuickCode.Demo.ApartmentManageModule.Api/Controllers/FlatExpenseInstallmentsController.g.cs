using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Application.Services.FlatExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class FlatExpenseInstallmentsController : QuickCodeBaseApiController
    {
        private readonly IFlatExpenseInstallmentService service;
        private readonly ILogger<FlatExpenseInstallmentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatExpenseInstallmentsController(IFlatExpenseInstallmentService service, IServiceProvider serviceProvider, ILogger<FlatExpenseInstallmentsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatExpenseInstallmentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "FlatExpenseInstallment") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatExpenseInstallmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatExpenseInstallmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatExpenseInstallmentDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, FlatExpenseInstallmentDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-expense-installments/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsExpenseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatExpenseInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsAsync(int flatExpenseInstallmentsFlatId, int flatExpenseInstallmentsExpenseId)
        {
            var response = await service.GetFlatExpenseInstallmentsAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsExpenseId);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsExpenseId: '{flatExpenseInstallmentsExpenseId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-unpaid-installments/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatUnpaidInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatUnpaidInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var response = await service.GetFlatUnpaidInstallmentsAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsPaid: '{flatExpenseInstallmentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-overdue-installments/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatOverdueInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatOverdueInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var response = await service.GetFlatOverdueInstallmentsAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsPaid: '{flatExpenseInstallmentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-apartment-flat-installments/{flatExpenseInstallmentsSiteId:int}/{flatExpenseInstallmentsApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentFlatInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFlatInstallmentsAsync(int flatExpenseInstallmentsSiteId, int flatExpenseInstallmentsApartmentId)
        {
            var response = await service.GetApartmentFlatInstallmentsAsync(flatExpenseInstallmentsSiteId, flatExpenseInstallmentsApartmentId);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"FlatExpenseInstallmentsSiteId: '{flatExpenseInstallmentsSiteId}', FlatExpenseInstallmentsApartmentId: '{flatExpenseInstallmentsApartmentId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-total-debt/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatTotalDebtResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatTotalDebtAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var response = await service.GetFlatTotalDebtAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsPaid: '{flatExpenseInstallmentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("mark-flat-installment-as-paid/{flatExpenseInstallmentsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkFlatInstallmentAsPaidAsync(int flatExpenseInstallmentsId, [FromBody] MarkFlatInstallmentAsPaidRequestDto updateRequest)
        {
            var response = await service.MarkFlatInstallmentAsPaidAsync(flatExpenseInstallmentsId, updateRequest);
            if (HandleResponseError(response, logger, "FlatExpenseInstallment", $"FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}