using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.SiteManager;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.SiteManager
{
    public partial class SiteManagerService : ISiteManagerService
    {
        private readonly ILogger<SiteManagerService> _logger;
        private readonly ISiteManagerRepository _repository;
        public SiteManagerService(ILogger<SiteManagerService> logger, ISiteManagerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<SiteManagerDto>> InsertAsync(SiteManagerDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(SiteManagerDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, SiteManagerDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SiteManagerDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<SiteManagerDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetSiteManagersResponseDto>>> GetSiteManagersAsync(int siteManagersSiteId, bool siteManagersIsActive)
        {
            var returnValue = await _repository.GetSiteManagersAsync(siteManagersSiteId, siteManagersIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetActiveManagerBySiteResponseDto>> GetActiveManagerBySiteAsync(int siteManagersSiteId, bool siteManagersIsActive)
        {
            var returnValue = await _repository.GetActiveManagerBySiteAsync(siteManagersSiteId, siteManagersIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetSiteManagerWithContactResponseDto>>> GetSiteManagerWithContactAsync(int siteManagersContactId)
        {
            var returnValue = await _repository.GetSiteManagerWithContactAsync(siteManagersContactId);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> CheckSiteHasManagerAsync(int siteManagersSiteId, bool siteManagersIsActive)
        {
            var returnValue = await _repository.CheckSiteHasManagerAsync(siteManagersSiteId, siteManagersIsActive);
            return returnValue.ToResponse();
        }
    }
}