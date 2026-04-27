using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.Seller;
using QuickCode.Demo.SellerManagementModule.Application.Services.Seller;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Api.Controllers
{
    public partial class SellersController : QuickCodeBaseApiController
    {
        private readonly ISellerService service;
        private readonly ILogger<SellersController> logger;
        private readonly IServiceProvider serviceProvider;
        public SellersController(ISellerService service, IServiceProvider serviceProvider, ILogger<SellersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SellerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Seller", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Seller") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Seller", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SellerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SellerDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Seller") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, SellerDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Seller", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Seller", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-user-id/{sellerUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByUserIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByUserIdAsync(int sellerUserId)
        {
            var response = await service.GetByUserIdAsync(sellerUserId);
            if (HandleResponseError(response, logger, "Seller", $"SellerUserId: '{sellerUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-status/{sellerStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByStatusResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByStatusAsync(SellerStatus sellerStatus, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetByStatusAsync(sellerStatus, page, size);
            if (HandleResponseError(response, logger, "Seller", $"SellerStatus: '{sellerStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("search-by-company-name/{sellerCompanyName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchByCompanyNameResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SearchByCompanyNameAsync(string sellerCompanyName, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.SearchByCompanyNameAsync(sellerCompanyName, page, size);
            if (HandleResponseError(response, logger, "Seller", $"SellerCompanyName: '{sellerCompanyName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-verification-count/{sellerStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingVerificationCountAsync(SellerStatus sellerStatus)
        {
            var response = await service.GetPendingVerificationCountAsync(sellerStatus);
            if (HandleResponseError(response, logger, "Seller", $"SellerStatus: '{sellerStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("approve/{sellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ApproveAsync(int sellerId, [FromBody] ApproveRequestDto updateRequest)
        {
            var response = await service.ApproveAsync(sellerId, updateRequest);
            if (HandleResponseError(response, logger, "Seller", $"SellerId: '{sellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("suspend/{sellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SuspendAsync(int sellerId, [FromBody] SuspendRequestDto updateRequest)
        {
            var response = await service.SuspendAsync(sellerId, updateRequest);
            if (HandleResponseError(response, logger, "Seller", $"SellerId: '{sellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("reject/{sellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RejectAsync(int sellerId, [FromBody] RejectRequestDto updateRequest)
        {
            var response = await service.RejectAsync(sellerId, updateRequest);
            if (HandleResponseError(response, logger, "Seller", $"SellerId: '{sellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-tier/{sellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateTierAsync(int sellerId, [FromBody] UpdateTierRequestDto updateRequest)
        {
            var response = await service.UpdateTierAsync(sellerId, updateRequest);
            if (HandleResponseError(response, logger, "Seller", $"SellerId: '{sellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}