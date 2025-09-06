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
    public class ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsQuery : IRequest<Response<List<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>>>
    {
        public int ExpenseInstallmentsId { get; set; }

        public ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsQuery(int expenseInstallmentsId)
        {
            this.ExpenseInstallmentsId = expenseInstallmentsId;
        }

        public class ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsHandler : IRequestHandler<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsQuery, Response<List<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>>>
        {
            private readonly ILogger<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsHandler(ILogger<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>>> Handle(ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsAsync(request.ExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}