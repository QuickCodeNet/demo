using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SellerManagementModule.Domain.Entities;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.SellerPerformanceReview;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Application.Services.SellerPerformanceReview
{
    public partial class SellerPerformanceReviewService : ISellerPerformanceReviewService
    {
        private readonly ILogger<SellerPerformanceReviewService> _logger;
        private readonly ISellerPerformanceReviewRepository _repository;
        public SellerPerformanceReviewService(ILogger<SellerPerformanceReviewService> logger, ISellerPerformanceReviewRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SellerPerformanceReviewDto>> InsertAsync(SellerPerformanceReviewDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SellerPerformanceReviewDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SellerPerformanceReviewDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SellerPerformanceReviewDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SellerPerformanceReviewDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerPerformanceReviewSellerId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetBySellerIdAsync(sellerPerformanceReviewSellerId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetSellerAverageRatingResponseDto>> GetSellerAverageRatingAsync(int sellerPerformanceReviewSellerId)
        {
            var returnValue = await _repository.GetSellerAverageRatingAsync(sellerPerformanceReviewSellerId);
            return returnValue.ToResponse();
        }
    }
}