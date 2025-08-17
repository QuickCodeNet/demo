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
    public class ExpenseInstallmentsGetOverdueInstallmentsQuery : IRequest<Response<List<ExpenseInstallmentsGetOverdueInstallmentsResponseDto>>>
    {
        public bool ExpenseInstallmentsPaid { get; set; }

        public ExpenseInstallmentsGetOverdueInstallmentsQuery(bool expenseInstallmentsPaid)
        {
            this.ExpenseInstallmentsPaid = expenseInstallmentsPaid;
        }

        public class ExpenseInstallmentsGetOverdueInstallmentsHandler : IRequestHandler<ExpenseInstallmentsGetOverdueInstallmentsQuery, Response<List<ExpenseInstallmentsGetOverdueInstallmentsResponseDto>>>
        {
            private readonly ILogger<ExpenseInstallmentsGetOverdueInstallmentsHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetOverdueInstallmentsHandler(ILogger<ExpenseInstallmentsGetOverdueInstallmentsHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseInstallmentsGetOverdueInstallmentsResponseDto>>> Handle(ExpenseInstallmentsGetOverdueInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsGetOverdueInstallmentsAsync(request.ExpenseInstallmentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}