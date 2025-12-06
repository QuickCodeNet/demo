using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Flat;
using QuickCode.Demo.ApartmentManageModule.Application.Services.Flat;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class FlatsController : QuickCodeBaseApiController
    {
        private readonly IFlatService service;
        private readonly ILogger<FlatsController> logger;
        private readonly IServiceProvider serviceProvider;
        public FlatsController(IFlatService service, IServiceProvider serviceProvider, ILogger<FlatsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FlatDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Flat", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Flat") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FlatDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Flat", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FlatDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(FlatDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Flat") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, FlatDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Flat", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Flat", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flats-by-apartment/{flatsApartmentId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatsByApartmentResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsByApartmentAsync(int flatsApartmentId, bool flatsIsActive)
        {
            var response = await service.GetFlatsByApartmentAsync(flatsApartmentId, flatsIsActive);
            if (HandleResponseError(response, logger, "Flat", $"FlatsApartmentId: '{flatsApartmentId}', FlatsIsActive: '{flatsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flats-by-site/{flatsSiteId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatsBySiteResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsBySiteAsync(int flatsSiteId, bool flatsIsActive)
        {
            var response = await service.GetFlatsBySiteAsync(flatsSiteId, flatsIsActive);
            if (HandleResponseError(response, logger, "Flat", $"FlatsSiteId: '{flatsSiteId}', FlatsIsActive: '{flatsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flats-with-contacts/{flatsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatsWithContactsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsWithContactsAsync(int flatsId)
        {
            var response = await service.GetFlatsWithContactsAsync(flatsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-vacant-flats/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetVacantFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetVacantFlatsAsync(bool flatsIsActive)
        {
            var response = await service.GetVacantFlatsAsync(flatsIsActive);
            if (HandleResponseError(response, logger, "Flat", $"FlatsIsActive: '{flatsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-rented-flats/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetRentedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetRentedFlatsAsync(bool flatsIsActive)
        {
            var response = await service.GetRentedFlatsAsync(flatsIsActive);
            if (HandleResponseError(response, logger, "Flat", $"FlatsIsActive: '{flatsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flat-by-number/{flatsSiteId:int}/{flatsFlatNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatByNumberResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatByNumberAsync(int flatsSiteId, string flatsFlatNumber)
        {
            var response = await service.GetFlatByNumberAsync(flatsSiteId, flatsFlatNumber);
            if (HandleResponseError(response, logger, "Flat", $"FlatsSiteId: '{flatsSiteId}', FlatsFlatNumber: '{flatsFlatNumber}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-owned-flats/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOwnedFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOwnedFlatsAsync(bool flatsIsActive)
        {
            var response = await service.GetOwnedFlatsAsync(flatsIsActive);
            if (HandleResponseError(response, logger, "Flat", $"FlatsIsActive: '{flatsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flats-count-by-site/{flatsSiteId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsCountBySiteAsync(int flatsSiteId, bool flatsIsActive)
        {
            var response = await service.GetFlatsCountBySiteAsync(flatsSiteId, flatsIsActive);
            if (HandleResponseError(response, logger, "Flat", $"FlatsSiteId: '{flatsSiteId}', FlatsIsActive: '{flatsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-flats-count-by-apartment/{flatsApartmentId:int}/{flatsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatsCountByApartmentAsync(int flatsApartmentId, bool flatsIsActive)
        {
            var response = await service.GetFlatsCountByApartmentAsync(flatsApartmentId, flatsIsActive);
            if (HandleResponseError(response, logger, "Flat", $"FlatsApartmentId: '{flatsApartmentId}', FlatsIsActive: '{flatsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/flat-contact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatContactsForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsForFlatsAsync(int flatsId)
        {
            var response = await service.GetFlatContactsForFlatsAsync(flatsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/flat-contact/{flatContactId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatContactsForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsForFlatsDetailsAsync(int flatsId, int flatContactsId)
        {
            var response = await service.GetFlatContactsForFlatsDetailsAsync(flatsId, flatContactsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}', FlatContactsId: '{flatContactsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/flat-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatPaymentsForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForFlatsAsync(int flatsId)
        {
            var response = await service.GetFlatPaymentsForFlatsAsync(flatsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/flat-payment/{flatPaymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatPaymentsForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatPaymentsForFlatsDetailsAsync(int flatsId, int flatPaymentsId)
        {
            var response = await service.GetFlatPaymentsForFlatsDetailsAsync(flatsId, flatPaymentsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}', FlatPaymentsId: '{flatPaymentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/flat-expense-installment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatExpenseInstallmentsForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForFlatsAsync(int flatsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForFlatsAsync(flatsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/flat-expense-installment/{flatExpenseInstallmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatExpenseInstallmentsForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatExpenseInstallmentsForFlatsDetailsAsync(int flatsId, int flatExpenseInstallmentsId)
        {
            var response = await service.GetFlatExpenseInstallmentsForFlatsDetailsAsync(flatsId, flatExpenseInstallmentsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}', FlatExpenseInstallmentsId: '{flatExpenseInstallmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/user-site-access")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserSiteAccessesForFlatsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForFlatsAsync(int flatsId)
        {
            var response = await service.GetUserSiteAccessesForFlatsAsync(flatsId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{flatId}/user-site-access/{userSiteAccessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserSiteAccessesForFlatsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserSiteAccessesForFlatsDetailsAsync(int flatsId, int userSiteAccessesId)
        {
            var response = await service.GetUserSiteAccessesForFlatsDetailsAsync(flatsId, userSiteAccessesId);
            if (HandleResponseError(response, logger, "Flat", $"FlatsId: '{flatsId}', UserSiteAccessesId: '{userSiteAccessesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}