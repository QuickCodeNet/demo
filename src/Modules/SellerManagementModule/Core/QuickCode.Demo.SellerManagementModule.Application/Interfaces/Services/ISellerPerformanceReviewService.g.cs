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
    public partial interface ISellerPerformanceReviewService
    {
        Task<Response<SellerPerformanceReviewDto>> InsertAsync(SellerPerformanceReviewDto request);
        Task<Response<bool>> DeleteAsync(SellerPerformanceReviewDto request);
        Task<Response<bool>> UpdateAsync(int id, SellerPerformanceReviewDto request);
        Task<Response<List<SellerPerformanceReviewDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SellerPerformanceReviewDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerPerformanceReviewSellerId, int? page, int? size);
        Task<Response<GetSellerAverageRatingResponseDto>> GetSellerAverageRatingAsync(int sellerPerformanceReviewSellerId);
    }
}