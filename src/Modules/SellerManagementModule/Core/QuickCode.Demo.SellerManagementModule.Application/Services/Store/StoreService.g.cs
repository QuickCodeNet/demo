using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SellerManagementModule.Domain.Entities;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.Store;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Application.Services.Store
{
    public partial class StoreService : IStoreService
    {
        private readonly ILogger<StoreService> _logger;
        private readonly IStoreRepository _repository;
        public StoreService(ILogger<StoreService> logger, IStoreRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<StoreDto>> InsertAsync(StoreDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(StoreDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, StoreDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StoreDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<StoreDto>> GetItemAsync(int id)
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

        public async Task<Response<GetBySellerIdResponseDto>> GetBySellerIdAsync(int storeSellerId)
        {
            var returnValue = await _repository.GetBySellerIdAsync(storeSellerId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetBySlugResponseDto>> GetBySlugAsync(string storeSlug)
        {
            var returnValue = await _repository.GetBySlugAsync(storeSlug);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateProfileAsync(int storeId, UpdateProfileRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateProfileAsync(storeId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeactivateAsync(int storeId, DeactivateRequestDto updateRequest)
        {
            var returnValue = await _repository.DeactivateAsync(storeId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}