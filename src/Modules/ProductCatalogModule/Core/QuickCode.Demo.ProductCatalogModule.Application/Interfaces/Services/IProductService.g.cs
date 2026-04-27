using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.Product;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.Product
{
    public partial interface IProductService
    {
        Task<Response<ProductDto>> InsertAsync(ProductDto request);
        Task<Response<bool>> DeleteAsync(ProductDto request);
        Task<Response<bool>> UpdateAsync(int id, ProductDto request);
        Task<Response<List<ProductDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetBySkuResponseDto>> GetBySkuAsync(string productSku);
        Task<Response<List<GetActiveBySellerResponseDto>>> GetActiveBySellerAsync(int productSellerId, ProductStatus productStatus, int? page, int? size);
        Task<Response<List<SearchProductsResponseDto>>> SearchProductsAsync(string productName, ProductStatus productStatus, int? page, int? size);
        Task<Response<List<GetPendingApprovalResponseDto>>> GetPendingApprovalAsync(ProductStatus productStatus, int? page, int? size);
        Task<Response<List<GetFeaturedResponseDto>>> GetFeaturedAsync(ProductStatus productStatus, bool productIsFeatured, int? page, int? size);
        Task<Response<List<GetProductsWithDetailsResponseDto>>> GetProductsWithDetailsAsync(int productsBrandId, int productPrimaryCategoryId, int categoryId, int brandId, int productsPrimaryCategoryId, int productBrandId, int? page, int? size);
        Task<Response<long>> GetCountByStatusAsync(ProductStatus productStatus);
        Task<Response<int>> ApproveAsync(int productId, ApproveRequestDto updateRequest);
        Task<Response<int>> RejectAsync(int productId, RejectRequestDto updateRequest);
        Task<Response<int>> SetFeaturedAsync(int productId, SetFeaturedRequestDto updateRequest);
    }
}