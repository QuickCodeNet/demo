using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductVariant;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.ProductVariant
{
    public partial interface IProductVariantService
    {
        Task<Response<ProductVariantDto>> InsertAsync(ProductVariantDto request);
        Task<Response<bool>> DeleteAsync(ProductVariantDto request);
        Task<Response<bool>> UpdateAsync(int id, ProductVariantDto request);
        Task<Response<List<ProductVariantDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductVariantDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByProductIdResponseDto>>> GetByProductIdAsync(int productVariantProductId, int? pageNumber, int? pageSize);
        Task<Response<List<GetActiveByProductIdResponseDto>>> GetActiveByProductIdAsync(int productVariantProductId, int? pageNumber, int? pageSize);
        Task<Response<long>> GetLowStockVariantsAsync();
        Task<Response<int>> UpdateStockAsync(int productVariantId, UpdateStockRequestDto updateRequest);
        Task<Response<int>> UpdatePriceAsync(int productVariantId, UpdatePriceRequestDto updateRequest);
        Task<Response<int>> DeactivateAsync(int productVariantId, DeactivateRequestDto updateRequest);
    }
}