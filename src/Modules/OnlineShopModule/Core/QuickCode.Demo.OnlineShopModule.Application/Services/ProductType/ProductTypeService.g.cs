using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductType;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ProductType
{
    public partial class ProductTypeService : IProductTypeService
    {
        private readonly ILogger<ProductTypeService> _logger;
        private readonly IProductTypeRepository _repository;
        public ProductTypeService(ILogger<ProductTypeService> logger, IProductTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProductTypeDto>> InsertAsync(ProductTypeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProductTypeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ProductTypeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProductTypeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProductTypeDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetProductTypesResponseDto>>> GetProductTypesAsync(int productGroupsProductTypeId)
        {
            var returnValue = await _repository.GetProductTypesAsync(productGroupsProductTypeId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductGroupsForProductTypesResponseDto>>> GetProductGroupsForProductTypesAsync(int productTypesId)
        {
            var returnValue = await _repository.GetProductGroupsForProductTypesAsync(productTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetProductGroupsForProductTypesResponseDto>> GetProductGroupsForProductTypesDetailsAsync(int productTypesId, int productGroupsId)
        {
            var returnValue = await _repository.GetProductGroupsForProductTypesDetailsAsync(productTypesId, productGroupsId);
            return returnValue.ToResponse();
        }
    }
}