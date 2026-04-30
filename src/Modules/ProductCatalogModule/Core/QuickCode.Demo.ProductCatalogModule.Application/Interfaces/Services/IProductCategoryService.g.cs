using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductCategory;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.ProductCategory
{
    public partial interface IProductCategoryService
    {
        Task<Response<ProductCategoryDto>> InsertAsync(ProductCategoryDto request);
        Task<Response<bool>> DeleteAsync(ProductCategoryDto request);
        Task<Response<bool>> UpdateAsync(int productId, int categoryId, ProductCategoryDto request);
        Task<Response<List<ProductCategoryDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductCategoryDto>> GetItemAsync(int productId, int categoryId);
        Task<Response<bool>> DeleteItemAsync(int productId, int categoryId);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByProductIdResponseDto>>> GetByProductIdAsync(int productCategoryProductId);
        Task<Response<List<GetByCategoryIdResponseDto>>> GetByCategoryIdAsync(int productCategoryCategoryId, int? pageNumber, int? pageSize);
        Task<Response<int>> RemoveFromCategoryAsync(int productCategoryProductId, int productCategoryCategoryId);
    }
}