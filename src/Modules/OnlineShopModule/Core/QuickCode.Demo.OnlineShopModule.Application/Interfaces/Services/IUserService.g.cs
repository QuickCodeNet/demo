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
    public partial interface IUserService
    {
        Task<Response<UserDto>> InsertAsync(UserDto request);
        Task<Response<bool>> DeleteAsync(UserDto request);
        Task<Response<bool>> UpdateAsync(int id, UserDto request);
        Task<Response<List<UserDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<UserDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetNewUsersResponseDto>>> GetNewUsersAsync(bool usersIsNew);
        Task<Response<List<GetCouponsForUsersResponseDto>>> GetCouponsForUsersAsync(int usersId);
        Task<Response<GetCouponsForUsersResponseDto>> GetCouponsForUsersDetailsAsync(int usersId, int couponsId);
        Task<Response<List<GetUserCouponsForUsersResponseDto>>> GetUserCouponsForUsersAsync(int usersId);
        Task<Response<GetUserCouponsForUsersResponseDto>> GetUserCouponsForUsersDetailsAsync(int usersId, int userCouponsUserId);
        Task<Response<List<GetProductReviewsForUsersResponseDto>>> GetProductReviewsForUsersAsync(int usersId);
        Task<Response<GetProductReviewsForUsersResponseDto>> GetProductReviewsForUsersDetailsAsync(int usersId, int productReviewsId);
        Task<Response<List<GetCartsForUsersResponseDto>>> GetCartsForUsersAsync(int usersId);
        Task<Response<GetCartsForUsersResponseDto>> GetCartsForUsersDetailsAsync(int usersId, int cartsId);
        Task<Response<List<GetOrdersForUsersResponseDto>>> GetOrdersForUsersAsync(int usersId);
        Task<Response<GetOrdersForUsersResponseDto>> GetOrdersForUsersDetailsAsync(int usersId, int ordersId);
    }
}