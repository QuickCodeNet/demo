using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Campaign;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Campaign
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

        public async Task<Response<List<GetActiveCampaignsResponseDto>>> GetActiveCampaignsAsync(bool campaignsIsActive)
        {
            var returnValue = await _repository.GetActiveCampaignsAsync(campaignsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCouponsForCampaignsResponseDto>>> GetCouponsForCampaignsAsync(int campaignsId)
        {
            var returnValue = await _repository.GetCouponsForCampaignsAsync(campaignsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCouponsForCampaignsResponseDto>> GetCouponsForCampaignsDetailsAsync(int campaignsId, int couponsId)
        {
            var returnValue = await _repository.GetCouponsForCampaignsDetailsAsync(campaignsId, couponsId);
            return returnValue.ToResponse();
        }
    }
}