using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.SellerCommissionAssignment;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.SellerCommissionAssignment
{
    public partial interface ISellerCommissionAssignmentService
    {
        Task<Response<SellerCommissionAssignmentDto>> InsertAsync(SellerCommissionAssignmentDto request);
        Task<Response<bool>> DeleteAsync(SellerCommissionAssignmentDto request);
        Task<Response<bool>> UpdateAsync(int sellerId, SellerCommissionAssignmentDto request);
        Task<Response<List<SellerCommissionAssignmentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SellerCommissionAssignmentDto>> GetItemAsync(int sellerId);
        Task<Response<bool>> DeleteItemAsync(int sellerId);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetBySellerIdResponseDto>> GetBySellerIdAsync(int sellerCommissionAssignmentSellerId);
        Task<Response<List<GetByModelIdResponseDto>>> GetByModelIdAsync(int sellerCommissionAssignmentCommissionModelId, int? page, int? size);
        Task<Response<int>> RemoveAssignmentAsync(int sellerCommissionAssignmentSellerId);
    }
}