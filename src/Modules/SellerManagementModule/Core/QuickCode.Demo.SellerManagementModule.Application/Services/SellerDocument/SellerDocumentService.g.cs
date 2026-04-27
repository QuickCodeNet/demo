using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SellerManagementModule.Domain.Entities;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.SellerDocument;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Application.Services.SellerDocument
{
    public partial class SellerDocumentService : ISellerDocumentService
    {
        private readonly ILogger<SellerDocumentService> _logger;
        private readonly ISellerDocumentRepository _repository;
        public SellerDocumentService(ILogger<SellerDocumentService> logger, ISellerDocumentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SellerDocumentDto>> InsertAsync(SellerDocumentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SellerDocumentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SellerDocumentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SellerDocumentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SellerDocumentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerDocumentSellerId, int? page, int? size)
        {
            var returnValue = await _repository.GetBySellerIdAsync(sellerDocumentSellerId, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetPendingDocumentsResponseDto>>> GetPendingDocumentsAsync(VerificationStatus sellerDocumentStatus, int? page, int? size)
        {
            var returnValue = await _repository.GetPendingDocumentsAsync(sellerDocumentStatus, page, size);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> VerifyAsync(int sellerDocumentId, VerifyRequestDto updateRequest)
        {
            var returnValue = await _repository.VerifyAsync(sellerDocumentId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RejectAsync(int sellerDocumentId, RejectRequestDto updateRequest)
        {
            var returnValue = await _repository.RejectAsync(sellerDocumentId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}