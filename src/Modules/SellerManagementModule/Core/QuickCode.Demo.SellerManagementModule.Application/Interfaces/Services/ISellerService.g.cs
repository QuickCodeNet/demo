using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SellerManagementModule.Domain.Entities;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.Seller;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Application.Services.Seller
{
    public partial interface ISellerService
    {
        Task<Response<SellerDto>> InsertAsync(SellerDto request);
        Task<Response<bool>> DeleteAsync(SellerDto request);
        Task<Response<bool>> UpdateAsync(int id, SellerDto request);
        Task<Response<List<SellerDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SellerDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByUserIdResponseDto>> GetByUserIdAsync(int sellerUserId);
        Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(SellerStatus sellerStatus, int? page, int? size);
        Task<Response<List<SearchByCompanyNameResponseDto>>> SearchByCompanyNameAsync(string sellerCompanyName, int? page, int? size);
        Task<Response<long>> GetPendingVerificationCountAsync(SellerStatus sellerStatus);
        Task<Response<int>> ApproveAsync(int sellerId, ApproveRequestDto updateRequest);
        Task<Response<int>> SuspendAsync(int sellerId, SuspendRequestDto updateRequest);
        Task<Response<int>> RejectAsync(int sellerId, RejectRequestDto updateRequest);
        Task<Response<int>> UpdateTierAsync(int sellerId, UpdateTierRequestDto updateRequest);
    }
}