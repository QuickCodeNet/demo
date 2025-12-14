using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductGroup;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ProductGroup
{
    public partial class ProductGroupService : IProductGroupService
    {
        private readonly ILogger<ProductGroupService> _logger;
        private readonly IProductGroupRepository _repository;
        public ProductGroupService(ILogger<ProductGroupService> logger, IProductGroupRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProductGroupDto>> InsertAsync(ProductGroupDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProductGroupDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ProductGroupDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProductGroupDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProductGroupDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetGroupsByTypeResponseDto>>> GetGroupsByTypeAsync(int productGroupsProductTypeId)
        {
            var returnValue = await _repository.GetGroupsByTypeAsync(productGroupsProductTypeId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductGroupsByTypeResponseDto>>> GetProductGroupsByTypeAsync(int productGroupsProductTypeId)
        {
            var returnValue = await _repository.GetProductGroupsByTypeAsync(productGroupsProductTypeId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductsForProductGroupsResponseDto>>> GetProductsForProductGroupsAsync(int productGroupsId)
        {
            var returnValue = await _repository.GetProductsForProductGroupsAsync(productGroupsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetProductsForProductGroupsResponseDto>> GetProductsForProductGroupsDetailsAsync(int productGroupsId, int productsId)
        {
            var returnValue = await _repository.GetProductsForProductGroupsDetailsAsync(productGroupsId, productsId);
            return returnValue.ToResponse();
        }
    }
}