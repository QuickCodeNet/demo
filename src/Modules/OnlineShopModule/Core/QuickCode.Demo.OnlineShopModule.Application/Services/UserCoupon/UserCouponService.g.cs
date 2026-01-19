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
    public partial class UserCouponService : IUserCouponService
    {
        private readonly ILogger<UserCouponService> _logger;
        private readonly IUserCouponRepository _repository;
        public UserCouponService(ILogger<UserCouponService> logger, IUserCouponRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<UserCouponDto>> InsertAsync(UserCouponDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(UserCouponDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int userId, int couponId, UserCouponDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.UserId, request.CouponId);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<UserCouponDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<UserCouponDto>> GetItemAsync(int userId, int couponId)
        {
            var returnValue = await _repository.GetByPkAsync(userId, couponId);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int userId, int couponId)
        {
            var deleteItem = await _repository.GetByPkAsync(userId, couponId);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUsedCouponsResponseDto>>> GetUsedCouponsAsync(bool userCouponsIsUsed)
        {
            var returnValue = await _repository.GetUsedCouponsAsync(userCouponsIsUsed);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateUsedCouponsAsync(bool userCouponsIsUsed, UpdateUsedCouponsRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateUsedCouponsAsync(userCouponsIsUsed, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateAllUsedCouponsAsync(UpdateAllUsedCouponsRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateAllUsedCouponsAsync(updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateAllTrueUsedCouponsAsync(UpdateAllTrueUsedCouponsRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateAllTrueUsedCouponsAsync(updateRequest);
            return returnValue.ToResponse();
        }
    }
}