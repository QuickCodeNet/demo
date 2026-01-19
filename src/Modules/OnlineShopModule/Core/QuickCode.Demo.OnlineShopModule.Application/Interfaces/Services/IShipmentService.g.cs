using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Shipment;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Shipment
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
        Task<Response<List<GetUndeliveredShipmentsResponseDto>>> GetUndeliveredShipmentsAsync(CargoCompany shipmentsCargoCompany);
        Task<Response<List<TrackShipmentResponseDto>>> TrackShipmentAsync(string shipmentsTrackingCode, int? page, int? size);
        Task<Response<int>> MarkShipmentDeliveredAsync(int shipmentsId, MarkShipmentDeliveredRequestDto updateRequest);
    }
}