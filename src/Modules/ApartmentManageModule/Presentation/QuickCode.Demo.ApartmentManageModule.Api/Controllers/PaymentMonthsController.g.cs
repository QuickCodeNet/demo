using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentMonth;
using QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentMonth;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class PaymentMonthsController : QuickCodeBaseApiController
    {
        private readonly IPaymentMonthService service;
        private readonly ILogger<PaymentMonthsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PaymentMonthsController(IPaymentMonthService service, IServiceProvider serviceProvider, ILogger<PaymentMonthsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentMonthDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "PaymentMonth", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "PaymentMonth") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentMonthDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "PaymentMonth", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentMonthDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PaymentMonthDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "PaymentMonth") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, PaymentMonthDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "PaymentMonth", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "PaymentMonth", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-all-months")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAllMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAllMonthsAsync()
        {
            var response = await service.GetAllMonthsAsync();
            if (HandleResponseError(response, logger, "PaymentMonth", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentMonthId}/apartment-fee-plan")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentFeePlansForPaymentMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentMonthsAsync(int paymentMonthsId)
        {
            var response = await service.GetApartmentFeePlansForPaymentMonthsAsync(paymentMonthsId);
            if (HandleResponseError(response, logger, "PaymentMonth", $"PaymentMonthsId: '{paymentMonthsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentMonthId}/apartment-fee-plan/{apartmentFeePlanId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApartmentFeePlansForPaymentMonthsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentMonthsDetailsAsync(int paymentMonthsId, int apartmentFeePlansId)
        {
            var response = await service.GetApartmentFeePlansForPaymentMonthsDetailsAsync(paymentMonthsId, apartmentFeePlansId);
            if (HandleResponseError(response, logger, "PaymentMonth", $"PaymentMonthsId: '{paymentMonthsId}', ApartmentFeePlansId: '{apartmentFeePlansId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentMonthId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForPaymentMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentMonthsAsync(int paymentMonthsId)
        {
            var response = await service.GetFlatPaymentsForPaymentMonthsAsync(paymentMonthsId);
            if (HandleResponseError(response, logger, "PaymentMonth", $"PaymentMonthsId: '{paymentMonthsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentMonthId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForPaymentMonthsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentMonthsDetailsAsync(int paymentMonthsId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForPaymentMonthsDetailsAsync(paymentMonthsId, flatPaymentsId);
            if (HandleResponseError(response, logger, "PaymentMonth", $"PaymentMonthsId: '{paymentMonthsId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentMonthId}/common-expense")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCommonExpensesForPaymentMonthsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentMonthsAsync(int paymentMonthsId)
        {
            var response = await service.GetCommonExpensesForPaymentMonthsAsync(paymentMonthsId);
            if (HandleResponseError(response, logger, "PaymentMonth", $"PaymentMonthsId: '{paymentMonthsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentMonthId}/common-expense/{commonExpenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCommonExpensesForPaymentMonthsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentMonthsDetailsAsync(int paymentMonthsId, int commonExpensesId)
        {
            var response = await service.GetCommonExpensesForPaymentMonthsDetailsAsync(paymentMonthsId, commonExpensesId);
            if (HandleResponseError(response, logger, "PaymentMonth", $"PaymentMonthsId: '{paymentMonthsId}', CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}