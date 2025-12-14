using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.UserCoupon;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.UserCoupon
{
    public partial interface IUserCouponService
    {
        Task<Response<UserCouponDto>> InsertAsync(UserCouponDto request);
        Task<Response<bool>> DeleteAsync(UserCouponDto request);
        Task<Response<bool>> UpdateAsync(int userId, int couponId, UserCouponDto request);
        Task<Response<List<UserCouponDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<UserCouponDto>> GetItemAsync(int userId, int couponId);
        Task<Response<bool>> DeleteItemAsync(int userId, int couponId);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetUsedCouponsResponseDto>>> GetUsedCouponsAsync(bool userCouponsIsUsed);
        Task<Response<int>> UpdateUsedCouponsAsync(bool userCouponsIsUsed, UpdateUsedCouponsRequestDto updateRequest);
        Task<Response<int>> UpdateAllUsedCouponsAsync(UpdateAllUsedCouponsRequestDto updateRequest);
        Task<Response<int>> UpdateAllTrueUsedCouponsAsync(UpdateAllTrueUsedCouponsRequestDto updateRequest);
    }
}