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
    public class StatsWeeklyHttpRequestsQuery : IRequest<Response<StatsWeeklyHttpRequestsResponseDto>>
    {
        public StatsWeeklyHttpRequestsQuery()
        {
        }

        public class StatsWeeklyHttpRequestsHandler : IRequestHandler<StatsWeeklyHttpRequestsQuery, Response<StatsWeeklyHttpRequestsResponseDto>>
        {
            private readonly ILogger<StatsWeeklyHttpRequestsHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsWeeklyHttpRequestsHandler(ILogger<StatsWeeklyHttpRequestsHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<StatsWeeklyHttpRequestsResponseDto>> Handle(StatsWeeklyHttpRequestsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsWeeklyHttpRequestsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}