using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductGroup;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ProductGroup
{
    public partial interface IProductGroupService
    {
        Task<Response<ProductGroupDto>> InsertAsync(ProductGroupDto request);
        Task<Response<bool>> DeleteAsync(ProductGroupDto request);
        Task<Response<bool>> UpdateAsync(int id, ProductGroupDto request);
        Task<Response<List<ProductGroupDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductGroupDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetGroupsByTypeResponseDto>>> GetGroupsByTypeAsync(int productGroupsProductTypeId);
        Task<Response<List<GetProductGroupsByTypeResponseDto>>> GetProductGroupsByTypeAsync(int productGroupsProductTypeId);
        Task<Response<List<GetProductsForProductGroupsResponseDto>>> GetProductsForProductGroupsAsync(int productGroupsId);
        Task<Response<GetProductsForProductGroupsResponseDto>> GetProductsForProductGroupsDetailsAsync(int productGroupsId, int productsId);
    }
}