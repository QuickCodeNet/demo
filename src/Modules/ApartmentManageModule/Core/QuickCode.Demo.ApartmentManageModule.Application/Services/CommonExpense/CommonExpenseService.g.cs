using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.CommonExpense;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.CommonExpense
{
    public partial class CommonExpenseService : ICommonExpenseService
    {
        private readonly ILogger<CommonExpenseService> _logger;
        private readonly ICommonExpenseRepository _repository;
        public CommonExpenseService(ILogger<CommonExpenseService> logger, ICommonExpenseRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CommonExpenseDto>> InsertAsync(CommonExpenseDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CommonExpenseDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CommonExpenseDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CommonExpenseDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CommonExpenseDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetExpensesByApartmentMonthResponseDto>>> GetExpensesByApartmentMonthAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            var returnValue = await _repository.GetExpensesByApartmentMonthAsync(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesYearId, commonExpensesMonthId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetExpensesBySiteResponseDto>>> GetExpensesBySiteAsync(int commonExpensesSiteId)
        {
            var returnValue = await _repository.GetExpensesBySiteAsync(commonExpensesSiteId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetExpensesSummaryByYearResponseDto>>> GetExpensesSummaryByYearAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId)
        {
            var returnValue = await _repository.GetExpensesSummaryByYearAsync(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesYearId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetExpensesByTypeResponseDto>>> GetExpensesByTypeAsync(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesExpenseTypeId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            var returnValue = await _repository.GetExpensesByTypeAsync(commonExpensesSiteId, commonExpensesApartmentId, commonExpensesExpenseTypeId, commonExpensesYearId, commonExpensesMonthId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUnpaidExpensesByApartmentResponseDto>>> GetUnpaidExpensesByApartmentAsync(int commonExpensesApartmentId, bool commonExpensesPaid)
        {
            var returnValue = await _repository.GetUnpaidExpensesByApartmentAsync(commonExpensesApartmentId, commonExpensesPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<long>> GetExpensesCountByApartmentAsync(int commonExpensesApartmentId)
        {
            var returnValue = await _repository.GetExpensesCountByApartmentAsync(commonExpensesApartmentId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetTotalExpenseAmountByApartmentResponseDto>> GetTotalExpenseAmountByApartmentAsync(int commonExpensesApartmentId)
        {
            var returnValue = await _repository.GetTotalExpenseAmountByApartmentAsync(commonExpensesApartmentId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForCommonExpensesResponseDto>>> GetFlatPaymentsForCommonExpensesAsync(int commonExpensesId)
        {
            var returnValue = await _repository.GetFlatPaymentsForCommonExpensesAsync(commonExpensesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForCommonExpensesResponseDto>> GetFlatPaymentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForCommonExpensesDetailsAsync(commonExpensesId, flatPaymentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetExpenseInstallmentsForCommonExpensesResponseDto>>> GetExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForCommonExpensesAsync(commonExpensesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetExpenseInstallmentsForCommonExpensesResponseDto>> GetExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int expenseInstallmentsId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsForCommonExpensesDetailsAsync(commonExpensesId, expenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatExpenseInstallmentsForCommonExpensesResponseDto>>> GetFlatExpenseInstallmentsForCommonExpensesAsync(int commonExpensesId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForCommonExpensesAsync(commonExpensesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatExpenseInstallmentsForCommonExpensesResponseDto>> GetFlatExpenseInstallmentsForCommonExpensesDetailsAsync(int commonExpensesId, int flatExpenseInstallmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForCommonExpensesDetailsAsync(commonExpensesId, flatExpenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> MarkExpenseAsPaidAsync(int commonExpensesId, MarkExpenseAsPaidRequestDto updateRequest)
        {
            var returnValue = await _repository.MarkExpenseAsPaidAsync(commonExpensesId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}