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
    public partial class SellerService : ISellerService
    {
        private readonly ILogger<SellerService> _logger;
        private readonly ISellerRepository _repository;
        public SellerService(ILogger<SellerService> logger, ISellerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SellerDto>> InsertAsync(SellerDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SellerDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SellerDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SellerDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SellerDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByUserIdResponseDto>> GetByUserIdAsync(int sellerUserId)
        {
            var returnValue = await _repository.GetByUserIdAsync(sellerUserId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetByStatusResponseDto>>> GetByStatusAsync(SellerStatus sellerStatus, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetByStatusAsync(sellerStatus, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SearchByCompanyNameResponseDto>>> SearchByCompanyNameAsync(string sellerCompanyName, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.SearchByCompanyNameAsync(sellerCompanyName, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetPendingVerificationCountAsync()
        {
            var returnValue = await _repository.GetPendingVerificationCountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> ApproveAsync(int sellerId, ApproveRequestDto updateRequest)
        {
            var returnValue = await _repository.ApproveAsync(sellerId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> SuspendAsync(int sellerId, SuspendRequestDto updateRequest)
        {
            var returnValue = await _repository.SuspendAsync(sellerId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RejectAsync(int sellerId, RejectRequestDto updateRequest)
        {
            var returnValue = await _repository.RejectAsync(sellerId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateTierAsync(int sellerId, UpdateTierRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateTierAsync(sellerId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}