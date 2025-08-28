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
    public class ExpenseInstallmentsInsertCommand : IRequest<Response<ExpenseInstallmentsDto>>
    {
        public ExpenseInstallmentsDto request { get; set; }

        public ExpenseInstallmentsInsertCommand(ExpenseInstallmentsDto request)
        {
            this.request = request;
        }

        public class ExpenseInstallmentsInsertHandler : IRequestHandler<ExpenseInstallmentsInsertCommand, Response<ExpenseInstallmentsDto>>
        {
            private readonly ILogger<ExpenseInstallmentsInsertHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsInsertHandler(ILogger<ExpenseInstallmentsInsertHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ExpenseInstallmentsDto>> Handle(ExpenseInstallmentsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}