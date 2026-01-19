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
    public partial class ShippingInfoService : IShippingInfoService
    {
        private readonly ILogger<ShippingInfoService> _logger;
        private readonly IShippingInfoRepository _repository;
        public ShippingInfoService(ILogger<ShippingInfoService> logger, IShippingInfoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ShippingInfoDto>> InsertAsync(ShippingInfoDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ShippingInfoDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ShippingInfoDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ShippingInfoDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ShippingInfoDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetUserShippingInfoResponseDto>>> GetUserShippingInfoAsync(int shippingInfosUserId)
        {
            var returnValue = await _repository.GetUserShippingInfoAsync(shippingInfosUserId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOrdersForShippingInfosResponseDto>>> GetOrdersForShippingInfosAsync(int shippingInfosId)
        {
            var returnValue = await _repository.GetOrdersForShippingInfosAsync(shippingInfosId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOrdersForShippingInfosResponseDto>> GetOrdersForShippingInfosDetailsAsync(int shippingInfosId, int ordersId)
        {
            var returnValue = await _repository.GetOrdersForShippingInfosDetailsAsync(shippingInfosId, ordersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> SetDefaultShippingAsync(int shippingInfosUserId, SetDefaultShippingRequestDto updateRequest)
        {
            var returnValue = await _repository.SetDefaultShippingAsync(shippingInfosUserId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> SetDefaultShippingAddressAsync(int shippingInfosId, int shippingInfosUserId, SetDefaultShippingAddressRequestDto updateRequest)
        {
            var returnValue = await _repository.SetDefaultShippingAddressAsync(shippingInfosId, shippingInfosUserId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}