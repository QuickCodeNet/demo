using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Cart;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Cart
{
    public partial class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly ICartRepository _repository;
        public CartService(ILogger<CartService> logger, ICartRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CartDto>> InsertAsync(CartDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CartDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CartDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CartDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CartDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetAbandonedCartsResponseDto>>> GetAbandonedCartsAsync(bool cartsIsActive)
        {
            var returnValue = await _repository.GetAbandonedCartsAsync(cartsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCartItemsForCartsResponseDto>>> GetCartItemsForCartsAsync(int cartsId)
        {
            var returnValue = await _repository.GetCartItemsForCartsAsync(cartsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCartItemsForCartsResponseDto>> GetCartItemsForCartsDetailsAsync(int cartsId, int cartItemsId)
        {
            var returnValue = await _repository.GetCartItemsForCartsDetailsAsync(cartsId, cartItemsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeactivateStaleCartsAsync(bool cartsIsActive, DeactivateStaleCartsRequestDto updateRequest)
        {
            var returnValue = await _repository.DeactivateStaleCartsAsync(cartsIsActive, updateRequest);
            return returnValue.ToResponse();
        }
    }
}