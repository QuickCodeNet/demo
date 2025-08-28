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
    public partial class FlatsController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<FlatsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatsController(IMediator mediator, IServiceProvider serviceProvider, ILogger<FlatsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new FlatsListQuery(page, size));
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
            var response = await mediator.Send(new FlatsTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await mediator.Send(new FlatsGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Flats Table";
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatsDto model)
        {
            var response = await mediator.Send(new FlatsInsertCommand(model));
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
        public async Task<IActionResult> UpdateAsync(int id, FlatsDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new FlatsUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Flats Table";
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
            var response = await mediator.Send(new FlatsDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in Flats Table";
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

        [HttpGet("get-flats-by-apartment/{flatsApartmentId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetFlatsByApartmentResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsByApartmentAsync(int flatsApartmentId, bool flatsIsActive)
        {
            var response = await mediator.Send(new FlatsGetFlatsByApartmentQuery(flatsApartmentId, flatsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsApartmentId: '{flatsApartmentId}', FlatsIsActive: '{flatsIsActive}' not found in Flats Table";
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

        [HttpGet("get-flats-by-site/{flatsSiteId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetFlatsBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsBySiteAsync(int flatsSiteId, bool flatsIsActive)
        {
            var response = await mediator.Send(new FlatsGetFlatsBySiteQuery(flatsSiteId, flatsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsSiteId: '{flatsSiteId}', FlatsIsActive: '{flatsIsActive}' not found in Flats Table";
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

        [HttpGet("get-flats-with-contacts/{flatsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetFlatsWithContactsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsWithContactsAsync(int flatsId)
        {
            var response = await mediator.Send(new FlatsGetFlatsWithContactsQuery(flatsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}' not found in Flats Table";
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

        [HttpGet("get-vacant-flats/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetVacantFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetVacantFlatsAsync(bool flatsIsActive)
        {
            var response = await mediator.Send(new FlatsGetVacantFlatsQuery(flatsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsIsActive: '{flatsIsActive}' not found in Flats Table";
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

        [HttpGet("get-rented-flats/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetRentedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetRentedFlatsAsync(bool flatsIsActive)
        {
            var response = await mediator.Send(new FlatsGetRentedFlatsQuery(flatsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsIsActive: '{flatsIsActive}' not found in Flats Table";
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

        [HttpGet("get-flat-by-number/{flatsSiteId:int}/{flatsFlatNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatsGetFlatByNumberResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatByNumberAsync(int flatsSiteId, string flatsFlatNumber)
        {
            var response = await mediator.Send(new FlatsGetFlatByNumberQuery(flatsSiteId, flatsFlatNumber));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsSiteId: '{flatsSiteId}', FlatsFlatNumber: '{flatsFlatNumber}' not found in Flats Table";
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

        [HttpGet("get-owned-flats/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetOwnedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOwnedFlatsAsync(bool flatsIsActive)
        {
            var response = await mediator.Send(new FlatsGetOwnedFlatsQuery(flatsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsIsActive: '{flatsIsActive}' not found in Flats Table";
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

        [HttpGet("get-flats-count-by-site/{flatsSiteId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsCountBySiteAsync(int flatsSiteId, bool flatsIsActive)
        {
            var response = await mediator.Send(new FlatsGetFlatsCountBySiteQuery(flatsSiteId, flatsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsSiteId: '{flatsSiteId}', FlatsIsActive: '{flatsIsActive}' not found in Flats Table";
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

        [HttpGet("get-flats-count-by-apartment/{flatsApartmentId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsCountByApartmentAsync(int flatsApartmentId, bool flatsIsActive)
        {
            var response = await mediator.Send(new FlatsGetFlatsCountByApartmentQuery(flatsApartmentId, flatsIsActive));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsApartmentId: '{flatsApartmentId}', FlatsIsActive: '{flatsIsActive}' not found in Flats Table";
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

        [HttpGet("{flatsId}/flat-contacts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetFlatContactsForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsForFlatsAsync(int flatsId)
        {
            var response = await mediator.Send(new FlatsGetFlatContactsForFlatsQuery(flatsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}' not found in Flats Table";
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

        [HttpGet("{flatsId}/flat-contacts/{flatContactsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatsGetFlatContactsForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsForFlatsDetailsAsync(int flatsId, int flatContactsId)
        {
            var response = await mediator.Send(new FlatsGetFlatContactsForFlatsDetailsQuery(flatsId, flatContactsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}', FlatContactsId: '{flatContactsId}' not found in Flats Table";
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

        [HttpGet("{flatsId}/flat-payments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetFlatPaymentsForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForFlatsAsync(int flatsId)
        {
            var response = await mediator.Send(new FlatsGetFlatPaymentsForFlatsQuery(flatsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}' not found in Flats Table";
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

        [HttpGet("{flatsId}/flat-payments/{flatPaymentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatsGetFlatPaymentsForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForFlatsDetailsAsync(int flatsId, int flatPaymentsId)
        {
            var response = await mediator.Send(new FlatsGetFlatPaymentsForFlatsDetailsQuery(flatsId, flatPaymentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}', FlatPaymentsId: '{flatPaymentsId}' not found in Flats Table";
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

        [HttpGet("{flatsId}/flat-expense-installments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetFlatExpenseInstallmentsForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForFlatsAsync(int flatsId)
        {
            var response = await mediator.Send(new FlatsGetFlatExpenseInstallmentsForFlatsQuery(flatsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}' not found in Flats Table";
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

        [HttpGet("{flatsId}/flat-expense-installments/{flatExpenseInstallmentsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatsGetFlatExpenseInstallmentsForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForFlatsDetailsAsync(int flatsId, int flatExpenseInstallmentsId)
        {
            var response = await mediator.Send(new FlatsGetFlatExpenseInstallmentsForFlatsDetailsQuery(flatsId, flatExpenseInstallmentsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}' not found in Flats Table";
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

        [HttpGet("{flatsId}/user-site-accesses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatsGetUserSiteAccessesForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForFlatsAsync(int flatsId)
        {
            var response = await mediator.Send(new FlatsGetUserSiteAccessesForFlatsQuery(flatsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}' not found in Flats Table";
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

        [HttpGet("{flatsId}/user-site-accesses/{userSiteAccessesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatsGetUserSiteAccessesForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForFlatsDetailsAsync(int flatsId, int userSiteAccessesId)
        {
            var response = await mediator.Send(new FlatsGetUserSiteAccessesForFlatsDetailsQuery(flatsId, userSiteAccessesId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"FlatsId: '{flatsId}', UserSiteAccessesId: '{userSiteAccessesId}' not found in Flats Table";
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