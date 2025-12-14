using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Order;
using QuickCode.Demo.OnlineShopModule.Application.Services.Order;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
{
    public partial class OrdersController : QuickCodeBaseApiController
    {
        private readonly IOrderService service;
        private readonly ILogger<OrdersController> logger;
        private readonly IServiceProvider serviceProvider;
        public OrdersController(IOrderService service, IServiceProvider serviceProvider, ILogger<OrdersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Order", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Order") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Order", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(OrderDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Order") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, OrderDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Order", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Order", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-orders-yearly/{ordersUserId:int}/{ordersTotalAmount:decimal}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOrdersYearlyResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrdersYearlyAsync(int ordersUserId, decimal ordersTotalAmount)
        {
            var response = await service.GetOrdersYearlyAsync(ordersUserId, ordersTotalAmount);
            if (HandleResponseError(response, logger, "Order", $"OrdersUserId: '{ordersUserId}', OrdersTotalAmount: '{ordersTotalAmount}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-orders-by-status-date/{ordersOrderDate:DateTime}/{ordersStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOrdersByStatusDateResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrdersByStatusDateAsync(DateTime ordersOrderDate, OrderStatus ordersStatus)
        {
            var response = await service.GetOrdersByStatusDateAsync(ordersOrderDate, ordersStatus);
            if (HandleResponseError(response, logger, "Order", $"OrdersOrderDate: '{ordersOrderDate}', OrdersStatus: '{ordersStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user-orders/{ordersUserId:int}/{paymentsStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserOrdersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserOrdersAsync(int ordersUserId, PaymentStatus paymentsStatus)
        {
            var response = await service.GetUserOrdersAsync(ordersUserId, paymentsStatus);
            if (HandleResponseError(response, logger, "Order", $"OrdersUserId: '{ordersUserId}', PaymentsStatus: '{paymentsStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{orderId}/order-item")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOrderItemsForOrdersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrderItemsForOrdersAsync(int ordersId)
        {
            var response = await service.GetOrderItemsForOrdersAsync(ordersId);
            if (HandleResponseError(response, logger, "Order", $"OrdersId: '{ordersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{orderId}/order-item/{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderItemsForOrdersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrderItemsForOrdersDetailsAsync(int ordersId, int orderItemsId)
        {
            var response = await service.GetOrderItemsForOrdersDetailsAsync(ordersId, orderItemsId);
            if (HandleResponseError(response, logger, "Order", $"OrdersId: '{ordersId}', OrderItemsId: '{orderItemsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{orderId}/shipment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetShipmentsForOrdersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetShipmentsForOrdersAsync(int ordersId)
        {
            var response = await service.GetShipmentsForOrdersAsync(ordersId);
            if (HandleResponseError(response, logger, "Order", $"OrdersId: '{ordersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{orderId}/shipment/{shipmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetShipmentsForOrdersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetShipmentsForOrdersDetailsAsync(int ordersId, int shipmentsId)
        {
            var response = await service.GetShipmentsForOrdersDetailsAsync(ordersId, shipmentsId);
            if (HandleResponseError(response, logger, "Order", $"OrdersId: '{ordersId}', ShipmentsId: '{shipmentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}