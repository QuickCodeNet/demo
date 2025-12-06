using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.ExpenseType;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.ExpenseType
{
    public partial class ExpenseTypeService : IExpenseTypeService
    {
        private readonly ILogger<ExpenseTypeService> _logger;
        private readonly IExpenseTypeRepository _repository;
        public ExpenseTypeService(ILogger<ExpenseTypeService> logger, IExpenseTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ExpenseTypeDto>> InsertAsync(ExpenseTypeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ExpenseTypeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ExpenseTypeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ExpenseTypeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ExpenseTypeDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetActiveExpenseTypesResponseDto>>> GetActiveExpenseTypesAsync(bool expenseTypesIsActive)
        {
            var returnValue = await _repository.GetActiveExpenseTypesAsync(expenseTypesIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetCommonExpensesForExpenseTypesResponseDto>>> GetCommonExpensesForExpenseTypesAsync(int expenseTypesId)
        {
            var returnValue = await _repository.GetCommonExpensesForExpenseTypesAsync(expenseTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetCommonExpensesForExpenseTypesResponseDto>> GetCommonExpensesForExpenseTypesDetailsAsync(int expenseTypesId, int commonExpensesId)
        {
            var returnValue = await _repository.GetCommonExpensesForExpenseTypesDetailsAsync(expenseTypesId, commonExpensesId);
            return returnValue.ToResponse();
        }
    }
}