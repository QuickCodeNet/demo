using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FlatPayment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.FlatPayment
{
    public partial class FlatPaymentService : IFlatPaymentService
    {
        private readonly ILogger<FlatPaymentService> _logger;
        private readonly IFlatPaymentRepository _repository;
        public FlatPaymentService(ILogger<FlatPaymentService> logger, IFlatPaymentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<FlatPaymentDto>> InsertAsync(FlatPaymentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(FlatPaymentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, FlatPaymentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<FlatPaymentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<FlatPaymentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetPaymentsByFlatYearMonthResponseDto>>> GetPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            var returnValue = await _repository.GetPaymentsByFlatYearMonthAsync(flatPaymentsSiteId, flatPaymentsFlatId, flatPaymentsYearId, flatPaymentsMonthId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUnpaidPaymentsByFlatResponseDto>>> GetUnpaidPaymentsByFlatAsync(int flatPaymentsSiteId, int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            var returnValue = await _repository.GetUnpaidPaymentsByFlatAsync(flatPaymentsSiteId, flatPaymentsFlatId, flatPaymentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUnpaidPaymentsBySiteResponseDto>>> GetUnpaidPaymentsBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var returnValue = await _repository.GetUnpaidPaymentsBySiteAsync(flatPaymentsSiteId, flatPaymentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTotalCashInSafeResponseDto>>> GetTotalCashInSafeAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var returnValue = await _repository.GetTotalCashInSafeAsync(flatPaymentsSiteId, flatPaymentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetPendingPaymentsByFlatYearMonthResponseDto>>> GetPendingPaymentsByFlatYearMonthAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var returnValue = await _repository.GetPendingPaymentsByFlatYearMonthAsync(flatPaymentsSiteId, flatPaymentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsByMonthResponseDto>>> GetFlatPaymentsByMonthAsync(int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            var returnValue = await _repository.GetFlatPaymentsByMonthAsync(flatPaymentsFlatId, flatPaymentsYearId, flatPaymentsMonthId);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetPaymentsCountByFlatAsync(int flatPaymentsFlatId)
        {
            var returnValue = await _repository.GetPaymentsCountByFlatAsync(flatPaymentsFlatId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetTotalPaidAmountByFlatResponseDto>> GetTotalPaidAmountByFlatAsync(int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            var returnValue = await _repository.GetTotalPaidAmountByFlatAsync(flatPaymentsFlatId, flatPaymentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetUnpaidPaymentsCountBySiteAsync(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            var returnValue = await _repository.GetUnpaidPaymentsCountBySiteAsync(flatPaymentsSiteId, flatPaymentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> MarkPaymentAsPaidAsync(int flatPaymentsId, MarkPaymentAsPaidRequestDto updateRequest)
        {
            var returnValue = await _repository.MarkPaymentAsPaidAsync(flatPaymentsId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}