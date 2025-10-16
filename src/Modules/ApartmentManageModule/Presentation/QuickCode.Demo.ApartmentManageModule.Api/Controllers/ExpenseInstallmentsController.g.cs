using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Application.Services.ExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class ExpenseInstallmentsController : QuickCodeBaseApiController
    {
        private readonly IExpenseInstallmentService service;
        private readonly ILogger<ExpenseInstallmentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ExpenseInstallmentsController(IExpenseInstallmentService service, IServiceProvider serviceProvider, ILogger<ExpenseInstallmentsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "ExpenseInstallment", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "ExpenseInstallment") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseInstallmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExpenseInstallmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ExpenseInstallmentDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "ExpenseInstallment") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ExpenseInstallmentDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-expense-installments/{expenseInstallmentsExpenseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpenseInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsAsync(int expenseInstallmentsExpenseId)
        {
            var response = await service.GetExpenseInstallmentsAsync(expenseInstallmentsExpenseId);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsExpenseId: '{expenseInstallmentsExpenseId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-unpaid-installments/{expenseInstallmentsExpenseId:int}/{expenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUnpaidInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidInstallmentsAsync(int expenseInstallmentsExpenseId, bool expenseInstallmentsPaid)
        {
            var response = await service.GetUnpaidInstallmentsAsync(expenseInstallmentsExpenseId, expenseInstallmentsPaid);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsExpenseId: '{expenseInstallmentsExpenseId}', ExpenseInstallmentsPaid: '{expenseInstallmentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-overdue-installments/{expenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOverdueInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOverdueInstallmentsAsync(bool expenseInstallmentsPaid)
        {
            var response = await service.GetOverdueInstallmentsAsync(expenseInstallmentsPaid);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsPaid: '{expenseInstallmentsPaid}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-apartment-installments/{expenseInstallmentsSiteId:int}/{expenseInstallmentsApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentInstallmentsAsync(int expenseInstallmentsSiteId, int expenseInstallmentsApartmentId)
        {
            var response = await service.GetApartmentInstallmentsAsync(expenseInstallmentsSiteId, expenseInstallmentsApartmentId);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsSiteId: '{expenseInstallmentsSiteId}', ExpenseInstallmentsApartmentId: '{expenseInstallmentsApartmentId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-site-installments/{expenseInstallmentsSiteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSiteInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteInstallmentsAsync(int expenseInstallmentsSiteId)
        {
            var response = await service.GetSiteInstallmentsAsync(expenseInstallmentsSiteId);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsSiteId: '{expenseInstallmentsSiteId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{expenseInstallmentId}/flat-expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForExpenseInstallmentsAsync(int expenseInstallmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForExpenseInstallmentsAsync(expenseInstallmentsId);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsId: '{expenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{expenseInstallmentId}/flat-expense-installment/{flatExpenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForExpenseInstallmentsDetailsAsync(int expenseInstallmentsId, int flatExpenseInstallmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForExpenseInstallmentsDetailsAsync(expenseInstallmentsId, flatExpenseInstallmentsId);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsId: '{expenseInstallmentsId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("mark-installment-as-paid/{expenseInstallmentsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkInstallmentAsPaidAsync(int expenseInstallmentsId, [FromBody] MarkInstallmentAsPaidRequestDto updateRequest)
        {
            var response = await service.MarkInstallmentAsPaidAsync(expenseInstallmentsId, updateRequest);
            if (HandleResponseError(response, logger, "ExpenseInstallment", $"ExpenseInstallmentsId: '{expenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}