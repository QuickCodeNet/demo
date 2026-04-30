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

        public async Task<Response<GetBySkuResponseDto>> GetBySkuAsync(string productSku)
        {
            var returnValue = await _repository.GetBySkuAsync(productSku);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveBySellerResponseDto>>> GetActiveBySellerAsync(int productSellerId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetActiveBySellerAsync(productSellerId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SearchProductsResponseDto>>> SearchProductsAsync(string productName, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.SearchProductsAsync(productName, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetPendingApprovalResponseDto>>> GetPendingApprovalAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetPendingApprovalAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFeaturedResponseDto>>> GetFeaturedAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetFeaturedAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductsWithDetailsResponseDto>>> GetProductsWithDetailsAsync(int productsBrandId, int productPrimaryCategoryId, int categoryId, int brandId, int productsPrimaryCategoryId, int productBrandId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetProductsWithDetailsAsync(productsBrandId, productPrimaryCategoryId, categoryId, brandId, productsPrimaryCategoryId, productBrandId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetCountByStatusAsync(ProductStatus productStatus)
        {
            var returnValue = await _repository.GetCountByStatusAsync(productStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> ApproveAsync(int productId, ApproveRequestDto updateRequest)
        {
            var returnValue = await _repository.ApproveAsync(productId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RejectAsync(int productId, RejectRequestDto updateRequest)
        {
            var returnValue = await _repository.RejectAsync(productId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> SetFeaturedAsync(int productId, SetFeaturedRequestDto updateRequest)
        {
            var returnValue = await _repository.SetFeaturedAsync(productId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}