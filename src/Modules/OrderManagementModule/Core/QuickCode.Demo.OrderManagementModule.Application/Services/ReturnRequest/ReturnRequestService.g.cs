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
    public partial class ReturnRequestService : IReturnRequestService
    {
        private readonly ILogger<ReturnRequestService> _logger;
        private readonly IReturnRequestRepository _repository;
        public ReturnRequestService(ILogger<ReturnRequestService> logger, IReturnRequestRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ReturnRequestDto>> InsertAsync(ReturnRequestDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ReturnRequestDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ReturnRequestDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ReturnRequestDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ReturnRequestDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetByOrderIdResponseDto>>> GetByOrderIdAsync(int returnRequestOrderId, int? page, int? size)
        {
            var returnValue = await _repository.GetByOrderIdAsync(returnRequestOrderId, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetPendingReturnsBySellerResponseDto>>> GetPendingReturnsBySellerAsync(int ordersSellerId, ReturnStatus returnRequestsStatus, int? page, int? size)
        {
            var returnValue = await _repository.GetPendingReturnsBySellerAsync(ordersSellerId, returnRequestsStatus, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> ApproveAsync(int returnRequestId, ApproveRequestDto updateRequest)
        {
            var returnValue = await _repository.ApproveAsync(returnRequestId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RejectAsync(int returnRequestId, RejectRequestDto updateRequest)
        {
            var returnValue = await _repository.RejectAsync(returnRequestId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> CompleteAsync(int returnRequestId, CompleteRequestDto updateRequest)
        {
            var returnValue = await _repository.CompleteAsync(returnRequestId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}