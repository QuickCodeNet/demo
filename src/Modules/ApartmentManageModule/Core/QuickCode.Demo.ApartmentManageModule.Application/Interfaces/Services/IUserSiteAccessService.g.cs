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
    public partial interface IUserSiteAccessService
    {
        Task<Response<UserSiteAccessDto>> InsertAsync(UserSiteAccessDto request);
        Task<Response<bool>> DeleteAsync(UserSiteAccessDto request);
        Task<Response<bool>> UpdateAsync(int id, UserSiteAccessDto request);
        Task<Response<List<UserSiteAccessDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<UserSiteAccessDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetUserSitesResponseDto>>> GetUserSitesAsync(int userSiteAccessesUserId, bool userSiteAccessesIsActive);
        Task<Response<List<GetSiteUsersResponseDto>>> GetSiteUsersAsync(int userSiteAccessesSiteId, bool userSiteAccessesIsActive);
        Task<Response<List<GetUserFlatsResponseDto>>> GetUserFlatsAsync(int userSiteAccessesUserId, bool userSiteAccessesIsActive);
    }
}