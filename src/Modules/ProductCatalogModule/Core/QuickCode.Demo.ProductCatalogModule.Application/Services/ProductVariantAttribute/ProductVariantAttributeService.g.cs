using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductVariantAttribute;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.ProductVariantAttribute
{
    public partial class ProductVariantAttributeService : IProductVariantAttributeService
    {
        private readonly ILogger<ProductVariantAttributeService> _logger;
        private readonly IProductVariantAttributeRepository _repository;
        public ProductVariantAttributeService(ILogger<ProductVariantAttributeService> logger, IProductVariantAttributeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProductVariantAttributeDto>> InsertAsync(ProductVariantAttributeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProductVariantAttributeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int variantId, int attributeValueId, ProductVariantAttributeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.VariantId, request.AttributeValueId);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProductVariantAttributeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProductVariantAttributeDto>> GetItemAsync(int variantId, int attributeValueId)
        {
            var returnValue = await _repository.GetByPkAsync(variantId, attributeValueId);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int variantId, int attributeValueId)
        {
            var deleteItem = await _repository.GetByPkAsync(variantId, attributeValueId);
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

        public async Task<Response<List<GetByVariantIdResponseDto>>> GetByVariantIdAsync(int productVariantAttributeVariantId)
        {
            var returnValue = await _repository.GetByVariantIdAsync(productVariantAttributeVariantId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RemoveAttributeAsync(int productVariantAttributeVariantId, int productVariantAttributeAttributeValueId)
        {
            var returnValue = await _repository.RemoveAttributeAsync(productVariantAttributeVariantId, productVariantAttributeAttributeValueId);
            return returnValue.ToResponse();
        }
    }
}