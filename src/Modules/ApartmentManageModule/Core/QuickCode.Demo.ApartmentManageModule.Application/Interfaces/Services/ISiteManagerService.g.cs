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
    public partial interface ISiteManagerService
    {
        Task<Response<SiteManagerDto>> InsertAsync(SiteManagerDto request);
        Task<Response<bool>> DeleteAsync(SiteManagerDto request);
        Task<Response<bool>> UpdateAsync(int id, SiteManagerDto request);
        Task<Response<List<SiteManagerDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<SiteManagerDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetSiteManagersResponseDto>>> GetSiteManagersAsync(int siteManagersSiteId, bool siteManagersIsActive);
        Task<Response<GetActiveManagerBySiteResponseDto>> GetActiveManagerBySiteAsync(int siteManagersSiteId, bool siteManagersIsActive);
        Task<Response<List<GetSiteManagerWithContactResponseDto>>> GetSiteManagerWithContactAsync(int siteManagersContactId);
        Task<Response<bool>> CheckSiteHasManagerAsync(int siteManagersSiteId, bool siteManagersIsActive);
    }
}