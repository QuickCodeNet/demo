using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SellerManagementModule.Domain.Entities;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.SellerTier;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Application.Services.SellerTier
{
    public partial interface ISellerTierService
    {
        Task<Response<SellerTierDto>> InsertAsync(SellerTierDto request);
        Task<Response<bool>> DeleteAsync(SellerTierDto request);
        Task<Response<bool>> UpdateAsync(int id, SellerTierDto request);
        Task<Response<List<SellerTierDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SellerTierDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<GetByNameResponseDto>> GetByNameAsync(string sellerTierName);
    }
}