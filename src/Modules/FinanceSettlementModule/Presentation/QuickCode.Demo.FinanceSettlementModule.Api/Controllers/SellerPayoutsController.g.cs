using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.SellerPayout;
using QuickCode.Demo.FinanceSettlementModule.Application.Services.SellerPayout;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Api.Controllers
{
    public partial class SellerPayoutsController : QuickCodeBaseApiController
    {
        private readonly ISellerPayoutService service;
        private readonly ILogger<SellerPayoutsController> logger;
        private readonly IServiceProvider serviceProvider;
        public SellerPayoutsController(ISellerPayoutService service, IServiceProvider serviceProvider, ILogger<SellerPayoutsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SellerPayoutDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "SellerPayout", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "SellerPayout") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellerPayoutDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "SellerPayout", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SellerPayoutDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SellerPayoutDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "SellerPayout") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SellerPayoutDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "SellerPayout", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "SellerPayout", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-seller-id/{sellerPayoutSellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetBySellerIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBySellerIdAsync(int sellerPayoutSellerId, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetBySellerIdAsync(sellerPayoutSellerId, page, size);
            if (HandleResponseError(response, logger, "SellerPayout", $"SellerPayoutSellerId: '{sellerPayoutSellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-status/{sellerPayoutStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByStatusResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByStatusAsync(PayoutStatus sellerPayoutStatus, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetByStatusAsync(sellerPayoutStatus, page, size);
            if (HandleResponseError(response, logger, "SellerPayout", $"SellerPayoutStatus: '{sellerPayoutStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-period/{sellerPayoutPayoutPeriodId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByPeriodResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByPeriodAsync(int sellerPayoutPayoutPeriodId, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetByPeriodAsync(sellerPayoutPayoutPeriodId, page, size);
            if (HandleResponseError(response, logger, "SellerPayout", $"SellerPayoutPayoutPeriodId: '{sellerPayoutPayoutPeriodId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-payouts-summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPendingPayoutsSummaryResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingPayoutsSummaryAsync()
        {
            var response = await service.GetPendingPayoutsSummaryAsync();
            if (HandleResponseError(response, logger, "SellerPayout", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("approve/{sellerPayoutId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ApproveAsync(int sellerPayoutId, [FromBody] ApproveRequestDto updateRequest)
        {
            var response = await service.ApproveAsync(sellerPayoutId, updateRequest);
            if (HandleResponseError(response, logger, "SellerPayout", $"SellerPayoutId: '{sellerPayoutId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("mark-as-paid/{sellerPayoutId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkAsPaidAsync(int sellerPayoutId, [FromBody] MarkAsPaidRequestDto updateRequest)
        {
            var response = await service.MarkAsPaidAsync(sellerPayoutId, updateRequest);
            if (HandleResponseError(response, logger, "SellerPayout", $"SellerPayoutId: '{sellerPayoutId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("mark-as-failed/{sellerPayoutId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> MarkAsFailedAsync(int sellerPayoutId, [FromBody] MarkAsFailedRequestDto updateRequest)
        {
            var response = await service.MarkAsFailedAsync(sellerPayoutId, updateRequest);
            if (HandleResponseError(response, logger, "SellerPayout", $"SellerPayoutId: '{sellerPayoutId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}