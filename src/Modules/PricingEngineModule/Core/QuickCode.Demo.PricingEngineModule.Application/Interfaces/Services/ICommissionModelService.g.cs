using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.PricingEngineModule.Domain.Entities;
using QuickCode.Demo.PricingEngineModule.Application.Interfaces.Repositories;
using QuickCode.Demo.PricingEngineModule.Application.Dtos.CommissionModel;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Application.Services.CommissionModel
{
    public partial interface ICommissionModelService
    {
        Task<Response<CommissionModelDto>> InsertAsync(CommissionModelDto request);
        Task<Response<bool>> DeleteAsync(CommissionModelDto request);
        Task<Response<bool>> UpdateAsync(int id, CommissionModelDto request);
        Task<Response<List<CommissionModelDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CommissionModelDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveResponseDto>>> GetActiveAsync(bool commissionModelIsActive, int? page, int? size);
        Task<Response<GetByNameResponseDto>> GetByNameAsync(string commissionModelName);
        Task<Response<int>> DeactivateAsync(int commissionModelId, DeactivateRequestDto updateRequest);
    }
}