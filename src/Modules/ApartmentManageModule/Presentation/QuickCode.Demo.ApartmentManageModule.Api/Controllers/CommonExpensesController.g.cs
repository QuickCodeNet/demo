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
    public partial class CommonExpensesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<CommonExpensesController> logger;
        private readonly IServiceProvider serviceProvider;
        public CommonExpensesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<CommonExpensesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new CommonExpensesListQuery(page, size));
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
            var response = await mediator.Send(new CommonExpensesTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonExpensesDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new CommonExpensesGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in CommonExpenses Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommonExpensesDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(CommonExpensesDto model)
        {
            var response = await mediator.Send(new CommonExpensesInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, CommonExpensesDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new CommonExpensesUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in CommonExpenses Table";
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
            var response = await mediator.Send(new CommonExpensesDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in CommonExpenses Table";
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

        [HttpGet("get-expenses-by-apartment-month/{commonExpensesSiteId:int}/{commonExpensesApartmentId:int}/{commonExpensesYearId:int}/{commonExpensesMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetExpensesByApartmentMonthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesByApartmentMonthAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            var response = await mediator.Send(new CommonExpensesGetExpensesByApartmentMonthQuery(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesYearId, commonExpensesMonthId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesSiteId: '{commonExpensesSiteId}', CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesYearId: '{commonExpensesYearId}', CommonExpensesMonthId: '{commonExpensesMonthId}' not found in CommonExpenses Table";
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

        [HttpGet("get-expenses-by-site/{commonExpensesSiteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetExpensesBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesBySiteAsync(int commonExpensesSiteId)
        {
            var response = await mediator.Send(new CommonExpensesGetExpensesBySiteQuery(commonExpensesSiteId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesSiteId: '{commonExpensesSiteId}' not found in CommonExpenses Table";
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

        [HttpGet("get-expenses-summary-by-year/{commonExpensesSiteId:int}/{commonExpensesApartmentId:int}/{commonExpensesYearId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetExpensesSummaryByYearResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesSummaryByYearAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId)
        {
            var response = await mediator.Send(new CommonExpensesGetExpensesSummaryByYearQuery(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesYearId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesSiteId: '{commonExpensesSiteId}', CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesYearId: '{commonExpensesYearId}' not found in CommonExpenses Table";
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

        [HttpGet("get-expenses-by-type/{commonExpensesSiteId:int}/{commonExpensesApartmentId:int}/{commonExpensesExpenseTypeId:int}/{commonExpensesYearId:int}/{commonExpensesMonthId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetExpensesByTypeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesByTypeAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesExpenseTypeId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            var response = await mediator.Send(new CommonExpensesGetExpensesByTypeQuery(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesExpenseTypeId, commonExpensesYearId, commonExpensesMonthId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesSiteId: '{commonExpensesSiteId}', CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesExpenseTypeId: '{commonExpensesExpenseTypeId}', CommonExpensesYearId: '{commonExpensesYearId}', CommonExpensesMonthId: '{commonExpensesMonthId}' not found in CommonExpenses Table";
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

        [HttpGet("get-unpaid-expenses-by-apartment/{commonExpensesApartmentId:int}/{commonExpensesPaid:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetUnpaidExpensesByApartmentResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUnpaidExpensesByApartmentAsync(int commonExpensesApartmentId, bool commonExpensesPaid)
        {
            var response = await mediator.Send(new CommonExpensesGetUnpaidExpensesByApartmentQuery(commonExpensesApartmentId, commonExpensesPaid));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesApartmentId: '{commonExpensesApartmentId}', CommonExpensesPaid: '{commonExpensesPaid}' not found in CommonExpenses Table";
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

        [HttpGet("get-expenses-count-by-apartment/{commonExpensesApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpensesCountByApartmentAsync(int commonExpensesApartmentId)
        {
            var response = await mediator.Send(new CommonExpensesGetExpensesCountByApartmentQuery(commonExpensesApartmentId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesApartmentId: '{commonExpensesApartmentId}' not found in CommonExpenses Table";
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

        [HttpGet("get-total-expense-amount-by-apartment/{commonExpensesApartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonExpensesGetTotalExpenseAmountByApartmentResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTotalExpenseAmountByApartmentAsync(int commonExpensesApartmentId)
        {
            var response = await mediator.Send(new CommonExpensesGetTotalExpenseAmountByApartmentQuery(commonExpensesApartmentId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesApartmentId: '{commonExpensesApartmentId}' not found in CommonExpenses Table";
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

        [HttpGet("{commonExpensesId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForCommonExpensesAsync(int commonExpensesId)
        {
            var response = await mediator.Send(new CommonExpensesGetFlatPaymentsForCommonExpensesQuery(commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesId: '{commonExpensesId}' not found in CommonExpenses Table";
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

        [HttpGet("{commonExpensesId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatPaymentsId)
        {
            var response = await mediator.Send(new CommonExpensesGetFlatPaymentsForCommonExpensesDetailsQuery(commonExpensesId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesId: '{commonExpensesId}', FlatPaymentsId: '{flatPaymentsId}' not found in CommonExpenses Table";
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

        [HttpGet("{commonExpensesId}/expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId)
        {
            var response = await mediator.Send(new CommonExpensesGetExpenseInstallmentsForCommonExpensesQuery(commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesId: '{commonExpensesId}' not found in CommonExpenses Table";
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

        [HttpGet("{commonExpensesId}/expense-installments/{expenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int expenseInstallmentsId)
        {
            var response = await mediator.Send(new CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsQuery(commonExpensesId, expenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesId: '{commonExpensesId}', ExpenseInstallmentsId: '{expenseInstallmentsId}' not found in CommonExpenses Table";
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

        [HttpGet("{commonExpensesId}/flat-expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId)
        {
            var response = await mediator.Send(new CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesQuery(commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesId: '{commonExpensesId}' not found in CommonExpenses Table";
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

        [HttpGet("{commonExpensesId}/flat-expense-installments/{flatExpenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatExpenseInstallmentsId)
        {
            var response = await mediator.Send(new CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsQuery(commonExpensesId, flatExpenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"CommonExpensesId: '{commonExpensesId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}' not found in CommonExpenses Table";
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

        [HttpPatch("mark-expense-as-paid/{commonExpensesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkExpenseAsPaidAsync(int commonExpensesId, [FromBody] CommonExpensesMarkExpenseAsPaidRequestDto updateRequest)
        {
            var response = await mediator.Send(new CommonExpensesMarkExpenseAsPaidCommand(commonExpensesId, updateRequest));
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