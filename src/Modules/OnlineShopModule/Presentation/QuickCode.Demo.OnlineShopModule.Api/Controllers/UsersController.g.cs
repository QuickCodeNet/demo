using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.User;
using QuickCode.Demo.OnlineShopModule.Application.Services.User;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
{
    public partial class UsersController : QuickCodeBaseApiController
    {
        private readonly IUserService service;
        private readonly ILogger<UsersController> logger;
        private readonly IServiceProvider serviceProvider;
        public UsersController(IUserService service, IServiceProvider serviceProvider, ILogger<UsersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "User", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "User") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "User", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(UserDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "User") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, UserDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "User", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "User", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-new-users/{usersIsNew:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetNewUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetNewUsersAsync(bool usersIsNew)
        {
            var response = await service.GetNewUsersAsync(usersIsNew);
            if (HandleResponseError(response, logger, "User", $"UsersIsNew: '{usersIsNew}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/coupon")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCouponsForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCouponsForUsersAsync(int usersId)
        {
            var response = await service.GetCouponsForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/coupon/{couponId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCouponsForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCouponsForUsersDetailsAsync(int usersId, int couponsId)
        {
            var response = await service.GetCouponsForUsersDetailsAsync(usersId, couponsId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', CouponsId: '{couponsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/user-coupon")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserCouponsForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserCouponsForUsersAsync(int usersId)
        {
            var response = await service.GetUserCouponsForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/user-coupon/{userCouponUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserCouponsForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserCouponsForUsersDetailsAsync(int usersId, int userCouponsUserId)
        {
            var response = await service.GetUserCouponsForUsersDetailsAsync(usersId, userCouponsUserId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', UserCouponsUserId: '{userCouponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/product-review")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProductReviewsForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductReviewsForUsersAsync(int usersId)
        {
            var response = await service.GetProductReviewsForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/product-review/{productReviewId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductReviewsForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductReviewsForUsersDetailsAsync(int usersId, int productReviewsId)
        {
            var response = await service.GetProductReviewsForUsersDetailsAsync(usersId, productReviewsId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', ProductReviewsId: '{productReviewsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/cart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCartsForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCartsForUsersAsync(int usersId)
        {
            var response = await service.GetCartsForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/cart/{cartId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCartsForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCartsForUsersDetailsAsync(int usersId, int cartsId)
        {
            var response = await service.GetCartsForUsersDetailsAsync(usersId, cartsId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', CartsId: '{cartsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/order")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOrdersForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrdersForUsersAsync(int usersId)
        {
            var response = await service.GetOrdersForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrdersForUsersDetailsAsync(int usersId, int ordersId)
        {
            var response = await service.GetOrdersForUsersDetailsAsync(usersId, ordersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', OrdersId: '{ordersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}