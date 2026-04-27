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
    public partial class ProductCategoryService : IProductCategoryService
    {
        private readonly ILogger<ProductCategoryService> _logger;
        private readonly IProductCategoryRepository _repository;
        public ProductCategoryService(ILogger<ProductCategoryService> logger, IProductCategoryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProductCategoryDto>> InsertAsync(ProductCategoryDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProductCategoryDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int productId, int categoryId, ProductCategoryDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.ProductId, request.CategoryId);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProductCategoryDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProductCategoryDto>> GetItemAsync(int productId, int categoryId)
        {
            var returnValue = await _repository.GetByPkAsync(productId, categoryId);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int productId, int categoryId)
        {
            var deleteItem = await _repository.GetByPkAsync(productId, categoryId);
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

        public async Task<Response<List<GetByProductIdResponseDto>>> GetByProductIdAsync(int productCategoryProductId)
        {
            var returnValue = await _repository.GetByProductIdAsync(productCategoryProductId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByCategoryIdResponseDto>>> GetByCategoryIdAsync(int productCategoryCategoryId, int? page, int? size)
        {
            var returnValue = await _repository.GetByCategoryIdAsync(productCategoryCategoryId, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RemoveFromCategoryAsync(int productCategoryProductId, int productCategoryCategoryId)
        {
            var returnValue = await _repository.RemoveFromCategoryAsync(productCategoryProductId, productCategoryCategoryId);
            return returnValue.ToResponse();
        }
    }
}