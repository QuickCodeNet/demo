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
    public partial class PaymentMonthsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PaymentMonthsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PaymentMonthsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PaymentMonthsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentMonthsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new PaymentMonthsListQuery(page, size));
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
            var response = await mediator.Send(new PaymentMonthsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentMonthsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new PaymentMonthsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentMonths Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentMonthsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PaymentMonthsDto model)
        {
            var response = await mediator.Send(new PaymentMonthsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, PaymentMonthsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new PaymentMonthsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentMonths Table";
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
            var response = await mediator.Send(new PaymentMonthsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentMonths Table";
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

        [HttpGet("get-all-months")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentMonthsGetAllMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAllMonthsAsync()
        {
            var response = await mediator.Send(new PaymentMonthsGetAllMonthsQuery());
            if (response.Code == 404)
            {
                var notFoundMessage = $" not found in PaymentMonths Table";
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

        [HttpGet("{paymentMonthsId}/apartment-fee-plans")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentMonthsAsync(int paymentMonthsId)
        {
            var response = await mediator.Send(new PaymentMonthsGetApartmentFeePlansForPaymentMonthsQuery(paymentMonthsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentMonthsId: '{paymentMonthsId}' not found in PaymentMonths Table";
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

        [HttpGet("{paymentMonthsId}/apartment-fee-plans/{apartmentFeePlansId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentMonthsDetailsAsync(int paymentMonthsId, int apartmentFeePlansId)
        {
            var response = await mediator.Send(new PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsQuery(paymentMonthsId, apartmentFeePlansId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentMonthsId: '{paymentMonthsId}', ApartmentFeePlansId: '{apartmentFeePlansId}' not found in PaymentMonths Table";
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

        [HttpGet("{paymentMonthsId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentMonthsAsync(int paymentMonthsId)
        {
            var response = await mediator.Send(new PaymentMonthsGetFlatPaymentsForPaymentMonthsQuery(paymentMonthsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentMonthsId: '{paymentMonthsId}' not found in PaymentMonths Table";
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

        [HttpGet("{paymentMonthsId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentMonthsDetailsAsync(int paymentMonthsId, int flatPaymentsId)
        {
            var response = await mediator.Send(new PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsQuery(paymentMonthsId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentMonthsId: '{paymentMonthsId}', FlatPaymentsId: '{flatPaymentsId}' not found in PaymentMonths Table";
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

        [HttpGet("{paymentMonthsId}/common-expenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentMonthsAsync(int paymentMonthsId)
        {
            var response = await mediator.Send(new PaymentMonthsGetCommonExpensesForPaymentMonthsQuery(paymentMonthsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentMonthsId: '{paymentMonthsId}' not found in PaymentMonths Table";
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

        [HttpGet("{paymentMonthsId}/common-expenses/{commonExpensesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentMonthsDetailsAsync(int paymentMonthsId, int commonExpensesId)
        {
            var response = await mediator.Send(new PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsQuery(paymentMonthsId, commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentMonthsId: '{paymentMonthsId}', CommonExpensesId: '{commonExpensesId}' not found in PaymentMonths Table";
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