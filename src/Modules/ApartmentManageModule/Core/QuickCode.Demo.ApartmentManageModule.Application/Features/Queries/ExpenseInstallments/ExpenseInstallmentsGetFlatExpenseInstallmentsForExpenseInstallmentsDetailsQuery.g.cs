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
    public class ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsQuery : IRequest<Response<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>>
    {
        public int ExpenseInstallmentsId { get; set; }
        public int FlatExpenseInstallmentsId { get; set; }

        public ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsQuery(int expenseInstallmentsId, int flatExpenseInstallmentsId)
        {
            this.ExpenseInstallmentsId = expenseInstallmentsId;
            this.FlatExpenseInstallmentsId = flatExpenseInstallmentsId;
        }

        public class ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsHandler : IRequestHandler<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsQuery, Response<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>>
        {
            private readonly ILogger<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsHandler(ILogger<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsResponseDto>> Handle(ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsGetFlatExpenseInstallmentsForExpenseInstallmentsDetailsAsync(request.ExpenseInstallmentsId, request.FlatExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}