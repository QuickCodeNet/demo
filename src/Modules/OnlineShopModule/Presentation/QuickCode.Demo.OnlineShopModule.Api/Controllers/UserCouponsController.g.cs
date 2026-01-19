using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.UserCoupon;
using QuickCode.Demo.OnlineShopModule.Application.Services.UserCoupon;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
{
    public partial class UserCouponsController : QuickCodeBaseApiController
    {
        private readonly IUserCouponService service;
        private readonly ILogger<UserCouponsController> logger;
        private readonly IServiceProvider serviceProvider;
        public UserCouponsController(IUserCouponService service, IServiceProvider serviceProvider, ILogger<UserCouponsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserCouponDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "UserCoupon", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "UserCoupon") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId:int}/{couponId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserCouponDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int userId, int couponId)
        {
            var response = await service.GetItemAsync(userId, couponId);
            if (HandleResponseError(response, logger, "UserCoupon", $"UserId: '{userId}', CouponId: '{couponId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserCouponDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(UserCouponDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "UserCoupon") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { userId = response.Value.UserId, couponId = response.Value.CouponId }, response.Value);
        }

        [HttpPut("{userId:int}/{couponId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int userId, int couponId, UserCouponDto model)
        {
            if (!(model.UserId == userId && model.CouponId == couponId))
            {
                return BadRequest($"UserId: '{userId}', CouponId: '{couponId}' must be equal to model.UserId: '{model.UserId}', model.CouponId: '{model.CouponId}'");
            }

            var response = await service.UpdateAsync(userId, couponId, model);
            if (HandleResponseError(response, logger, "UserCoupon", $"UserId: '{userId}', CouponId: '{couponId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{userId:int}/{couponId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int userId, int couponId)
        {
            var response = await service.DeleteItemAsync(userId, couponId);
            if (HandleResponseError(response, logger, "UserCoupon", $"UserId: '{userId}', CouponId: '{couponId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-used-coupons/{userCouponsIsUsed:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUsedCouponsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUsedCouponsAsync(bool userCouponsIsUsed)
        {
            var response = await service.GetUsedCouponsAsync(userCouponsIsUsed);
            if (HandleResponseError(response, logger, "UserCoupon", $"UserCouponsIsUsed: '{userCouponsIsUsed}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-used-coupons/{userCouponsIsUsed:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUsedCouponsAsync(bool userCouponsIsUsed, [FromBody] UpdateUsedCouponsRequestDto updateRequest)
        {
            var response = await service.UpdateUsedCouponsAsync(userCouponsIsUsed, updateRequest);
            if (HandleResponseError(response, logger, "UserCoupon", $"UserCouponsIsUsed: '{userCouponsIsUsed}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-all-used-coupons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAllUsedCouponsAsync([FromBody] UpdateAllUsedCouponsRequestDto updateRequest)
        {
            var response = await service.UpdateAllUsedCouponsAsync(updateRequest);
            if (HandleResponseError(response, logger, "UserCoupon", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-all-true-used-coupons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAllTrueUsedCouponsAsync([FromBody] UpdateAllTrueUsedCouponsRequestDto updateRequest)
        {
            var response = await service.UpdateAllTrueUsedCouponsAsync(updateRequest);
            if (HandleResponseError(response, logger, "UserCoupon", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}