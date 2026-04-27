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
    public partial class CategoryCommissionOverrideService : ICategoryCommissionOverrideService
    {
        private readonly ILogger<CategoryCommissionOverrideService> _logger;
        private readonly ICategoryCommissionOverrideRepository _repository;
        public CategoryCommissionOverrideService(ILogger<CategoryCommissionOverrideService> logger, ICategoryCommissionOverrideRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CategoryCommissionOverrideDto>> InsertAsync(CategoryCommissionOverrideDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CategoryCommissionOverrideDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CategoryCommissionOverrideDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CategoryCommissionOverrideDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CategoryCommissionOverrideDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByCategoryIdResponseDto>> GetByCategoryIdAsync(int categoryCommissionOverrideCategoryId)
        {
            var returnValue = await _repository.GetByCategoryIdAsync(categoryCommissionOverrideCategoryId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RemoveOverrideAsync(int categoryCommissionOverrideId)
        {
            var returnValue = await _repository.RemoveOverrideAsync(categoryCommissionOverrideId);
            return returnValue.ToResponse();
        }
    }
}