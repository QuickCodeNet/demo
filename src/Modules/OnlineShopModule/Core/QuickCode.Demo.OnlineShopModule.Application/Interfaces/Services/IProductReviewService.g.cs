using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductReview;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ProductReview
{
    public partial interface IProductReviewService
    {
        Task<Response<ProductReviewDto>> InsertAsync(ProductReviewDto request);
        Task<Response<bool>> DeleteAsync(ProductReviewDto request);
        Task<Response<bool>> UpdateAsync(int id, ProductReviewDto request);
        Task<Response<List<ProductReviewDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductReviewDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetRecentReviewsResponseDto>>> GetRecentReviewsAsync(int productReviewsProductId);
        Task<Response<List<GetProductReviewsResponseDto>>> GetProductReviewsAsync(int productReviewsProductId);
    }
}