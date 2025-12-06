using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatContact;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.FlatContact
{
    public partial interface IFlatContactService
    {
        Task<Response<FlatContactDto>> InsertAsync(FlatContactDto request);
        Task<Response<bool>> DeleteAsync(FlatContactDto request);
        Task<Response<bool>> UpdateAsync(int id, FlatContactDto request);
        Task<Response<List<FlatContactDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<FlatContactDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetFlatOwnersResponseDto>>> GetFlatOwnersAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive);
        Task<Response<List<GetFlatTenantsResponseDto>>> GetFlatTenantsAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive);
        Task<Response<List<GetContactFlatsResponseDto>>> GetContactFlatsAsync(int flatContactsContactId, bool flatContactsIsActive);
        Task<Response<List<GetContactOwnedFlatsResponseDto>>> GetContactOwnedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive);
        Task<Response<List<GetContactRentedFlatsResponseDto>>> GetContactRentedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive);
        Task<Response<bool>> CheckFlatHasOwnerAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive);
        Task<Response<bool>> CheckFlatHasTenantAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive);
        Task<Response<long>> GetFlatContactsCountAsync(int flatContactsFlatId, bool flatContactsIsActive);
    }
}