using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Coupon;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Coupon
{
    public partial interface ICouponService
    {
        Task<Response<CouponDto>> InsertAsync(CouponDto request);
        Task<Response<bool>> DeleteAsync(CouponDto request);
        Task<Response<bool>> UpdateAsync(int id, CouponDto request);
        Task<Response<List<CouponDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CouponDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetValidCouponsResponseDto>>> GetValidCouponsAsync(string couponsCouponCode);
        Task<Response<List<GetUserCouponsResponseDto>>> GetUserCouponsAsync(int couponsUserId);
        Task<Response<List<GetUserCouponsForCouponsResponseDto>>> GetUserCouponsForCouponsAsync(int couponsId);
        Task<Response<GetUserCouponsForCouponsResponseDto>> GetUserCouponsForCouponsDetailsAsync(int couponsId, int userCouponsUserId);
        Task<Response<int>> UpdateUserCouponsAsync(int couponsUserId, UpdateUserCouponsRequestDto updateRequest);
        Task<Response<int>> UpdateUserCouponsPrmAsync(int couponsUserId, UpdateUserCouponsPrmRequestDto updateRequest);
        Task<Response<int>> UpdateUserCouponsSingleAsync(int couponsUserId, UpdateUserCouponsSingleRequestDto updateRequest);
        Task<Response<int>> UpdateUserCouponsWherePrmAsync(int couponsUserId, UpdateUserCouponsWherePrmRequestDto updateRequest);
        Task<Response<int>> UpdateUserCouponsRefColAsync(int couponsUserId, UpdateUserCouponsRefColRequestDto updateRequest);
    }
}