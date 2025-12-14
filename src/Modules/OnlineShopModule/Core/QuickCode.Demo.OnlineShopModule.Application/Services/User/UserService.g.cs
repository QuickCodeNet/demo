using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.User;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.User
{
    public partial class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _repository;
        public UserService(ILogger<UserService> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<UserDto>> InsertAsync(UserDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(UserDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, UserDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<UserDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<UserDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetNewUsersResponseDto>>> GetNewUsersAsync(bool usersIsNew)
        {
            var returnValue = await _repository.GetNewUsersAsync(usersIsNew);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCouponsForUsersResponseDto>>> GetCouponsForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetCouponsForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCouponsForUsersResponseDto>> GetCouponsForUsersDetailsAsync(int usersId, int couponsId)
        {
            var returnValue = await _repository.GetCouponsForUsersDetailsAsync(usersId, couponsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUserCouponsForUsersResponseDto>>> GetUserCouponsForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetUserCouponsForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetUserCouponsForUsersResponseDto>> GetUserCouponsForUsersDetailsAsync(int usersId, int userCouponsUserId)
        {
            var returnValue = await _repository.GetUserCouponsForUsersDetailsAsync(usersId, userCouponsUserId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductReviewsForUsersResponseDto>>> GetProductReviewsForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetProductReviewsForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetProductReviewsForUsersResponseDto>> GetProductReviewsForUsersDetailsAsync(int usersId, int productReviewsId)
        {
            var returnValue = await _repository.GetProductReviewsForUsersDetailsAsync(usersId, productReviewsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCartsForUsersResponseDto>>> GetCartsForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetCartsForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCartsForUsersResponseDto>> GetCartsForUsersDetailsAsync(int usersId, int cartsId)
        {
            var returnValue = await _repository.GetCartsForUsersDetailsAsync(usersId, cartsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOrdersForUsersResponseDto>>> GetOrdersForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetOrdersForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOrdersForUsersResponseDto>> GetOrdersForUsersDetailsAsync(int usersId, int ordersId)
        {
            var returnValue = await _repository.GetOrdersForUsersDetailsAsync(usersId, ordersId);
            return returnValue.ToResponse();
        }
    }
}