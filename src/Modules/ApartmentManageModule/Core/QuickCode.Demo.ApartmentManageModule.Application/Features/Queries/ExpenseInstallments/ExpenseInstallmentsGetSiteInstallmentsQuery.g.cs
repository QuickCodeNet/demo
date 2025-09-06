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
    public class ExpenseInstallmentsGetSiteInstallmentsQuery : IRequest<Response<List<ExpenseInstallmentsGetSiteInstallmentsResponseDto>>>
    {
        public int ExpenseInstallmentsSiteId { get; set; }

        public ExpenseInstallmentsGetSiteInstallmentsQuery(int expenseInstallmentsSiteId)
        {
            this.ExpenseInstallmentsSiteId = expenseInstallmentsSiteId;
        }

        public class ExpenseInstallmentsGetSiteInstallmentsHandler : IRequestHandler<ExpenseInstallmentsGetSiteInstallmentsQuery, Response<List<ExpenseInstallmentsGetSiteInstallmentsResponseDto>>>
        {
            private readonly ILogger<ExpenseInstallmentsGetSiteInstallmentsHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetSiteInstallmentsHandler(ILogger<ExpenseInstallmentsGetSiteInstallmentsHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseInstallmentsGetSiteInstallmentsResponseDto>>> Handle(ExpenseInstallmentsGetSiteInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsGetSiteInstallmentsAsync(request.ExpenseInstallmentsSiteId);
                return returnValue.ToResponse();
            }
        }
    }
}