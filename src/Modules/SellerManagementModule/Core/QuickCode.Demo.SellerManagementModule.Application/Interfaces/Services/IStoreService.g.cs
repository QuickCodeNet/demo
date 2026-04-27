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
    public partial interface IStoreService
    {
        Task<Response<StoreDto>> InsertAsync(StoreDto request);
        Task<Response<bool>> DeleteAsync(StoreDto request);
        Task<Response<bool>> UpdateAsync(int id, StoreDto request);
        Task<Response<List<StoreDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<StoreDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetBySellerIdResponseDto>> GetBySellerIdAsync(int storeSellerId);
        Task<Response<GetBySlugResponseDto>> GetBySlugAsync(string storeSlug);
        Task<Response<int>> UpdateProfileAsync(int storeId, UpdateProfileRequestDto updateRequest);
        Task<Response<int>> DeactivateAsync(int storeId, DeactivateRequestDto updateRequest);
    }
}