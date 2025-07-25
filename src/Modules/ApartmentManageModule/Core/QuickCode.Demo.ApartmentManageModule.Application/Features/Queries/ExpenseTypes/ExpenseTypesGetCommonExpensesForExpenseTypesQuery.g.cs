using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Features
{
    public class ExpenseTypesGetCommonExpensesForExpenseTypesQuery : IRequest<Response<List<ExpenseTypesGetCommonExpensesForExpenseTypesResponseDto>>>
    {
        public int ExpenseTypesId { get; set; }

        public ExpenseTypesGetCommonExpensesForExpenseTypesQuery(int expenseTypesId)
        {
            this.ExpenseTypesId = expenseTypesId;
        }

        public class ExpenseTypesGetCommonExpensesForExpenseTypesHandler : IRequestHandler<ExpenseTypesGetCommonExpensesForExpenseTypesQuery, Response<List<ExpenseTypesGetCommonExpensesForExpenseTypesResponseDto>>>
        {
            private readonly ILogger<ExpenseTypesGetCommonExpensesForExpenseTypesHandler> _logger;
            private readonly IExpenseTypesRepository _repository;
            public ExpenseTypesGetCommonExpensesForExpenseTypesHandler(ILogger<ExpenseTypesGetCommonExpensesForExpenseTypesHandler> logger, IExpenseTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseTypesGetCommonExpensesForExpenseTypesResponseDto>>> Handle(ExpenseTypesGetCommonExpensesForExpenseTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseTypesGetCommonExpensesForExpenseTypesAsync(request.ExpenseTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}