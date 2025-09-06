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
    public class CommonExpensesTotalItemCountQuery : IRequest<Response<int>>
    {
        public CommonExpensesTotalItemCountQuery()
        {
        }

        public class CommonExpensesTotalItemCountHandler : IRequestHandler<CommonExpensesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<CommonExpensesTotalItemCountHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesTotalItemCountHandler(ILogger<CommonExpensesTotalItemCountHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(CommonExpensesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}