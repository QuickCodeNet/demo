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
    public class ExpenseInstallmentsGetApartmentInstallmentsQuery : IRequest<Response<List<ExpenseInstallmentsGetApartmentInstallmentsResponseDto>>>
    {
        public int ExpenseInstallmentsSiteId { get; set; }
        public int ExpenseInstallmentsApartmentId { get; set; }

        public ExpenseInstallmentsGetApartmentInstallmentsQuery(int expenseInstallmentsSiteId, int expenseInstallmentsApartmentId)
        {
            this.ExpenseInstallmentsSiteId = expenseInstallmentsSiteId;
            this.ExpenseInstallmentsApartmentId = expenseInstallmentsApartmentId;
        }

        public class ExpenseInstallmentsGetApartmentInstallmentsHandler : IRequestHandler<ExpenseInstallmentsGetApartmentInstallmentsQuery, Response<List<ExpenseInstallmentsGetApartmentInstallmentsResponseDto>>>
        {
            private readonly ILogger<ExpenseInstallmentsGetApartmentInstallmentsHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetApartmentInstallmentsHandler(ILogger<ExpenseInstallmentsGetApartmentInstallmentsHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseInstallmentsGetApartmentInstallmentsResponseDto>>> Handle(ExpenseInstallmentsGetApartmentInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsGetApartmentInstallmentsAsync(request.ExpenseInstallmentsSiteId, request.ExpenseInstallmentsApartmentId);
                return returnValue.ToResponse();
            }
        }
    }
}