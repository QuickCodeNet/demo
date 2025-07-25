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
    public class ExpenseTypesGetCommonExpensesForExpenseTypesDetailsQuery : IRequest<Response<ExpenseTypesGetCommonExpensesForExpenseTypesResponseDto>>
    {
        public int ExpenseTypesId { get; set; }
        public int CommonExpensesId { get; set; }

        public ExpenseTypesGetCommonExpensesForExpenseTypesDetailsQuery(int expenseTypesId, int commonExpensesId)
        {
            this.ExpenseTypesId = expenseTypesId;
            this.CommonExpensesId = commonExpensesId;
        }

        public class ExpenseTypesGetCommonExpensesForExpenseTypesDetailsHandler : IRequestHandler<ExpenseTypesGetCommonExpensesForExpenseTypesDetailsQuery, Response<ExpenseTypesGetCommonExpensesForExpenseTypesResponseDto>>
        {
            private readonly ILogger<ExpenseTypesGetCommonExpensesForExpenseTypesDetailsHandler> _logger;
            private readonly IExpenseTypesRepository _repository;
            public ExpenseTypesGetCommonExpensesForExpenseTypesDetailsHandler(ILogger<ExpenseTypesGetCommonExpensesForExpenseTypesDetailsHandler> logger, IExpenseTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ExpenseTypesGetCommonExpensesForExpenseTypesResponseDto>> Handle(ExpenseTypesGetCommonExpensesForExpenseTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseTypesGetCommonExpensesForExpenseTypesDetailsAsync(request.ExpenseTypesId, request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}