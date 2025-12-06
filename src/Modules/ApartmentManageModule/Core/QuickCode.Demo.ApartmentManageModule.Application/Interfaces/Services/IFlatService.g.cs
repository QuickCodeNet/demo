using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Flat;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.Flat
{
    public partial interface IFlatService
    {
        Task<Response<FlatDto>> InsertAsync(FlatDto request);
        Task<Response<bool>> DeleteAsync(FlatDto request);
        Task<Response<bool>> UpdateAsync(int id, FlatDto request);
        Task<Response<List<FlatDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<FlatDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetFlatsByApartmentResponseDto>>> GetFlatsByApartmentAsync(int flatsApartmentId, bool flatsIsActive);
        Task<Response<List<GetFlatsBySiteResponseDto>>> GetFlatsBySiteAsync(int flatsSiteId, bool flatsIsActive);
        Task<Response<List<GetFlatsWithContactsResponseDto>>> GetFlatsWithContactsAsync(int flatsId);
        Task<Response<List<GetVacantFlatsResponseDto>>> GetVacantFlatsAsync(bool flatsIsActive);
        Task<Response<List<GetRentedFlatsResponseDto>>> GetRentedFlatsAsync(bool flatsIsActive);
        Task<Response<GetFlatByNumberResponseDto>> GetFlatByNumberAsync(int flatsSiteId, string flatsFlatNumber);
        Task<Response<List<GetOwnedFlatsResponseDto>>> GetOwnedFlatsAsync(bool flatsIsActive);
        Task<Response<long>> GetFlatsCountBySiteAsync(int flatsSiteId, bool flatsIsActive);
        Task<Response<long>> GetFlatsCountByApartmentAsync(int flatsApartmentId, bool flatsIsActive);
        Task<Response<List<GetFlatContactsForFlatsResponseDto>>> GetFlatContactsForFlatsAsync(int flatsId);
        Task<Response<GetFlatContactsForFlatsResponseDto>> GetFlatContactsForFlatsDetailsAsync(int flatsId, int flatContactsId);
        Task<Response<List<GetFlatPaymentsForFlatsResponseDto>>> GetFlatPaymentsForFlatsAsync(int flatsId);
        Task<Response<GetFlatPaymentsForFlatsResponseDto>> GetFlatPaymentsForFlatsDetailsAsync(int flatsId, int flatPaymentsId);
        Task<Response<List<GetFlatExpenseInstallmentsForFlatsResponseDto>>> GetFlatExpenseInstallmentsForFlatsAsync(int flatsId);
        Task<Response<GetFlatExpenseInstallmentsForFlatsResponseDto>> GetFlatExpenseInstallmentsForFlatsDetailsAsync(int flatsId, int flatExpenseInstallmentsId);
        Task<Response<List<GetUserSiteAccessesForFlatsResponseDto>>> GetUserSiteAccessesForFlatsAsync(int flatsId);
        Task<Response<GetUserSiteAccessesForFlatsResponseDto>> GetUserSiteAccessesForFlatsDetailsAsync(int flatsId, int userSiteAccessesId);
    }
}