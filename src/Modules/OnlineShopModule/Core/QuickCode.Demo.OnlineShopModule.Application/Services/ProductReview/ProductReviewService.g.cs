using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductReview;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.ProductReview
{
    public partial class ProductReviewService : IProductReviewService
    {
        private readonly ILogger<ProductReviewService> _logger;
        private readonly IProductReviewRepository _repository;
        public ProductReviewService(ILogger<ProductReviewService> logger, IProductReviewRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProductReviewDto>> InsertAsync(ProductReviewDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProductReviewDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ProductReviewDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProductReviewDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProductReviewDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetRecentReviewsResponseDto>>> GetRecentReviewsAsync(int productReviewsProductId)
        {
            var returnValue = await _repository.GetRecentReviewsAsync(productReviewsProductId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProductReviewsResponseDto>>> GetProductReviewsAsync(int productReviewsProductId)
        {
            var returnValue = await _repository.GetProductReviewsAsync(productReviewsProductId);
            return returnValue.ToResponse();
        }
    }
}