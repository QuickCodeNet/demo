using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.BlackList;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.BlackList
{
    public partial class BlackListService : IBlackListService
    {
        private readonly ILogger<BlackListService> _logger;
        private readonly IBlackListRepository _repository;
        public BlackListService(ILogger<BlackListService> logger, IBlackListRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<BlackListDto>> InsertAsync(BlackListDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(BlackListDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, BlackListDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<BlackListDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<BlackListDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int blackListsId)
        {
            var returnValue = await _repository.GetByIdAsync(blackListsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetByRecipientResponseDto>> GetByRecipientAsync(string blackListsRecipient)
        {
            var returnValue = await _repository.GetByRecipientAsync(blackListsRecipient);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> ExistsByRecipientAsync(string blackListsRecipient)
        {
            var returnValue = await _repository.ExistsByRecipientAsync(blackListsRecipient);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetBlacklistCountAsync(BlacklistReasonType blackListsReasonType)
        {
            var returnValue = await _repository.GetBlacklistCountAsync(blackListsReasonType);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> AddToBlacklistAsync(string blackListsRecipient, AddToBlacklistRequestDto updateRequest)
        {
            var returnValue = await _repository.AddToBlacklistAsync(blackListsRecipient, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> RemoveFromBlacklistAsync(int blackListsId)
        {
            var returnValue = await _repository.RemoveFromBlacklistAsync(blackListsId);
            return returnValue.ToResponse();
        }
    }
}