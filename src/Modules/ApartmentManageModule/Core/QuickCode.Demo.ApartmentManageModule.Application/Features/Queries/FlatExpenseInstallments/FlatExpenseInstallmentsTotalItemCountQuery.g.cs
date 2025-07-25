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
    public class FlatExpenseInstallmentsTotalItemCountQuery : IRequest<Response<int>>
    {
        public FlatExpenseInstallmentsTotalItemCountQuery()
        {
        }

        public class FlatExpenseInstallmentsTotalItemCountHandler : IRequestHandler<FlatExpenseInstallmentsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<FlatExpenseInstallmentsTotalItemCountHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsTotalItemCountHandler(ILogger<FlatExpenseInstallmentsTotalItemCountHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(FlatExpenseInstallmentsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}