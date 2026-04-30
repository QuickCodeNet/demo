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
    public class StatsYesterdayHttpRequestsQuery : IRequest<Response<StatsYesterdayHttpRequestsResponseDto>>
    {
        public StatsYesterdayHttpRequestsQuery()
        {
        }

        public class StatsYesterdayHttpRequestsHandler : IRequestHandler<StatsYesterdayHttpRequestsQuery, Response<StatsYesterdayHttpRequestsResponseDto>>
        {
            private readonly ILogger<StatsYesterdayHttpRequestsHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsYesterdayHttpRequestsHandler(ILogger<StatsYesterdayHttpRequestsHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<StatsYesterdayHttpRequestsResponseDto>> Handle(StatsYesterdayHttpRequestsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsYesterdayHttpRequestsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}