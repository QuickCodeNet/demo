using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ExpenseInstallment;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.ExpenseInstallment
{
    public partial class ExpenseInstallmentService : IExpenseInstallmentService
    {
        private readonly ILogger<ExpenseInstallmentService> _logger;
        private readonly IExpenseInstallmentRepository _repository;
        public ExpenseInstallmentService(ILogger<ExpenseInstallmentService> logger, IExpenseInstallmentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ExpenseInstallmentDto>> InsertAsync(ExpenseInstallmentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ExpenseInstallmentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ExpenseInstallmentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ExpenseInstallmentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ExpenseInstallmentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetExpenseInstallmentsResponseDto>>> GetExpenseInstallmentsAsync(int expenseInstallmentsExpenseId)
        {
            var returnValue = await _repository.GetExpenseInstallmentsAsync(expenseInstallmentsExpenseId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUnpaidInstallmentsResponseDto>>> GetUnpaidInstallmentsAsync(int expenseInstallmentsExpenseId, bool expenseInstallmentsPaid)
        {
            var returnValue = await _repository.GetUnpaidInstallmentsAsync(expenseInstallmentsExpenseId, expenseInstallmentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOverdueInstallmentsResponseDto>>> GetOverdueInstallmentsAsync(bool expenseInstallmentsPaid)
        {
            var returnValue = await _repository.GetOverdueInstallmentsAsync(expenseInstallmentsPaid);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetApartmentInstallmentsResponseDto>>> GetApartmentInstallmentsAsync(int expenseInstallmentsSiteId, int expenseInstallmentsApartmentId)
        {
            var returnValue = await _repository.GetApartmentInstallmentsAsync(expenseInstallmentsSiteId, expenseInstallmentsApartmentId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetSiteInstallmentsResponseDto>>> GetSiteInstallmentsAsync(int expenseInstallmentsSiteId)
        {
            var returnValue = await _repository.GetSiteInstallmentsAsync(expenseInstallmentsSiteId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>>> GetFlatExpenseInstallmentsForExpenseInstallmentsAsync(int expenseInstallmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForExpenseInstallmentsAsync(expenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>> GetFlatExpenseInstallmentsForExpenseInstallmentsDetailsAsync(int expenseInstallmentsId, int flatExpenseInstallmentsId)
        {
            var returnValue = await _repository.GetFlatExpenseInstallmentsForExpenseInstallmentsDetailsAsync(expenseInstallmentsId, flatExpenseInstallmentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> MarkInstallmentAsPaidAsync(int expenseInstallmentsId, MarkInstallmentAsPaidRequestDto updateRequest)
        {
            var returnValue = await _repository.MarkInstallmentAsPaidAsync(expenseInstallmentsId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}