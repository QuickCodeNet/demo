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
    public partial interface IProductVariantAttributeService
    {
        Task<Response<ProductVariantAttributeDto>> InsertAsync(ProductVariantAttributeDto request);
        Task<Response<bool>> DeleteAsync(ProductVariantAttributeDto request);
        Task<Response<bool>> UpdateAsync(int variantId, int attributeValueId, ProductVariantAttributeDto request);
        Task<Response<List<ProductVariantAttributeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProductVariantAttributeDto>> GetItemAsync(int variantId, int attributeValueId);
        Task<Response<bool>> DeleteItemAsync(int variantId, int attributeValueId);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByVariantIdResponseDto>>> GetByVariantIdAsync(int productVariantAttributeVariantId);
        Task<Response<int>> RemoveAttributeAsync(int productVariantAttributeVariantId, int productVariantAttributeAttributeValueId);
    }
}