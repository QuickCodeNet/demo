using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.PaymentYear;
using QuickCode.Demo.ApartmentManageModule.Application.Services.PaymentYear;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class PaymentYearsController : QuickCodeBaseApiController
    {
        private readonly IPaymentYearService service;
        private readonly ILogger<PaymentYearsController> logger;
        private readonly IServiceProvider serviceProvider;
        public PaymentYearsController(IPaymentYearService service, IServiceProvider serviceProvider, ILogger<PaymentYearsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentYearDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "PaymentYear", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "PaymentYear") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentYearDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "PaymentYear", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentYearDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(PaymentYearDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "PaymentYear") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, PaymentYearDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "PaymentYear", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "PaymentYear", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-all-years")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAllYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAllYearsAsync()
        {
            var response = await service.GetAllYearsAsync();
            if (HandleResponseError(response, logger, "PaymentYear", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentYearId}/apartment-fee-plan")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentFeePlansForPaymentYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentYearsAsync(int paymentYearsId)
        {
            var response = await service.GetApartmentFeePlansForPaymentYearsAsync(paymentYearsId);
            if (HandleResponseError(response, logger, "PaymentYear", $"PaymentYearsId: '{paymentYearsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentYearId}/apartment-fee-plan/{apartmentFeePlanId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApartmentFeePlansForPaymentYearsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForPaymentYearsDetailsAsync(int paymentYearsId, int apartmentFeePlansId)
        {
            var response = await service.GetApartmentFeePlansForPaymentYearsDetailsAsync(paymentYearsId, apartmentFeePlansId);
            if (HandleResponseError(response, logger, "PaymentYear", $"PaymentYearsId: '{paymentYearsId}', ApartmentFeePlansId: '{apartmentFeePlansId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentYearId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForPaymentYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentYearsAsync(int paymentYearsId)
        {
            var response = await service.GetFlatPaymentsForPaymentYearsAsync(paymentYearsId);
            if (HandleResponseError(response, logger, "PaymentYear", $"PaymentYearsId: '{paymentYearsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentYearId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForPaymentYearsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForPaymentYearsDetailsAsync(int paymentYearsId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForPaymentYearsDetailsAsync(paymentYearsId, flatPaymentsId);
            if (HandleResponseError(response, logger, "PaymentYear", $"PaymentYearsId: '{paymentYearsId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentYearId}/common-expense")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCommonExpensesForPaymentYearsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentYearsAsync(int paymentYearsId)
        {
            var response = await service.GetCommonExpensesForPaymentYearsAsync(paymentYearsId);
            if (HandleResponseError(response, logger, "PaymentYear", $"PaymentYearsId: '{paymentYearsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{paymentYearId}/common-expense/{commonExpenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCommonExpensesForPaymentYearsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForPaymentYearsDetailsAsync(int paymentYearsId, int commonExpensesId)
        {
            var response = await service.GetCommonExpensesForPaymentYearsDetailsAsync(paymentYearsId, commonExpensesId);
            if (HandleResponseError(response, logger, "PaymentYear", $"PaymentYearsId: '{paymentYearsId}', CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}