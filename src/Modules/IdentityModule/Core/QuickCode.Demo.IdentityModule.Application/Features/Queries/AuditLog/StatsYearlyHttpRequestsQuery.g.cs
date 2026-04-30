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
using QuickCode.Demo.IdentityModule.Application.Dtos.AuditLog;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.AuditLog
{
    public class StatsYearlyHttpRequestsQuery : IRequest<Response<StatsYearlyHttpRequestsResponseDto>>
    {
        public StatsYearlyHttpRequestsQuery()
        {
        }

        public class StatsYearlyHttpRequestsHandler : IRequestHandler<StatsYearlyHttpRequestsQuery, Response<StatsYearlyHttpRequestsResponseDto>>
        {
            private readonly ILogger<StatsYearlyHttpRequestsHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsYearlyHttpRequestsHandler(ILogger<StatsYearlyHttpRequestsHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<StatsYearlyHttpRequestsResponseDto>> Handle(StatsYearlyHttpRequestsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsYearlyHttpRequestsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}