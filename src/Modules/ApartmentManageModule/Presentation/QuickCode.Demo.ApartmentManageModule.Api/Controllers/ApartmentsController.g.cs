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
    public partial class ApartmentsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<ApartmentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ApartmentsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<ApartmentsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new ApartmentsListQuery(page, size));
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
            var response = await mediator.Send(new ApartmentsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new ApartmentsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Apartments Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApartmentsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ApartmentsDto model)
        {
            var response = await mediator.Send(new ApartmentsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, ApartmentsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new ApartmentsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Apartments Table";
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
            var response = await mediator.Send(new ApartmentsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Apartments Table";
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

        [HttpGet("get-apartments-by-site/{apartmentsSiteId:int}/{apartmentsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetApartmentsBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentsBySiteAsync(int apartmentsSiteId, bool apartmentsIsActive)
        {
            var response = await mediator.Send(new ApartmentsGetApartmentsBySiteQuery(apartmentsSiteId, apartmentsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsSiteId: '{apartmentsSiteId}', ApartmentsIsActive: '{apartmentsIsActive}' not found in Apartments Table";
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

        [HttpGet("get-active-apartments/{apartmentsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetActiveApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveApartmentsAsync(bool apartmentsIsActive)
        {
            var response = await mediator.Send(new ApartmentsGetActiveApartmentsQuery(apartmentsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsIsActive: '{apartmentsIsActive}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/flats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetFlatsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForApartmentsAsync(int apartmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetFlatsForApartmentsQuery(apartmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/flats/{flatsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentsGetFlatsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsForApartmentsDetailsAsync(int apartmentsId, int flatsId)
        {
            var response = await mediator.Send(new ApartmentsGetFlatsForApartmentsDetailsQuery(apartmentsId, flatsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}', FlatsId: '{flatsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetFlatPaymentsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentsAsync(int apartmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetFlatPaymentsForApartmentsQuery(apartmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentsGetFlatPaymentsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForApartmentsDetailsAsync(int apartmentsId, int flatPaymentsId)
        {
            var response = await mediator.Send(new ApartmentsGetFlatPaymentsForApartmentsDetailsQuery(apartmentsId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}', FlatPaymentsId: '{flatPaymentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/common-expenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetCommonExpensesForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForApartmentsAsync(int apartmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetCommonExpensesForApartmentsQuery(apartmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/common-expenses/{commonExpensesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentsGetCommonExpensesForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCommonExpensesForApartmentsDetailsAsync(int apartmentsId, int commonExpensesId)
        {
            var response = await mediator.Send(new ApartmentsGetCommonExpensesForApartmentsDetailsQuery(apartmentsId, commonExpensesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}', CommonExpensesId: '{commonExpensesId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/apartment-fee-plans")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetApartmentFeePlansForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForApartmentsAsync(int apartmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetApartmentFeePlansForApartmentsQuery(apartmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/apartment-fee-plans/{apartmentFeePlansId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentsGetApartmentFeePlansForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApartmentFeePlansForApartmentsDetailsAsync(int apartmentsId, int apartmentFeePlansId)
        {
            var response = await mediator.Send(new ApartmentsGetApartmentFeePlansForApartmentsDetailsQuery(apartmentsId, apartmentFeePlansId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}', ApartmentFeePlansId: '{apartmentFeePlansId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetExpenseInstallmentsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForApartmentsAsync(int apartmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetExpenseInstallmentsForApartmentsQuery(apartmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/expense-installments/{expenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentsGetExpenseInstallmentsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int expenseInstallmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetExpenseInstallmentsForApartmentsDetailsQuery(apartmentsId, expenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}', ExpenseInstallmentsId: '{expenseInstallmentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/flat-expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForApartmentsAsync(int apartmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetFlatExpenseInstallmentsForApartmentsQuery(apartmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}' not found in Apartments Table";
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

        [HttpGet("{apartmentsId}/flat-expense-installments/{flatExpenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int flatExpenseInstallmentsId)
        {
            var response = await mediator.Send(new ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsQuery(apartmentsId, flatExpenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"ApartmentsId: '{apartmentsId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}' not found in Apartments Table";
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