using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CategoryCommissionOverride;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.CategoryCommissionOverride
{
    public partial interface ICategoryCommissionOverrideService
    {
        Task<Response<CategoryCommissionOverrideDto>> InsertAsync(CategoryCommissionOverrideDto request);
        Task<Response<bool>> DeleteAsync(CategoryCommissionOverrideDto request);
        Task<Response<bool>> UpdateAsync(int id, CategoryCommissionOverrideDto request);
        Task<Response<List<CategoryCommissionOverrideDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CategoryCommissionOverrideDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByCategoryIdResponseDto>> GetByCategoryIdAsync(int categoryCommissionOverrideCategoryId);
        Task<Response<int>> RemoveOverrideAsync(int categoryCommissionOverrideId);
    }
}