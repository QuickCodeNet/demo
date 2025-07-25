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
    public partial class ExpenseInstallmentsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<ExpenseInstallmentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ExpenseInstallmentsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<ExpenseInstallmentsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new ExpenseInstallmentsListQuery(page, size));
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
            var response = await mediator.Send(new ExpenseInstallmentsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseInstallmentsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ExpenseInstallments Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExpenseInstallmentsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ExpenseInstallmentsDto model)
        {
            var response = await mediator.Send(new ExpenseInstallmentsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, ExpenseInstallmentsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new ExpenseInstallmentsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ExpenseInstallments Table";
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
            var response = await mediator.Send(new ExpenseInstallmentsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in ExpenseInstallments Table";
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

        [HttpGet("get-expense-installments/{expenseInstallmentsExpenseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentsGetExpenseInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsAsync(int expenseInstallmentsExpenseId)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetExpenseInstallmentsQuery(expenseInstallmentsExpenseId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ExpenseInstallmentsExpenseId: '{expenseInstallmentsExpenseId}' not found in ExpenseInstallments Table";
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

        [HttpGet("get-unpaid-installments/{expenseInstallmentsExpenseId:int}/{expenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentsGetUnpaidInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidInstallmentsAsync(int expenseInstallmentsExpenseId, bool expenseInstallmentsPaid)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetUnpaidInstallmentsQuery(expenseInstallmentsExpenseId, expenseInstallmentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ExpenseInstallmentsExpenseId: '{expenseInstallmentsExpenseId}', ExpenseInstallmentsPaid: '{expenseInstallmentsPaid}' not found in ExpenseInstallments Table";
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

        [HttpGet("get-overdue-installments/{expenseInstallmentsPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentsGetOverdueInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOverdueInstallmentsAsync(bool expenseInstallmentsPaid)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetOverdueInstallmentsQuery(expenseInstallmentsPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ExpenseInstallmentsPaid: '{expenseInstallmentsPaid}' not found in ExpenseInstallments Table";
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

        [HttpGet("get-apartment-installments/{expenseInstallmentsSiteId:int}/{expenseInstallmentsApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentsGetApartmentInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentInstallmentsAsync(int expenseInstallmentsSiteId, int expenseInstallmentsApartmentId)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetApartmentInstallmentsQuery(expenseInstallmentsSiteId, expenseInstallmentsApartmentId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ExpenseInstallmentsSiteId: '{expenseInstallmentsSiteId}', ExpenseInstallmentsApartmentId: '{expenseInstallmentsApartmentId}' not found in ExpenseInstallments Table";
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

        [HttpGet("get-site-installments/{expenseInstallmentsSiteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentsGetSiteInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteInstallmentsAsync(int expenseInstallmentsSiteId)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetSiteInstallmentsQuery(expenseInstallmentsSiteId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ExpenseInstallmentsSiteId: '{expenseInstallmentsSiteId}' not found in ExpenseInstallments Table";
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

        [HttpGet("{expenseInstallmentsId}/flat-expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForExpenseInstallmentsAsync(int expenseInstallmentsId)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsQuery(expenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ExpenseInstallmentsId: '{expenseInstallmentsId}' not found in ExpenseInstallments Table";
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

        [HttpGet("{expenseInstallmentsId}/flat-expense-installments/{flatExpenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForExpenseInstallmentsDetailsAsync(int expenseInstallmentsId, int flatExpenseInstallmentsId)
        {
            var response = await mediator.Send(new ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsQuery(expenseInstallmentsId, flatExpenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ExpenseInstallmentsId: '{expenseInstallmentsId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}' not found in ExpenseInstallments Table";
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

        [HttpPatch("mark-installment-as-paid/{expenseInstallmentsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkInstallmentAsPaidAsync(int expenseInstallmentsId, [FromBody] ExpenseInstallmentsMarkInstallmentAsPaidRequestDto updateRequest)
        {
            var response = await mediator.Send(new ExpenseInstallmentsMarkInstallmentAsPaidCommand(expenseInstallmentsId, updateRequest));
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