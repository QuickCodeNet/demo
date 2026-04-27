using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.ColumnType;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.ColumnType
{
    public class TotalCountColumnTypeQuery : IRequest<Response<int>>
    {
        public TotalCountColumnTypeQuery()
        {
        }

        public class TotalCountColumnTypeHandler : IRequestHandler<TotalCountColumnTypeQuery, Response<int>>
        {
            private readonly ILogger<TotalCountColumnTypeHandler> _logger;
            private readonly IColumnTypeRepository _repository;
            public TotalCountColumnTypeHandler(ILogger<TotalCountColumnTypeHandler> logger, IColumnTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountColumnTypeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}