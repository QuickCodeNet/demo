using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ShippingInfo;
using QuickCode.Demo.OnlineShopModule.Application.Services.ShippingInfo;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
{
    public partial class ShippingInfosController : QuickCodeBaseApiController
    {
        private readonly IShippingInfoService service;
        private readonly ILogger<ShippingInfosController> logger;
        private readonly IServiceProvider serviceProvider;
        public ShippingInfosController(IShippingInfoService service, IServiceProvider serviceProvider, ILogger<ShippingInfosController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ShippingInfoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "ShippingInfo", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "ShippingInfo") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShippingInfoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "ShippingInfo", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ShippingInfoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ShippingInfoDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "ShippingInfo") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ShippingInfoDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "ShippingInfo", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "ShippingInfo", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user-shipping-info/{shippingInfosUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserShippingInfoResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserShippingInfoAsync(int shippingInfosUserId)
        {
            var response = await service.GetUserShippingInfoAsync(shippingInfosUserId);
            if (HandleResponseError(response, logger, "ShippingInfo", $"ShippingInfosUserId: '{shippingInfosUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{shippingInfoId}/order")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOrdersForShippingInfosResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrdersForShippingInfosAsync(int shippingInfosId)
        {
            var response = await service.GetOrdersForShippingInfosAsync(shippingInfosId);
            if (HandleResponseError(response, logger, "ShippingInfo", $"ShippingInfosId: '{shippingInfosId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{shippingInfoId}/order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersForShippingInfosResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrdersForShippingInfosDetailsAsync(int shippingInfosId, int ordersId)
        {
            var response = await service.GetOrdersForShippingInfosDetailsAsync(shippingInfosId, ordersId);
            if (HandleResponseError(response, logger, "ShippingInfo", $"ShippingInfosId: '{shippingInfosId}', OrdersId: '{ordersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("set-default-shipping/{shippingInfosUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SetDefaultShippingAsync(int shippingInfosUserId, [FromBody] SetDefaultShippingRequestDto updateRequest)
        {
            var response = await service.SetDefaultShippingAsync(shippingInfosUserId, updateRequest);
            if (HandleResponseError(response, logger, "ShippingInfo", $"ShippingInfosUserId: '{shippingInfosUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("set-default-shipping-address/{shippingInfosId:int}/{shippingInfosUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SetDefaultShippingAddressAsync(int shippingInfosId, int shippingInfosUserId, [FromBody] SetDefaultShippingAddressRequestDto updateRequest)
        {
            var response = await service.SetDefaultShippingAddressAsync(shippingInfosId, shippingInfosUserId, updateRequest);
            if (HandleResponseError(response, logger, "ShippingInfo", $"ShippingInfosId: '{shippingInfosId}', ShippingInfosUserId: '{shippingInfosUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}