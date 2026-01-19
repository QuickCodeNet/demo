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
    public partial class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _repository;
        public ProductService(ILogger<ProductService> logger, IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProductDto>> InsertAsync(ProductDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProductDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ProductDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProductDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProductDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SearchProductsResponseDto>>> SearchProductsAsync(int productsProductGroupId, string productsTitle, decimal productsPrice)
        {
            var returnValue = await _repository.SearchProductsAsync(productsProductGroupId, productsTitle, productsPrice);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ListLowStockResponseDto>>> ListLowStockAsync()
        {
            var returnValue = await _repository.ListLowStockAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductImagesForProductsResponseDto>>> GetProductImagesForProductsAsync(int productsId)
        {
            var returnValue = await _repository.GetProductImagesForProductsAsync(productsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetProductImagesForProductsResponseDto>> GetProductImagesForProductsDetailsAsync(int productsId, int productImagesId)
        {
            var returnValue = await _repository.GetProductImagesForProductsDetailsAsync(productsId, productImagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductReviewsForProductsResponseDto>>> GetProductReviewsForProductsAsync(int productsId)
        {
            var returnValue = await _repository.GetProductReviewsForProductsAsync(productsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetProductReviewsForProductsResponseDto>> GetProductReviewsForProductsDetailsAsync(int productsId, int productReviewsId)
        {
            var returnValue = await _repository.GetProductReviewsForProductsDetailsAsync(productsId, productReviewsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCartItemsForProductsResponseDto>>> GetCartItemsForProductsAsync(int productsId)
        {
            var returnValue = await _repository.GetCartItemsForProductsAsync(productsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCartItemsForProductsResponseDto>> GetCartItemsForProductsDetailsAsync(int productsId, int cartItemsId)
        {
            var returnValue = await _repository.GetCartItemsForProductsDetailsAsync(productsId, cartItemsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOrderItemsForProductsResponseDto>>> GetOrderItemsForProductsAsync(int productsId)
        {
            var returnValue = await _repository.GetOrderItemsForProductsAsync(productsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOrderItemsForProductsResponseDto>> GetOrderItemsForProductsDetailsAsync(int productsId, int orderItemsId)
        {
            var returnValue = await _repository.GetOrderItemsForProductsDetailsAsync(productsId, orderItemsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> ReduceStockAsync(int productsId, ReduceStockRequestDto updateRequest)
        {
            var returnValue = await _repository.ReduceStockAsync(productsId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}