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
    public class ExpenseInstallmentsDeleteCommand : IRequest<Response<bool>>
    {
        public ExpenseInstallmentsDto request { get; set; }

        public ExpenseInstallmentsDeleteCommand(ExpenseInstallmentsDto request)
        {
            this.request = request;
        }

        public class ExpenseInstallmentsDeleteHandler : IRequestHandler<ExpenseInstallmentsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ExpenseInstallmentsDeleteHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsDeleteHandler(ILogger<ExpenseInstallmentsDeleteHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExpenseInstallmentsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}