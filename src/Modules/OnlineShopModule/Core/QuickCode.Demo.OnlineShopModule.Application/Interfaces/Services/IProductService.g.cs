using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Product;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Product
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
        Task<Response<List<SearchProductsResponseDto>>> SearchProductsAsync(int productsProductGroupId, string productsTitle, decimal productsPrice);
        Task<Response<List<ListLowStockResponseDto>>> ListLowStockAsync();
        Task<Response<List<GetProductImagesForProductsResponseDto>>> GetProductImagesForProductsAsync(int productsId);
        Task<Response<GetProductImagesForProductsResponseDto>> GetProductImagesForProductsDetailsAsync(int productsId, int productImagesId);
        Task<Response<List<GetProductReviewsForProductsResponseDto>>> GetProductReviewsForProductsAsync(int productsId);
        Task<Response<GetProductReviewsForProductsResponseDto>> GetProductReviewsForProductsDetailsAsync(int productsId, int productReviewsId);
        Task<Response<List<GetCartItemsForProductsResponseDto>>> GetCartItemsForProductsAsync(int productsId);
        Task<Response<GetCartItemsForProductsResponseDto>> GetCartItemsForProductsDetailsAsync(int productsId, int cartItemsId);
        Task<Response<List<GetOrderItemsForProductsResponseDto>>> GetOrderItemsForProductsAsync(int productsId);
        Task<Response<GetOrderItemsForProductsResponseDto>> GetOrderItemsForProductsDetailsAsync(int productsId, int orderItemsId);
        Task<Response<int>> ReduceStockAsync(int productsId, ReduceStockRequestDto updateRequest);
    }
}