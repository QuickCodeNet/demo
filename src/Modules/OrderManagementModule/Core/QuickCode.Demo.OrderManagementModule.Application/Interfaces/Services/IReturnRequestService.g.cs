using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OrderManagementModule.Domain.Entities;
using QuickCode.Demo.OrderManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OrderManagementModule.Application.Dtos.ReturnRequest;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Application.Services.ReturnRequest
{
    public partial interface IReturnRequestService
    {
        Task<Response<ReturnRequestDto>> InsertAsync(ReturnRequestDto request);
        Task<Response<bool>> DeleteAsync(ReturnRequestDto request);
        Task<Response<bool>> UpdateAsync(int id, ReturnRequestDto request);
        Task<Response<List<ReturnRequestDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ReturnRequestDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetByOrderIdResponseDto>>> GetByOrderIdAsync(int returnRequestOrderId, int? pageNumber, int? pageSize);
        Task<Response<List<GetPendingReturnsBySellerResponseDto>>> GetPendingReturnsBySellerAsync(int ordersSellerId, int? pageNumber, int? pageSize);
        Task<Response<int>> ApproveAsync(int returnRequestId, ApproveRequestDto updateRequest);
        Task<Response<int>> RejectAsync(int returnRequestId, RejectRequestDto updateRequest);
        Task<Response<int>> CompleteAsync(int returnRequestId, CompleteRequestDto updateRequest);
    }
}