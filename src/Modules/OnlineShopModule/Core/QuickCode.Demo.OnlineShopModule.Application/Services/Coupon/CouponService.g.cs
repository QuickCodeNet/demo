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
    public partial class CouponService : ICouponService
    {
        private readonly ILogger<CouponService> _logger;
        private readonly ICouponRepository _repository;
        public CouponService(ILogger<CouponService> logger, ICouponRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CouponDto>> InsertAsync(CouponDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CouponDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CouponDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CouponDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CouponDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
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

        public async Task<Response<List<GetValidCouponsResponseDto>>> GetValidCouponsAsync(string couponsCouponCode)
        {
            var returnValue = await _repository.GetValidCouponsAsync(couponsCouponCode);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUserCouponsResponseDto>>> GetUserCouponsAsync(int couponsUserId)
        {
            var returnValue = await _repository.GetUserCouponsAsync(couponsUserId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUserCouponsForCouponsResponseDto>>> GetUserCouponsForCouponsAsync(int couponsId)
        {
            var returnValue = await _repository.GetUserCouponsForCouponsAsync(couponsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetUserCouponsForCouponsResponseDto>> GetUserCouponsForCouponsDetailsAsync(int couponsId, int userCouponsUserId)
        {
            var returnValue = await _repository.GetUserCouponsForCouponsDetailsAsync(couponsId, userCouponsUserId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateUserCouponsAsync(int couponsUserId, UpdateUserCouponsRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateUserCouponsAsync(couponsUserId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateUserCouponsPrmAsync(int couponsUserId, UpdateUserCouponsPrmRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateUserCouponsPrmAsync(couponsUserId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateUserCouponsSingleAsync(int couponsUserId, UpdateUserCouponsSingleRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateUserCouponsSingleAsync(couponsUserId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateUserCouponsWherePrmAsync(int couponsUserId, UpdateUserCouponsWherePrmRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateUserCouponsWherePrmAsync(couponsUserId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateUserCouponsRefColAsync(int couponsUserId, UpdateUserCouponsRefColRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateUserCouponsRefColAsync(couponsUserId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}