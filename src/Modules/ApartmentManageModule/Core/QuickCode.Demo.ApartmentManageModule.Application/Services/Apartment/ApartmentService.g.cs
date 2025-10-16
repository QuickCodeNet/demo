using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Apartment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.Apartment
{
    public partial class ApartmentService : IApartmentService
    {
        private readonly ILogger<ApartmentService> _logger;
        private readonly IApartmentRepository _repository;
        public ApartmentService(ILogger<ApartmentService> logger, IApartmentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ApartmentDto>> InsertAsync(ApartmentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ApartmentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ApartmentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ApartmentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ApartmentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetApartmentsBySiteResponseDto>>> GetApartmentsBySiteAsync(int apartmentsSiteId, bool apartmentsIsActive)
        {
            var returnValue = await _repository.GetApartmentsBySiteAsync(apartmentsSiteId, apartmentsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetActiveApartmentsResponseDto>>> GetActiveApartmentsAsync(bool apartmentsIsActive)
        {
            var returnValue = await _repository.GetActiveApartmentsAsync(apartmentsIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatsForApartmentsResponseDto>>> GetFlatsForApartmentsAsync(int apartmentsId)
        {
            var returnValue = await _repository.GetFlatsForApartmentsAsync(apartmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatsForApartmentsResponseDto>> GetFlatsForApartmentsDetailsAsync(int apartmentsId, int flatsId)
        {
            var returnValue = await _repository.GetFlatsForApartmentsDetailsAsync(apartmentsId, flatsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForApartmentsResponseDto>>> GetFlatPaymentsForApartmentsAsync(int apartmentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForApartmentsAsync(apartmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForApartmentsResponseDto>> GetFlatPaymentsForApartmentsDetailsAsync(int apartmentsId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForApartmentsDetailsAsync(apartmentsId, flatPaymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCommonExpensesForApartmentsResponseDto>>> GetCommonExpensesForApartmentsAsync(int apartmentsId)
        {
            var returnValue = await _repository.GetCommonExpensesForApartmentsAsync(apartmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCommonExpensesForApartmentsResponseDto>> GetCommonExpensesForApartmentsDetailsAsync(int apartmentsId, int commonExpensesId)
        {
            var returnValue = await _repository.GetCommonExpensesForApartmentsDetailsAsync(apartmentsId, commonExpensesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetApartmentFeePlansForApartmentsResponseDto>>> GetApartmentFeePlansForApartmentsAsync(int apartmentsId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForApartmentsAsync(apartmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetApartmentFeePlansForApartmentsResponseDto>> GetApartmentFeePlansForApartmentsDetailsAsync(int apartmentsId, int apartmentFeePlansId)
        {
            var returnValue = await _repository.GetApartmentFeePlansForApartmentsDetailsAsync(apartmentsId, apartmentFeePlansId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetExpenseInstallmentsForApartmentsResponseDto>>> GetExpenseInstallmentsForApartmentsAsync(int apartmentsId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForApartmentsAsync(apartmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetExpenseInstallmentsForApartmentsResponseDto>> GetExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int expenseInstallmentsId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForApartmentsDetailsAsync(apartmentsId, expenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatExpenseInstallmentsForApartmentsResponseDto>>> GetFlatExpenseInstallmentsForApartmentsAsync(int apartmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForApartmentsAsync(apartmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatExpenseInstallmentsForApartmentsResponseDto>> GetFlatExpenseInstallmentsForApartmentsDetailsAsync(int apartmentsId, int flatExpenseInstallmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForApartmentsDetailsAsync(apartmentsId, flatExpenseInstallmentsId);
            return returnValue.ToResponse();
        }
    }
}