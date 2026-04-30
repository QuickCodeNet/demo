using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SellerManagementModule.Domain.Entities;
using QuickCode.Demo.SellerManagementModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SellerManagementModule.Application.Dtos.SellerBankAccount;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Application.Services.SellerBankAccount
{
    public partial class SellerBankAccountService : ISellerBankAccountService
    {
        private readonly ILogger<SellerBankAccountService> _logger;
        private readonly ISellerBankAccountRepository _repository;
        public SellerBankAccountService(ILogger<SellerBankAccountService> logger, ISellerBankAccountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SellerBankAccountDto>> InsertAsync(SellerBankAccountDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SellerBankAccountDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SellerBankAccountDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SellerBankAccountDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SellerBankAccountDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetBySellerIdResponseDto>>> GetBySellerIdAsync(int sellerBankAccountSellerId, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetBySellerIdAsync(sellerBankAccountSellerId, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetDefaultBySellerIdResponseDto>> GetDefaultBySellerIdAsync(int sellerBankAccountSellerId)
        {
            var returnValue = await _repository.GetDefaultBySellerIdAsync(sellerBankAccountSellerId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> SetAsDefaultAsync(int sellerBankAccountSellerId, SetAsDefaultRequestDto updateRequest)
        {
            var returnValue = await _repository.SetAsDefaultAsync(sellerBankAccountSellerId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> SetDefaultAccountAsync(int sellerBankAccountId, SetDefaultAccountRequestDto updateRequest)
        {
            var returnValue = await _repository.SetDefaultAccountAsync(sellerBankAccountId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}