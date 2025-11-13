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
    public partial class FlatService : IFlatService
    {
        private readonly ILogger<FlatService> _logger;
        private readonly IFlatRepository _repository;
        public FlatService(ILogger<FlatService> logger, IFlatRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<FlatDto>> InsertAsync(FlatDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(FlatDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, FlatDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<FlatDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<FlatDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetFlatsByApartmentResponseDto>>> GetFlatsByApartmentAsync(int flatsApartmentId, bool flatsIsActive)
        {
            var returnValue = await _repository.GetFlatsByApartmentAsync(flatsApartmentId, flatsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatsBySiteResponseDto>>> GetFlatsBySiteAsync(int flatsSiteId, bool flatsIsActive)
        {
            var returnValue = await _repository.GetFlatsBySiteAsync(flatsSiteId, flatsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatsWithContactsResponseDto>>> GetFlatsWithContactsAsync(int flatsId)
        {
            var returnValue = await _repository.GetFlatsWithContactsAsync(flatsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetVacantFlatsResponseDto>>> GetVacantFlatsAsync(bool flatsIsActive)
        {
            var returnValue = await _repository.GetVacantFlatsAsync(flatsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetRentedFlatsResponseDto>>> GetRentedFlatsAsync(bool flatsIsActive)
        {
            var returnValue = await _repository.GetRentedFlatsAsync(flatsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatByNumberResponseDto>> GetFlatByNumberAsync(int flatsSiteId, string flatsFlatNumber)
        {
            var returnValue = await _repository.GetFlatByNumberAsync(flatsSiteId, flatsFlatNumber);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOwnedFlatsResponseDto>>> GetOwnedFlatsAsync(bool flatsIsActive)
        {
            var returnValue = await _repository.GetOwnedFlatsAsync(flatsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetFlatsCountBySiteAsync(int flatsSiteId, bool flatsIsActive)
        {
            var returnValue = await _repository.GetFlatsCountBySiteAsync(flatsSiteId, flatsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetFlatsCountByApartmentAsync(int flatsApartmentId, bool flatsIsActive)
        {
            var returnValue = await _repository.GetFlatsCountByApartmentAsync(flatsApartmentId, flatsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatContactsForFlatsResponseDto>>> GetFlatContactsForFlatsAsync(int flatsId)
        {
            var returnValue = await _repository.GetFlatContactsForFlatsAsync(flatsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatContactsForFlatsResponseDto>> GetFlatContactsForFlatsDetailsAsync(int flatsId, int flatContactsId)
        {
            var returnValue = await _repository.GetFlatContactsForFlatsDetailsAsync(flatsId, flatContactsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForFlatsResponseDto>>> GetFlatPaymentsForFlatsAsync(int flatsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForFlatsAsync(flatsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForFlatsResponseDto>> GetFlatPaymentsForFlatsDetailsAsync(int flatsId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForFlatsDetailsAsync(flatsId, flatPaymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatExpenseInstallmentsForFlatsResponseDto>>> GetFlatExpenseInstallmentsForFlatsAsync(int flatsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForFlatsAsync(flatsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatExpenseInstallmentsForFlatsResponseDto>> GetFlatExpenseInstallmentsForFlatsDetailsAsync(int flatsId, int flatExpenseInstallmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForFlatsDetailsAsync(flatsId, flatExpenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUserSiteAccessesForFlatsResponseDto>>> GetUserSiteAccessesForFlatsAsync(int flatsId)
        {
            var returnValue = await _repository.GetUserSiteAccessesForFlatsAsync(flatsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetUserSiteAccessesForFlatsResponseDto>> GetUserSiteAccessesForFlatsDetailsAsync(int flatsId, int userSiteAccessesId)
        {
            var returnValue = await _repository.GetUserSiteAccessesForFlatsDetailsAsync(flatsId, userSiteAccessesId);
            return returnValue.ToResponse();
        }
    }
}