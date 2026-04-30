using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OrderManagementModule.Domain.Entities;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.ShippingMethod;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Application.Services.ShippingMethod
{
    public partial interface IShippingMethodService
    {
        Task<Response<ShippingMethodDto>> InsertAsync(ShippingMethodDto request);
        Task<Response<bool>> DeleteAsync(ShippingMethodDto request);
        Task<Response<bool>> UpdateAsync(int id, ShippingMethodDto request);
        Task<Response<List<ShippingMethodDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ShippingMethodDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveResponseDto>>> GetActiveAsync(int? pageNumber, int? pageSize);
        Task<Response<int>> DeactivateAsync(int shippingMethodId, DeactivateRequestDto updateRequest);
    }
}