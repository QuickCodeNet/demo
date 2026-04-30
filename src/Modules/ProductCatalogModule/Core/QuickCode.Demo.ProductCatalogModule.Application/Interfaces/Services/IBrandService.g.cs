using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.Brand;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.Brand
{
    public partial interface IBrandService
    {
        Task<Response<BrandDto>> InsertAsync(BrandDto request);
        Task<Response<bool>> DeleteAsync(BrandDto request);
        Task<Response<bool>> UpdateAsync(int id, BrandDto request);
        Task<Response<List<BrandDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<BrandDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveResponseDto>>> GetActiveAsync(int? pageNumber, int? pageSize);
        Task<Response<List<SearchByNameResponseDto>>> SearchByNameAsync(string brandName, int? pageNumber, int? pageSize);
        Task<Response<int>> DeactivateAsync(int brandId, DeactivateRequestDto updateRequest);
    }
}