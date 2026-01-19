using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductType;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ProductType
{
    public partial interface IProductTypeService
    {
        Task<Response<ProductTypeDto>> InsertAsync(ProductTypeDto request);
        Task<Response<bool>> DeleteAsync(ProductTypeDto request);
        Task<Response<bool>> UpdateAsync(int id, ProductTypeDto request);
        Task<Response<List<ProductTypeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductTypeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetProductTypesResponseDto>>> GetProductTypesAsync(int productGroupsProductTypeId);
        Task<Response<List<GetProductGroupsForProductTypesResponseDto>>> GetProductGroupsForProductTypesAsync(int productTypesId);
        Task<Response<GetProductGroupsForProductTypesResponseDto>> GetProductGroupsForProductTypesDetailsAsync(int productTypesId, int productGroupsId);
    }
}