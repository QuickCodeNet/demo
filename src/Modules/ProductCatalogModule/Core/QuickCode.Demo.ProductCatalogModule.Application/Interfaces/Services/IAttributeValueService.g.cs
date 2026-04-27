using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.AttributeValue;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.AttributeValue
{
    public partial interface IAttributeValueService
    {
        Task<Response<AttributeValueDto>> InsertAsync(AttributeValueDto request);
        Task<Response<bool>> DeleteAsync(AttributeValueDto request);
        Task<Response<bool>> UpdateAsync(int id, AttributeValueDto request);
        Task<Response<List<AttributeValueDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<AttributeValueDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByAttributeIdResponseDto>>> GetByAttributeIdAsync(int attributeValueAttributeId, int? page, int? size);
    }
}