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
    public class CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsQuery : IRequest<Response<CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto>>
    {
        public int CommonExpensesId { get; set; }
        public int ExpenseInstallmentsId { get; set; }

        public CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsQuery(int commonExpensesId, int expenseInstallmentsId)
        {
            this.CommonExpensesId = commonExpensesId;
            this.ExpenseInstallmentsId = expenseInstallmentsId;
        }

        public class CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsHandler : IRequestHandler<CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsQuery, Response<CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto>>
        {
            private readonly ILogger<CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsHandler(ILogger<CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto>> Handle(CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetExpenseInstallmentsForCommonExpensesDetailsAsync(request.CommonExpensesId, request.ExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}