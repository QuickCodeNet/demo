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
    public class ExpenseInstallmentsMarkInstallmentAsPaidCommand : IRequest<Response<int>>
    {
        public int ExpenseInstallmentsId { get; set; }
        public ExpenseInstallmentsMarkInstallmentAsPaidRequestDto UpdateRequest { get; set; }

        public ExpenseInstallmentsMarkInstallmentAsPaidCommand(int expenseInstallmentsId, ExpenseInstallmentsMarkInstallmentAsPaidRequestDto updateRequest)
        {
            this.ExpenseInstallmentsId = expenseInstallmentsId;
            this.UpdateRequest = updateRequest;
        }

        public class ExpenseInstallmentsMarkInstallmentAsPaidHandler : IRequestHandler<ExpenseInstallmentsMarkInstallmentAsPaidCommand, Response<int>>
        {
            private readonly ILogger<ExpenseInstallmentsMarkInstallmentAsPaidHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsMarkInstallmentAsPaidHandler(ILogger<ExpenseInstallmentsMarkInstallmentAsPaidHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ExpenseInstallmentsMarkInstallmentAsPaidCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseInstallmentsMarkInstallmentAsPaidAsync(request.ExpenseInstallmentsId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}