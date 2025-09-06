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
    public class ExpenseInstallmentsGetExpenseInstallmentsQuery : IRequest<Response<List<ExpenseInstallmentsGetExpenseInstallmentsResponseDto>>>
    {
        public int ExpenseInstallmentsExpenseId { get; set; }

        public ExpenseInstallmentsGetExpenseInstallmentsQuery(int expenseInstallmentsExpenseId)
        {
            this.ExpenseInstallmentsExpenseId = expenseInstallmentsExpenseId;
        }

        public class ExpenseInstallmentsGetExpenseInstallmentsHandler : IRequestHandler<ExpenseInstallmentsGetExpenseInstallmentsQuery, Response<List<ExpenseInstallmentsGetExpenseInstallmentsResponseDto>>>
        {
            private readonly ILogger<ExpenseInstallmentsGetExpenseInstallmentsHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetExpenseInstallmentsHandler(ILogger<ExpenseInstallmentsGetExpenseInstallmentsHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseInstallmentsGetExpenseInstallmentsResponseDto>>> Handle(ExpenseInstallmentsGetExpenseInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsGetExpenseInstallmentsAsync(request.ExpenseInstallmentsExpenseId);
                return returnValue.ToResponse();
            }
        }
    }
}