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
    public class ExpenseInstallmentsGetUnpaidInstallmentsQuery : IRequest<Response<List<ExpenseInstallmentsGetUnpaidInstallmentsResponseDto>>>
    {
        public int ExpenseInstallmentsExpenseId { get; set; }
        public bool ExpenseInstallmentsPaid { get; set; }

        public ExpenseInstallmentsGetUnpaidInstallmentsQuery(int expenseInstallmentsExpenseId, bool expenseInstallmentsPaid)
        {
            this.ExpenseInstallmentsExpenseId = expenseInstallmentsExpenseId;
            this.ExpenseInstallmentsPaid = expenseInstallmentsPaid;
        }

        public class ExpenseInstallmentsGetUnpaidInstallmentsHandler : IRequestHandler<ExpenseInstallmentsGetUnpaidInstallmentsQuery, Response<List<ExpenseInstallmentsGetUnpaidInstallmentsResponseDto>>>
        {
            private readonly ILogger<ExpenseInstallmentsGetUnpaidInstallmentsHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetUnpaidInstallmentsHandler(ILogger<ExpenseInstallmentsGetUnpaidInstallmentsHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseInstallmentsGetUnpaidInstallmentsResponseDto>>> Handle(ExpenseInstallmentsGetUnpaidInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsGetUnpaidInstallmentsAsync(request.ExpenseInstallmentsExpenseId, request.ExpenseInstallmentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}