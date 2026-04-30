using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductVariant;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.ProductVariant
{
    public partial class ProductVariantService : IProductVariantService
    {
        private readonly ILogger<ProductVariantService> _logger;
        private readonly IProductVariantRepository _repository;
        public ProductVariantService(ILogger<ProductVariantService> logger, IProductVariantRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProductVariantDto>> InsertAsync(ProductVariantDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProductVariantDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ProductVariantDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProductVariantDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProductVariantDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetByProductIdResponseDto>>> GetByProductIdAsync(int productVariantProductId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetByProductIdAsync(productVariantProductId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveByProductIdResponseDto>>> GetActiveByProductIdAsync(int productVariantProductId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetActiveByProductIdAsync(productVariantProductId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetLowStockVariantsAsync()
        {
            var returnValue = await _repository.GetLowStockVariantsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStockAsync(int productVariantId, UpdateStockRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStockAsync(productVariantId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdatePriceAsync(int productVariantId, UpdatePriceRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdatePriceAsync(productVariantId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeactivateAsync(int productVariantId, DeactivateRequestDto updateRequest)
        {
            var returnValue = await _repository.DeactivateAsync(productVariantId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}