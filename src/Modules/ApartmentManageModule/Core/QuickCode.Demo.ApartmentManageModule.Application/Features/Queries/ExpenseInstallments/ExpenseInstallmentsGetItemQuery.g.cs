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
    public class ExpenseInstallmentsGetItemQuery : IRequest<Response<ExpenseInstallmentsDto>>
    {
        public int Id { get; set; }

        public ExpenseInstallmentsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class ExpenseInstallmentsGetItemHandler : IRequestHandler<ExpenseInstallmentsGetItemQuery, Response<ExpenseInstallmentsDto>>
        {
            private readonly ILogger<ExpenseInstallmentsGetItemHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsGetItemHandler(ILogger<ExpenseInstallmentsGetItemHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ExpenseInstallmentsDto>> Handle(ExpenseInstallmentsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}