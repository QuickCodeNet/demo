using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ShippingInfo;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ShippingInfo
{
    public partial interface IShippingInfoService
    {
        Task<Response<ShippingInfoDto>> InsertAsync(ShippingInfoDto request);
        Task<Response<bool>> DeleteAsync(ShippingInfoDto request);
        Task<Response<bool>> UpdateAsync(int id, ShippingInfoDto request);
        Task<Response<List<ShippingInfoDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ShippingInfoDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetUserShippingInfoResponseDto>>> GetUserShippingInfoAsync(int shippingInfosUserId);
        Task<Response<List<GetOrdersForShippingInfosResponseDto>>> GetOrdersForShippingInfosAsync(int shippingInfosId);
        Task<Response<GetOrdersForShippingInfosResponseDto>> GetOrdersForShippingInfosDetailsAsync(int shippingInfosId, int ordersId);
        Task<Response<int>> SetDefaultShippingAsync(int shippingInfosUserId, SetDefaultShippingRequestDto updateRequest);
        Task<Response<int>> SetDefaultShippingAddressAsync(int shippingInfosId, int shippingInfosUserId, SetDefaultShippingAddressRequestDto updateRequest);
    }
}