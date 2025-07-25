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
    public partial class FlatExpenseInstallmentsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<FlatExpenseInstallmentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatExpenseInstallmentsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<FlatExpenseInstallmentsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatExpenseInstallmentsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new FlatExpenseInstallmentsListQuery(page, size));
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
            var response = await mediator.Send(new FlatExpenseInstallmentsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatExpenseInstallmentsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatExpenseInstallments Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatExpenseInstallmentsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatExpenseInstallmentsDto model)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, FlatExpenseInstallmentsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new FlatExpenseInstallmentsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatExpenseInstallments Table";
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
            var response = await mediator.Send(new FlatExpenseInstallmentsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in FlatExpenseInstallments Table";
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

        [HttpGet("get-flat-expense-installments/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsExpenseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatExpenseInstallmentsGetFlatExpenseInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsAsync(int flatExpenseInstallmentsFlatId, int flatExpenseInstallmentsExpenseId)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsGetFlatExpenseInstallmentsQuery(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsExpenseId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsExpenseId: '{flatExpenseInstallmentsExpenseId}' not found in FlatExpenseInstallments Table";
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

        [HttpGet("get-flat-unpaid-installments/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatExpenseInstallmentsGetFlatUnpaidInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatUnpaidInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsGetFlatUnpaidInstallmentsQuery(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsPaid: '{flatExpenseInstallmentsPaid}' not found in FlatExpenseInstallments Table";
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

        [HttpGet("get-flat-overdue-installments/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatExpenseInstallmentsGetFlatOverdueInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatOverdueInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsGetFlatOverdueInstallmentsQuery(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsPaid: '{flatExpenseInstallmentsPaid}' not found in FlatExpenseInstallments Table";
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

        [HttpGet("get-apartment-flat-installments/{flatExpenseInstallmentsSiteId:int}/{flatExpenseInstallmentsApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatExpenseInstallmentsGetApartmentFlatInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFlatInstallmentsAsync(int flatExpenseInstallmentsSiteId, int flatExpenseInstallmentsApartmentId)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsGetApartmentFlatInstallmentsQuery(flatExpenseInstallmentsSiteId, flatExpenseInstallmentsApartmentId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatExpenseInstallmentsSiteId: '{flatExpenseInstallmentsSiteId}', FlatExpenseInstallmentsApartmentId: '{flatExpenseInstallmentsApartmentId}' not found in FlatExpenseInstallments Table";
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

        [HttpGet("get-flat-total-debt/{flatExpenseInstallmentsFlatId:int}/{flatExpenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatExpenseInstallmentsGetFlatTotalDebtResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatTotalDebtAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsGetFlatTotalDebtQuery(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatExpenseInstallmentsFlatId: '{flatExpenseInstallmentsFlatId}', FlatExpenseInstallmentsPaid: '{flatExpenseInstallmentsPaid}' not found in FlatExpenseInstallments Table";
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

        [HttpPatch("mark-flat-installment-as-paid/{flatExpenseInstallmentsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkFlatInstallmentAsPaidAsync(int flatExpenseInstallmentsId, [FromBody] FlatExpenseInstallmentsMarkFlatInstallmentAsPaidRequestDto updateRequest)
        {
            var response = await mediator.Send(new FlatExpenseInstallmentsMarkFlatInstallmentAsPaidCommand(flatExpenseInstallmentsId, updateRequest));
            if (response.Code == 400)
            {
                return BadRequest($"Update Error: Response Code: {response.Code}, Message: {response.Message}");
            }
            else if (response.Code != 0)
            {
                return BadRequest($"Response Code: {response.Code}, Message: {response.Message}");
            }

            return Ok(response.Value);
        }
    }
}