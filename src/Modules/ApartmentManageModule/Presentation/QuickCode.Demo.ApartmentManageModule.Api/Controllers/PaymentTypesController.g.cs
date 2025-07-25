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
    public partial class PaymentTypesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PaymentTypesController> logger;
        private readonly IServiceProvider serviceProvider;
        public PaymentTypesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PaymentTypesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentTypesDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new PaymentTypesListQuery(page, size));
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
            var response = await mediator.Send(new PaymentTypesTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypesDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new PaymentTypesGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentTypes Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentTypesDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PaymentTypesDto model)
        {
            var response = await mediator.Send(new PaymentTypesInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, PaymentTypesDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new PaymentTypesUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentTypes Table";
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
            var response = await mediator.Send(new PaymentTypesDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentTypes Table";
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

        [HttpGet("get-active-payment-types/{paymentTypesIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentTypesGetActivePaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActivePaymentTypesAsync(bool paymentTypesIsActive)
        {
            var response = await mediator.Send(new PaymentTypesGetActivePaymentTypesQuery(paymentTypesIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesIsActive: '{paymentTypesIsActive}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await mediator.Send(new PaymentTypesGetFlatPaymentsForPaymentTypesQuery(paymentTypesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatPaymentsId)
        {
            var response = await mediator.Send(new PaymentTypesGetFlatPaymentsForPaymentTypesDetailsQuery(paymentTypesId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}', FlatPaymentsId: '{flatPaymentsId}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/common-expenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentTypesGetCommonExpensesForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await mediator.Send(new PaymentTypesGetCommonExpensesForPaymentTypesQuery(paymentTypesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/common-expenses/{commonExpensesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypesGetCommonExpensesForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentTypesDetailsAsync(int paymentTypesId, int commonExpensesId)
        {
            var response = await mediator.Send(new PaymentTypesGetCommonExpensesForPaymentTypesDetailsQuery(paymentTypesId, commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}', CommonExpensesId: '{commonExpensesId}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await mediator.Send(new PaymentTypesGetExpenseInstallmentsForPaymentTypesQuery(paymentTypesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/expense-installments/{expenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int expenseInstallmentsId)
        {
            var response = await mediator.Send(new PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsQuery(paymentTypesId, expenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}', ExpenseInstallmentsId: '{expenseInstallmentsId}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/flat-expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForPaymentTypesAsync(int paymentTypesId)
        {
            var response = await mediator.Send(new PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesQuery(paymentTypesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}' not found in PaymentTypes Table";
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

        [HttpGet("{paymentTypesId}/flat-expense-installments/{flatExpenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForPaymentTypesDetailsAsync(int paymentTypesId, int flatExpenseInstallmentsId)
        {
            var response = await mediator.Send(new PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsQuery(paymentTypesId, flatExpenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentTypesId: '{paymentTypesId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}' not found in PaymentTypes Table";
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