using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentType;
using QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentType;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class PaymentTypesController : QuickCodeBaseApiController
    {
        private readonly IPaymentTypeService service;
        private readonly ILogger<PaymentTypesController> logger;
        private readonly IServiceProvider serviceProvider;
        public PaymentTypesController(IPaymentTypeService service, IServiceProvider serviceProvider, ILogger<PaymentTypesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentTypeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "PaymentType", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "PaymentType") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "PaymentType", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PaymentTypeDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "PaymentType") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, PaymentTypeDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "PaymentType", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "PaymentType", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-payment-types/{paymentTypesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActivePaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActivePaymentTypesAsync(bool paymentTypesIsActive)
        {
            var response = await service.GetActivePaymentTypesAsync(paymentTypesIsActive);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesIsActive: '{paymentTypesIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await service.GetFlatPaymentsForPaymentTypesAsync(paymentTypesId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForPaymentTypesDetailsAsync(paymentTypesId, flatPaymentsId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/common-expense")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCommonExpensesForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await service.GetCommonExpensesForPaymentTypesAsync(paymentTypesId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/common-expense/{commonExpenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCommonExpensesForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentTypesDetailsAsync(int paymentTypesId, int commonExpensesId)
        {
            var response = await service.GetCommonExpensesForPaymentTypesDetailsAsync(paymentTypesId, commonExpensesId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}', CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpenseInstallmentsForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await service.GetExpenseInstallmentsForPaymentTypesAsync(paymentTypesId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/expense-installment/{expenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExpenseInstallmentsForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int expenseInstallmentsId)
        {
            var response = await service.GetExpenseInstallmentsForPaymentTypesDetailsAsync(paymentTypesId, expenseInstallmentsId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}', ExpenseInstallmentsId: '{expenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/flat-expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatExpenseInstallmentsForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await service.GetFlatExpenseInstallmentsForPaymentTypesAsync(paymentTypesId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentTypeId}/flat-expense-installment/{flatExpenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatExpenseInstallmentsForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatExpenseInstallmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForPaymentTypesDetailsAsync(paymentTypesId, flatExpenseInstallmentsId);
            if (HandleResponseError(response, logger, "PaymentType", $"PaymentTypesId: '{paymentTypesId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}