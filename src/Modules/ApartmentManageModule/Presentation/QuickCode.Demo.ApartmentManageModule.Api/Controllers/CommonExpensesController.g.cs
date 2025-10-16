using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.CommonExpense;
using QuickCode.Demo.ApartmentManageModule.Application.Services.CommonExpense;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class CommonExpensesController : QuickCodeBaseApiController
    {
        private readonly ICommonExpenseService service;
        private readonly ILogger<CommonExpensesController> logger;
        private readonly IServiceProvider serviceProvider;
        public CommonExpensesController(ICommonExpenseService service, IServiceProvider serviceProvider, ILogger<CommonExpensesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpenseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "CommonExpense", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "CommonExpense") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonExpenseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "CommonExpense", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommonExpenseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(CommonExpenseDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "CommonExpense") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, CommonExpenseDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "CommonExpense", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "CommonExpense", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-expenses-by-apartment-month/{commonExpensesSiteId:int}/{commonExpensesApartmentId:int}/{commonExpensesYearId:int}/{commonExpensesMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpensesByApartmentMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesByApartmentMonthAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            var response = await service.GetExpensesByApartmentMonthAsync(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesYearId, commonExpensesMonthId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesSiteId: '{commonExpensesSiteId}', CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesYearId: '{commonExpensesYearId}', CommonExpensesMonthId: '{commonExpensesMonthId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-expenses-by-site/{commonExpensesSiteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpensesBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesBySiteAsync(int commonExpensesSiteId)
        {
            var response = await service.GetExpensesBySiteAsync(commonExpensesSiteId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesSiteId: '{commonExpensesSiteId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-expenses-summary-by-year/{commonExpensesSiteId:int}/{commonExpensesApartmentId:int}/{commonExpensesYearId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpensesSummaryByYearResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesSummaryByYearAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId)
        {
            var response = await service.GetExpensesSummaryByYearAsync(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesYearId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesSiteId: '{commonExpensesSiteId}', CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesYearId: '{commonExpensesYearId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-expenses-by-type/{commonExpensesSiteId:int}/{commonExpensesApartmentId:int}/{commonExpensesExpenseTypeId:int}/{commonExpensesYearId:int}/{commonExpensesMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpensesByTypeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesByTypeAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesExpenseTypeId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            var response = await service.GetExpensesByTypeAsync(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesExpenseTypeId, commonExpensesYearId, commonExpensesMonthId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesSiteId: '{commonExpensesSiteId}', CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesExpenseTypeId: '{commonExpensesExpenseTypeId}', CommonExpensesYearId: '{commonExpensesYearId}', CommonExpensesMonthId: '{commonExpensesMonthId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-unpaid-expenses-by-apartment/{commonExpensesApartmentId:int}/{commonExpensesPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUnpaidExpensesByApartmentResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidExpensesByApartmentAsync(int commonExpensesApartmentId, bool commonExpensesPaid)
        {
            var response = await service.GetUnpaidExpensesByApartmentAsync(commonExpensesApartmentId, commonExpensesPaid);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesPaid: '{commonExpensesPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-expenses-count-by-apartment/{commonExpensesApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesCountByApartmentAsync(int commonExpensesApartmentId)
        {
            var response = await service.GetExpensesCountByApartmentAsync(commonExpensesApartmentId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesApartmentId: '{commonExpensesApartmentId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-total-expense-amount-by-apartment/{commonExpensesApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTotalExpenseAmountByApartmentResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalExpenseAmountByApartmentAsync(int commonExpensesApartmentId)
        {
            var response = await service.GetTotalExpenseAmountByApartmentAsync(commonExpensesApartmentId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesApartmentId: '{commonExpensesApartmentId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{commonExpenseId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForCommonExpensesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForCommonExpensesAsync(int commonExpensesId)
        {
            var response = await service.GetFlatPaymentsForCommonExpensesAsync(commonExpensesId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{commonExpenseId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForCommonExpensesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForCommonExpensesDetailsAsync(commonExpensesId, flatPaymentsId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesId: '{commonExpensesId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{commonExpenseId}/expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpenseInstallmentsForCommonExpensesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId)
        {
            var response = await service.GetExpenseInstallmentsForCommonExpensesAsync(commonExpensesId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{commonExpenseId}/expense-installment/{expenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExpenseInstallmentsForCommonExpensesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int expenseInstallmentsId)
        {
            var response = await service.GetExpenseInstallmentsForCommonExpensesDetailsAsync(commonExpensesId, expenseInstallmentsId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesId: '{commonExpensesId}', ExpenseInstallmentsId: '{expenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{commonExpenseId}/flat-expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatExpenseInstallmentsForCommonExpensesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId)
        {
            var response = await service.GetFlatExpenseInstallmentsForCommonExpensesAsync(commonExpensesId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{commonExpenseId}/flat-expense-installment/{flatExpenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatExpenseInstallmentsForCommonExpensesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatExpenseInstallmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForCommonExpensesDetailsAsync(commonExpensesId, flatExpenseInstallmentsId);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesId: '{commonExpensesId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("mark-expense-as-paid/{commonExpensesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkExpenseAsPaidAsync(int commonExpensesId, [FromBody] MarkExpenseAsPaidRequestDto updateRequest)
        {
            var response = await service.MarkExpenseAsPaidAsync(commonExpensesId, updateRequest);
            if (HandleResponseError(response, logger, "CommonExpense", $"CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}