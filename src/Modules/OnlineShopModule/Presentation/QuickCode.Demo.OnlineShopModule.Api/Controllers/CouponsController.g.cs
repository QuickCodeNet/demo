using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Coupon;
using QuickCode.Demo.OnlineShopModule.Application.Services.Coupon;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
{
    public partial class CouponsController : QuickCodeBaseApiController
    {
        private readonly ICouponService service;
        private readonly ILogger<CouponsController> logger;
        private readonly IServiceProvider serviceProvider;
        public CouponsController(ICouponService service, IServiceProvider serviceProvider, ILogger<CouponsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CouponDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Coupon", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Coupon") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CouponDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Coupon", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CouponDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(CouponDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Coupon") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, CouponDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Coupon", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Coupon", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-valid-coupons/{couponsCouponCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetValidCouponsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetValidCouponsAsync(string couponsCouponCode)
        {
            var response = await service.GetValidCouponsAsync(couponsCouponCode);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsCouponCode: '{couponsCouponCode}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user-coupons/{couponsUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserCouponsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserCouponsAsync(int couponsUserId)
        {
            var response = await service.GetUserCouponsAsync(couponsUserId);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsUserId: '{couponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{couponId}/user-coupon")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetUserCouponsForCouponsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserCouponsForCouponsAsync(int couponsId)
        {
            var response = await service.GetUserCouponsForCouponsAsync(couponsId);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsId: '{couponsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{couponId}/user-coupon/{userCouponUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserCouponsForCouponsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserCouponsForCouponsDetailsAsync(int couponsId, int userCouponsUserId)
        {
            var response = await service.GetUserCouponsForCouponsDetailsAsync(couponsId, userCouponsUserId);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsId: '{couponsId}', UserCouponsUserId: '{userCouponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-user-coupons/{couponsUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserCouponsAsync(int couponsUserId, [FromBody] UpdateUserCouponsRequestDto updateRequest)
        {
            var response = await service.UpdateUserCouponsAsync(couponsUserId, updateRequest);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsUserId: '{couponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-user-coupons-prm/{couponsUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserCouponsPrmAsync(int couponsUserId, [FromBody] UpdateUserCouponsPrmRequestDto updateRequest)
        {
            var response = await service.UpdateUserCouponsPrmAsync(couponsUserId, updateRequest);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsUserId: '{couponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-user-coupons-single/{couponsUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserCouponsSingleAsync(int couponsUserId, [FromBody] UpdateUserCouponsSingleRequestDto updateRequest)
        {
            var response = await service.UpdateUserCouponsSingleAsync(couponsUserId, updateRequest);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsUserId: '{couponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-user-coupons-where-prm/{couponsUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserCouponsWherePrmAsync(int couponsUserId, [FromBody] UpdateUserCouponsWherePrmRequestDto updateRequest)
        {
            var response = await service.UpdateUserCouponsWherePrmAsync(couponsUserId, updateRequest);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsUserId: '{couponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-user-coupons-ref-col/{couponsUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserCouponsRefColAsync(int couponsUserId, [FromBody] UpdateUserCouponsRefColRequestDto updateRequest)
        {
            var response = await service.UpdateUserCouponsRefColAsync(couponsUserId, updateRequest);
            if (HandleResponseError(response, logger, "Coupon", $"CouponsUserId: '{couponsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}