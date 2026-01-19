using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductImage;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ProductImage
{
    public partial interface IProductImageService
    {
        Task<Response<ProductImageDto>> InsertAsync(ProductImageDto request);
        Task<Response<bool>> DeleteAsync(ProductImageDto request);
        Task<Response<bool>> UpdateAsync(int id, ProductImageDto request);
        Task<Response<List<ProductImageDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductImageDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetValidProductImagesResponseDto>>> GetValidProductImagesAsync(int productImagesProductId);
    }
}