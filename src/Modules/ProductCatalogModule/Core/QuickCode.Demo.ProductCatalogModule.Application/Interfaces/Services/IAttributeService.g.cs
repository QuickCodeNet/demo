using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.Attribute;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.Attribute
{
    public partial interface IAttributeService
    {
        Task<Response<AttributeDto>> InsertAsync(AttributeDto request);
        Task<Response<bool>> DeleteAsync(AttributeDto request);
        Task<Response<bool>> UpdateAsync(int id, AttributeDto request);
        Task<Response<List<AttributeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<AttributeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByCodeResponseDto>> GetByCodeAsync(string attributeCode);
    }
}