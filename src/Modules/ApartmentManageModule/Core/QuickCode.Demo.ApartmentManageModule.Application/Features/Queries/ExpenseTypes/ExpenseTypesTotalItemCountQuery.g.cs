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
    public class ExpenseTypesTotalItemCountQuery : IRequest<Response<int>>
    {
        public ExpenseTypesTotalItemCountQuery()
        {
        }

        public class ExpenseTypesTotalItemCountHandler : IRequestHandler<ExpenseTypesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<ExpenseTypesTotalItemCountHandler> _logger;
            private readonly IExpenseTypesRepository _repository;
            public ExpenseTypesTotalItemCountHandler(ILogger<ExpenseTypesTotalItemCountHandler> logger, IExpenseTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ExpenseTypesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}