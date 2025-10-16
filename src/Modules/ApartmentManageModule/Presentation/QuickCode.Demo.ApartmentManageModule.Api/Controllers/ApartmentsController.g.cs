using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Apartment;
using QuickCode.Demo.ApartmentManageModule.Application.Services.Apartment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class ApartmentsController : QuickCodeBaseApiController
    {
        private readonly IApartmentService service;
        private readonly ILogger<ApartmentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ApartmentsController(IApartmentService service, IServiceProvider serviceProvider, ILogger<ApartmentsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Apartment", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Apartment") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Apartment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApartmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApartmentDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Apartment") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ApartmentDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Apartment", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Apartment", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-apartments-by-site/{apartmentsSiteId:int}/{apartmentsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentsBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentsBySiteAsync(int apartmentsSiteId, bool apartmentsIsActive)
        {
            var response = await service.GetApartmentsBySiteAsync(apartmentsSiteId, apartmentsIsActive);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsSiteId: '{apartmentsSiteId}', ApartmentsIsActive: '{apartmentsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-apartments/{apartmentsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveApartmentsAsync(bool apartmentsIsActive)
        {
            var response = await service.GetActiveApartmentsAsync(apartmentsIsActive);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsIsActive: '{apartmentsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/flat")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForApartmentsAsync(int apartmentsId)
        {
            var response = await service.GetFlatsForApartmentsAsync(apartmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/flat/{flatId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForApartmentsDetailsAsync(int apartmentsId, int flatsId)
        {
            var response = await service.GetFlatsForApartmentsDetailsAsync(apartmentsId, flatsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}', FlatsId: '{flatsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentsAsync(int apartmentsId)
        {
            var response = await service.GetFlatPaymentsForApartmentsAsync(apartmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentsDetailsAsync(int apartmentsId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForApartmentsDetailsAsync(apartmentsId, flatPaymentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/common-expense")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCommonExpensesForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForApartmentsAsync(int apartmentsId)
        {
            var response = await service.GetCommonExpensesForApartmentsAsync(apartmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/common-expense/{commonExpenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCommonExpensesForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForApartmentsDetailsAsync(int apartmentsId, int commonExpensesId)
        {
            var response = await service.GetCommonExpensesForApartmentsDetailsAsync(apartmentsId, commonExpensesId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}', CommonExpensesId: '{commonExpensesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/apartment-fee-plan")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetApartmentFeePlansForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForApartmentsAsync(int apartmentsId)
        {
            var response = await service.GetApartmentFeePlansForApartmentsAsync(apartmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/apartment-fee-plan/{apartmentFeePlanId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetApartmentFeePlansForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForApartmentsDetailsAsync(int apartmentsId, int apartmentFeePlansId)
        {
            var response = await service.GetApartmentFeePlansForApartmentsDetailsAsync(apartmentsId, apartmentFeePlansId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}', ApartmentFeePlansId: '{apartmentFeePlansId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetExpenseInstallmentsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForApartmentsAsync(int apartmentsId)
        {
            var response = await service.GetExpenseInstallmentsForApartmentsAsync(apartmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/expense-installment/{expenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetExpenseInstallmentsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int expenseInstallmentsId)
        {
            var response = await service.GetExpenseInstallmentsForApartmentsDetailsAsync(apartmentsId, expenseInstallmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}', ExpenseInstallmentsId: '{expenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/flat-expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatExpenseInstallmentsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForApartmentsAsync(int apartmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForApartmentsAsync(apartmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{apartmentId}/flat-expense-installment/{flatExpenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatExpenseInstallmentsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int flatExpenseInstallmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForApartmentsDetailsAsync(apartmentsId, flatExpenseInstallmentsId);
            if (HandleResponseError(response, logger, "Apartment", $"ApartmentsId: '{apartmentsId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}