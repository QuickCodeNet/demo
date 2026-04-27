using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.Category;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.Category
{
    public partial interface ICategoryService
    {
        Task<Response<CategoryDto>> InsertAsync(CategoryDto request);
        Task<Response<bool>> DeleteAsync(CategoryDto request);
        Task<Response<bool>> UpdateAsync(int id, CategoryDto request);
        Task<Response<List<CategoryDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CategoryDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveResponseDto>>> GetActiveAsync(bool categoryIsActive, int? page, int? size);
        Task<Response<List<GetSubCategoriesResponseDto>>> GetSubCategoriesAsync(int categoryParentCategoryId, bool categoryIsActive, int? page, int? size);
        Task<Response<GetBySlugResponseDto>> GetBySlugAsync(string categorySlug, bool categoryIsActive);
        Task<Response<int>> DeactivateAsync(int categoryId, DeactivateRequestDto updateRequest);
    }
}