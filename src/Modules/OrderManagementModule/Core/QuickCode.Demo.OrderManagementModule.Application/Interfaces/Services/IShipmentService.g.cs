using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OrderManagementModule.Domain.Entities;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.Shipment;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Application.Services.Shipment
{
    public partial interface IShipmentService
    {
        Task<Response<ShipmentDto>> InsertAsync(ShipmentDto request);
        Task<Response<bool>> DeleteAsync(ShipmentDto request);
        Task<Response<bool>> UpdateAsync(int id, ShipmentDto request);
        Task<Response<List<ShipmentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ShipmentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByOrderIdResponseDto>>> GetByOrderIdAsync(int shipmentOrderId, int? page, int? size);
        Task<Response<GetByTrackingNumberResponseDto>> GetByTrackingNumberAsync(string shipmentTrackingNumber);
        Task<Response<int>> MarkAsShippedAsync(int shipmentId, MarkAsShippedRequestDto updateRequest);
        Task<Response<int>> MarkAsDeliveredAsync(int shipmentId, MarkAsDeliveredRequestDto updateRequest);
    }
}