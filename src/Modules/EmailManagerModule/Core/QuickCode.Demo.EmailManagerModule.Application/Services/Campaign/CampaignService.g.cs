using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.Campaign;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.Campaign
{
    public partial class CampaignService : ICampaignService
    {
        private readonly ILogger<CampaignService> _logger;
        private readonly ICampaignRepository _repository;
        public CampaignService(ILogger<CampaignService> logger, ICampaignRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CampaignDto>> InsertAsync(CampaignDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CampaignDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CampaignDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CampaignDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CampaignDto>> GetItemAsync(int id)
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

        public async Task<Response<GetByIdResponseDto>> GetByIdAsync(int campaignsId)
        {
            var returnValue = await _repository.GetByIdAsync(campaignsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveCampaignsResponseDto>>> GetActiveCampaignsAsync(bool campaignsIsActive)
        {
            var returnValue = await _repository.GetActiveCampaignsAsync(campaignsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetByNameResponseDto>> GetByNameAsync(string campaignsName)
        {
            var returnValue = await _repository.GetByNameAsync(campaignsName);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> ExistsByNameAsync(string campaignsName)
        {
            var returnValue = await _repository.ExistsByNameAsync(campaignsName);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetCampaignsCountAsync(bool campaignsIsActive)
        {
            var returnValue = await _repository.GetCampaignsCountAsync(campaignsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessagesForCampaignsResponseDto>>> GetMessagesForCampaignsAsync(int campaignsId)
        {
            var returnValue = await _repository.GetMessagesForCampaignsAsync(campaignsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessagesForCampaignsResponseDto>> GetMessagesForCampaignsDetailsAsync(int campaignsId, int messagesId)
        {
            var returnValue = await _repository.GetMessagesForCampaignsDetailsAsync(campaignsId, messagesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetMessageQueuesForCampaignsResponseDto>>> GetMessageQueuesForCampaignsAsync(int campaignsId)
        {
            var returnValue = await _repository.GetMessageQueuesForCampaignsAsync(campaignsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetMessageQueuesForCampaignsResponseDto>> GetMessageQueuesForCampaignsDetailsAsync(int campaignsId, int messageQueuesId)
        {
            var returnValue = await _repository.GetMessageQueuesForCampaignsDetailsAsync(campaignsId, messageQueuesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateStatusAsync(int campaignsId, UpdateStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateStatusAsync(campaignsId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdatePriorityAsync(int campaignsId, UpdatePriorityRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdatePriorityAsync(campaignsId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}