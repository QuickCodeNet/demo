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
    public partial class PaymentYearsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<PaymentYearsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PaymentYearsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<PaymentYearsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentYearsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new PaymentYearsListQuery(page, size));
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
            var response = await mediator.Send(new PaymentYearsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentYearsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new PaymentYearsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentYears Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentYearsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PaymentYearsDto model)
        {
            var response = await mediator.Send(new PaymentYearsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, PaymentYearsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new PaymentYearsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentYears Table";
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
            var response = await mediator.Send(new PaymentYearsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in PaymentYears Table";
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

        [HttpGet("get-all-years")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentYearsGetAllYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAllYearsAsync()
        {
            var response = await mediator.Send(new PaymentYearsGetAllYearsQuery());
            if (response.Code == 404)
            {
                var notFoundMessage = $" not found in PaymentYears Table";
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

        [HttpGet("{paymentYearsId}/apartment-fee-plans")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentYearsAsync(int paymentYearsId)
        {
            var response = await mediator.Send(new PaymentYearsGetApartmentFeePlansForPaymentYearsQuery(paymentYearsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentYearsId: '{paymentYearsId}' not found in PaymentYears Table";
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

        [HttpGet("{paymentYearsId}/apartment-fee-plans/{apartmentFeePlansId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentYearsDetailsAsync(int paymentYearsId, int apartmentFeePlansId)
        {
            var response = await mediator.Send(new PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsQuery(paymentYearsId, apartmentFeePlansId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentYearsId: '{paymentYearsId}', ApartmentFeePlansId: '{apartmentFeePlansId}' not found in PaymentYears Table";
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

        [HttpGet("{paymentYearsId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentYearsAsync(int paymentYearsId)
        {
            var response = await mediator.Send(new PaymentYearsGetFlatPaymentsForPaymentYearsQuery(paymentYearsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentYearsId: '{paymentYearsId}' not found in PaymentYears Table";
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

        [HttpGet("{paymentYearsId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentYearsDetailsAsync(int paymentYearsId, int flatPaymentsId)
        {
            var response = await mediator.Send(new PaymentYearsGetFlatPaymentsForPaymentYearsDetailsQuery(paymentYearsId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentYearsId: '{paymentYearsId}', FlatPaymentsId: '{flatPaymentsId}' not found in PaymentYears Table";
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

        [HttpGet("{paymentYearsId}/common-expenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentYearsGetCommonExpensesForPaymentYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentYearsAsync(int paymentYearsId)
        {
            var response = await mediator.Send(new PaymentYearsGetCommonExpensesForPaymentYearsQuery(paymentYearsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentYearsId: '{paymentYearsId}' not found in PaymentYears Table";
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

        [HttpGet("{paymentYearsId}/common-expenses/{commonExpensesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentYearsGetCommonExpensesForPaymentYearsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentYearsDetailsAsync(int paymentYearsId, int commonExpensesId)
        {
            var response = await mediator.Send(new PaymentYearsGetCommonExpensesForPaymentYearsDetailsQuery(paymentYearsId, commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"PaymentYearsId: '{paymentYearsId}', CommonExpensesId: '{commonExpensesId}' not found in PaymentYears Table";
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