using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.ReturnRequest;
using QuickCode.Demo.OrderManagementModule.Application.Services.ReturnRequest;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Api.Controllers
{
    public partial class ReturnRequestsController : QuickCodeBaseApiController
    {
        private readonly IReturnRequestService service;
        private readonly ILogger<ReturnRequestsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ReturnRequestsController(IReturnRequestService service, IServiceProvider serviceProvider, ILogger<ReturnRequestsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReturnRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "ReturnRequest", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "ReturnRequest") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "ReturnRequest", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReturnRequestDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ReturnRequestDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "ReturnRequest") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ReturnRequestDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "ReturnRequest", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "ReturnRequest", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-order-id/{returnRequestOrderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByOrderIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByOrderIdAsync(int returnRequestOrderId, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetByOrderIdAsync(returnRequestOrderId, page, size);
            if (HandleResponseError(response, logger, "ReturnRequest", $"ReturnRequestOrderId: '{returnRequestOrderId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-returns-by-seller/{ordersSellerId:int}/{returnRequestsStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPendingReturnsBySellerResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingReturnsBySellerAsync(int ordersSellerId, ReturnStatus returnRequestsStatus, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetPendingReturnsBySellerAsync(ordersSellerId, returnRequestsStatus, page, size);
            if (HandleResponseError(response, logger, "ReturnRequest", $"OrdersSellerId: '{ordersSellerId}', ReturnRequestsStatus: '{returnRequestsStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("approve/{returnRequestId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ApproveAsync(int returnRequestId, [FromBody] ApproveRequestDto updateRequest)
        {
            var response = await service.ApproveAsync(returnRequestId, updateRequest);
            if (HandleResponseError(response, logger, "ReturnRequest", $"ReturnRequestId: '{returnRequestId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("reject/{returnRequestId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RejectAsync(int returnRequestId, [FromBody] RejectRequestDto updateRequest)
        {
            var response = await service.RejectAsync(returnRequestId, updateRequest);
            if (HandleResponseError(response, logger, "ReturnRequest", $"ReturnRequestId: '{returnRequestId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("complete/{returnRequestId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CompleteAsync(int returnRequestId, [FromBody] CompleteRequestDto updateRequest)
        {
            var response = await service.CompleteAsync(returnRequestId, updateRequest);
            if (HandleResponseError(response, logger, "ReturnRequest", $"ReturnRequestId: '{returnRequestId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}