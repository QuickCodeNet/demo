using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ApartmentFeePlan;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.ApartmentFeePlan
{
    public partial class ApartmentFeePlanService : IApartmentFeePlanService
    {
        private readonly ILogger<ApartmentFeePlanService> _logger;
        private readonly IApartmentFeePlanRepository _repository;
        public ApartmentFeePlanService(ILogger<ApartmentFeePlanService> logger, IApartmentFeePlanRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ApartmentFeePlanDto>> InsertAsync(ApartmentFeePlanDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ApartmentFeePlanDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ApartmentFeePlanDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ApartmentFeePlanDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ApartmentFeePlanDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetFeePlanByYearMonthResponseDto>>> GetFeePlanByYearMonthAsync(int apartmentFeePlansSiteId, int apartmentFeePlansApartmentId, int apartmentFeePlansYearId, int apartmentFeePlansMonthId, bool apartmentFeePlansIsActive)
        {
            var returnValue = await _repository.GetFeePlanByYearMonthAsync(apartmentFeePlansSiteId, apartmentFeePlansApartmentId, apartmentFeePlansYearId, apartmentFeePlansMonthId, apartmentFeePlansIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFeePlansBySiteResponseDto>>> GetFeePlansBySiteAsync(int apartmentFeePlansSiteId, bool apartmentFeePlansIsActive)
        {
            var returnValue = await _repository.GetFeePlansBySiteAsync(apartmentFeePlansSiteId, apartmentFeePlansIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForApartmentFeePlansResponseDto>>> GetFlatPaymentsForApartmentFeePlansAsync(int apartmentFeePlansId)
        {
            var returnValue = await _repository.GetFlatPaymentsForApartmentFeePlansAsync(apartmentFeePlansId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForApartmentFeePlansResponseDto>> GetFlatPaymentsForApartmentFeePlansDetailsAsync(int apartmentFeePlansId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForApartmentFeePlansDetailsAsync(apartmentFeePlansId, flatPaymentsId);
            return returnValue.ToResponse();
        }
    }
}