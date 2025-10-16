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
    public partial class FlatContactService : IFlatContactService
    {
        private readonly ILogger<FlatContactService> _logger;
        private readonly IFlatContactRepository _repository;
        public FlatContactService(ILogger<FlatContactService> logger, IFlatContactRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<FlatContactDto>> InsertAsync(FlatContactDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(FlatContactDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, FlatContactDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<FlatContactDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<FlatContactDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetFlatOwnersResponseDto>>> GetFlatOwnersAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var returnValue = await _repository.GetFlatOwnersAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatTenantsResponseDto>>> GetFlatTenantsAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var returnValue = await _repository.GetFlatTenantsAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetContactFlatsResponseDto>>> GetContactFlatsAsync(int flatContactsContactId, bool flatContactsIsActive)
        {
            var returnValue = await _repository.GetContactFlatsAsync(flatContactsContactId, flatContactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetContactOwnedFlatsResponseDto>>> GetContactOwnedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var returnValue = await _repository.GetContactOwnedFlatsAsync(flatContactsContactId, flatContactsRelationshipType, flatContactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetContactRentedFlatsResponseDto>>> GetContactRentedFlatsAsync(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var returnValue = await _repository.GetContactRentedFlatsAsync(flatContactsContactId, flatContactsRelationshipType, flatContactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> CheckFlatHasOwnerAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var returnValue = await _repository.CheckFlatHasOwnerAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> CheckFlatHasTenantAsync(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            var returnValue = await _repository.CheckFlatHasTenantAsync(flatContactsFlatId, flatContactsRelationshipType, flatContactsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetFlatContactsCountAsync(int flatContactsFlatId, bool flatContactsIsActive)
        {
            var returnValue = await _repository.GetFlatContactsCountAsync(flatContactsFlatId, flatContactsIsActive);
            return returnValue.ToResponse();
        }
    }
}