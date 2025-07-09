using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ColumnTypesTotalItemCountQuery : IRequest<Response<int>>
    {
        public ColumnTypesTotalItemCountQuery()
        {
        }

        public class ColumnTypesTotalItemCountHandler : IRequestHandler<ColumnTypesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<ColumnTypesTotalItemCountHandler> _logger;
            private readonly IColumnTypesRepository _repository;
            public ColumnTypesTotalItemCountHandler(ILogger<ColumnTypesTotalItemCountHandler> logger, IColumnTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ColumnTypesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}