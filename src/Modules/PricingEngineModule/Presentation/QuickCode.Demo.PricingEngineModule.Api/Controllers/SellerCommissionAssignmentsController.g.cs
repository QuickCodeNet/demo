using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.SellerCommissionAssignment;
using QuickCode.Demo.PricingEngineModule.Application.Services.SellerCommissionAssignment;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Api.Controllers
{
    public partial class SellerCommissionAssignmentsController : QuickCodeBaseApiController
    {
        private readonly ISellerCommissionAssignmentService service;
        private readonly ILogger<SellerCommissionAssignmentsController> logger;
        private readonly IServiceProvider serviceProvider;
        public SellerCommissionAssignmentsController(ISellerCommissionAssignmentService service, IServiceProvider serviceProvider, ILogger<SellerCommissionAssignmentsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SellerCommissionAssignmentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "SellerCommissionAssignment") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{sellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellerCommissionAssignmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int sellerId)
        {
            var response = await service.GetItemAsync(sellerId);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment", $"SellerId: '{sellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SellerCommissionAssignmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(SellerCommissionAssignmentDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { sellerId = response.Value.SellerId }, response.Value);
        }

        [HttpPut("{sellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int sellerId, SellerCommissionAssignmentDto model)
        {
            if (!(model.SellerId == sellerId))
            {
                return BadRequest($"SellerId: '{sellerId}' must be equal to model.SellerId: '{model.SellerId}'");
            }

            var response = await service.UpdateAsync(sellerId, model);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment", $"SellerId: '{sellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{sellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int sellerId)
        {
            var response = await service.DeleteItemAsync(sellerId);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment", $"SellerId: '{sellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-seller-id/{sellerCommissionAssignmentSellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBySellerIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBySellerIdAsync(int sellerCommissionAssignmentSellerId)
        {
            var response = await service.GetBySellerIdAsync(sellerCommissionAssignmentSellerId);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment", $"SellerCommissionAssignmentSellerId: '{sellerCommissionAssignmentSellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-model-id/{sellerCommissionAssignmentCommissionModelId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByModelIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByModelIdAsync(int sellerCommissionAssignmentCommissionModelId, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetByModelIdAsync(sellerCommissionAssignmentCommissionModelId, page, size);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment", $"SellerCommissionAssignmentCommissionModelId: '{sellerCommissionAssignmentCommissionModelId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("remove-assignment/{sellerCommissionAssignmentSellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RemoveAssignmentAsync(int sellerCommissionAssignmentSellerId)
        {
            var response = await service.RemoveAssignmentAsync(sellerCommissionAssignmentSellerId);
            if (HandleResponseError(response, logger, "SellerCommissionAssignment", $"SellerCommissionAssignmentSellerId: '{sellerCommissionAssignmentSellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}