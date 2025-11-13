using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.UserSiteAccess;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.UserSiteAccess
{
    public partial class UserSiteAccessService : IUserSiteAccessService
    {
        private readonly ILogger<UserSiteAccessService> _logger;
        private readonly IUserSiteAccessRepository _repository;
        public UserSiteAccessService(ILogger<UserSiteAccessService> logger, IUserSiteAccessRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<UserSiteAccessDto>> InsertAsync(UserSiteAccessDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(UserSiteAccessDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, UserSiteAccessDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<UserSiteAccessDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<UserSiteAccessDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetUserSitesResponseDto>>> GetUserSitesAsync(int userSiteAccessesUserId, bool userSiteAccessesIsActive)
        {
            var returnValue = await _repository.GetUserSitesAsync(userSiteAccessesUserId, userSiteAccessesIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetSiteUsersResponseDto>>> GetSiteUsersAsync(int userSiteAccessesSiteId, bool userSiteAccessesIsActive)
        {
            var returnValue = await _repository.GetSiteUsersAsync(userSiteAccessesSiteId, userSiteAccessesIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUserFlatsResponseDto>>> GetUserFlatsAsync(int userSiteAccessesUserId, bool userSiteAccessesIsActive)
        {
            var returnValue = await _repository.GetUserFlatsAsync(userSiteAccessesUserId, userSiteAccessesIsActive);
            return returnValue.ToResponse();
        }
    }
}