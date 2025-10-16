using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.CampaignType;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.CampaignType
{
    public partial class CampaignTypeService : ICampaignTypeService
    {
        private readonly ILogger<CampaignTypeService> _logger;
        private readonly ICampaignTypeRepository _repository;
        public CampaignTypeService(ILogger<CampaignTypeService> logger, ICampaignTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CampaignTypeDto>> InsertAsync(CampaignTypeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CampaignTypeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CampaignTypeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CampaignTypeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CampaignTypeDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetCampaignMessagesForCampaignTypesResponseDto>>> GetCampaignMessagesForCampaignTypesAsync(int campaignTypesId)
        {
            var returnValue = await _repository.GetCampaignMessagesForCampaignTypesAsync(campaignTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCampaignMessagesForCampaignTypesResponseDto>> GetCampaignMessagesForCampaignTypesDetailsAsync(int campaignTypesId, int campaignMessagesId)
        {
            var returnValue = await _repository.GetCampaignMessagesForCampaignTypesDetailsAsync(campaignTypesId, campaignMessagesId);
            return returnValue.ToResponse();
        }
    }
}