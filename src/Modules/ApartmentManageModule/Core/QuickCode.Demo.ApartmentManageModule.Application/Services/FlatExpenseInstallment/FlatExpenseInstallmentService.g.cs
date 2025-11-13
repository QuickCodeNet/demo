using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.FlatExpenseInstallment
{
    public partial class FlatExpenseInstallmentService : IFlatExpenseInstallmentService
    {
        private readonly ILogger<FlatExpenseInstallmentService> _logger;
        private readonly IFlatExpenseInstallmentRepository _repository;
        public FlatExpenseInstallmentService(ILogger<FlatExpenseInstallmentService> logger, IFlatExpenseInstallmentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<FlatExpenseInstallmentDto>> InsertAsync(FlatExpenseInstallmentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(FlatExpenseInstallmentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, FlatExpenseInstallmentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<FlatExpenseInstallmentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<FlatExpenseInstallmentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetFlatExpenseInstallmentsResponseDto>>> GetFlatExpenseInstallmentsAsync(int flatExpenseInstallmentsFlatId, int flatExpenseInstallmentsExpenseId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsExpenseId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatUnpaidInstallmentsResponseDto>>> GetFlatUnpaidInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var returnValue = await _repository.GetFlatUnpaidInstallmentsAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatOverdueInstallmentsResponseDto>>> GetFlatOverdueInstallmentsAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var returnValue = await _repository.GetFlatOverdueInstallmentsAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetApartmentFlatInstallmentsResponseDto>>> GetApartmentFlatInstallmentsAsync(int flatExpenseInstallmentsSiteId, int flatExpenseInstallmentsApartmentId)
        {
            var returnValue = await _repository.GetApartmentFlatInstallmentsAsync(flatExpenseInstallmentsSiteId, flatExpenseInstallmentsApartmentId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatTotalDebtResponseDto>>> GetFlatTotalDebtAsync(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            var returnValue = await _repository.GetFlatTotalDebtAsync(flatExpenseInstallmentsFlatId, flatExpenseInstallmentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> MarkFlatInstallmentAsPaidAsync(int flatExpenseInstallmentsId, MarkFlatInstallmentAsPaidRequestDto updateRequest)
        {
            var returnValue = await _repository.MarkFlatInstallmentAsPaidAsync(flatExpenseInstallmentsId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}