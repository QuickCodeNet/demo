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
    public partial interface ISellerDocumentService
    {
        Task<Response<SellerDocumentDto>> InsertAsync(SellerDocumentDto request);
        Task<Response<bool>> DeleteAsync(SellerDocumentDto request);
        Task<Response<bool>> UpdateAsync(int id, SellerDocumentDto request);
        Task<Response<List<SellerDocumentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SellerDocumentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerDocumentSellerId, int? page, int? size);
        Task<Response<List<GetPendingDocumentsResponseDto>>> GetPendingDocumentsAsync(VerificationStatus sellerDocumentStatus, int? page, int? size);
        Task<Response<int>> VerifyAsync(int sellerDocumentId, VerifyRequestDto updateRequest);
        Task<Response<int>> RejectAsync(int sellerDocumentId, RejectRequestDto updateRequest);
    }
}