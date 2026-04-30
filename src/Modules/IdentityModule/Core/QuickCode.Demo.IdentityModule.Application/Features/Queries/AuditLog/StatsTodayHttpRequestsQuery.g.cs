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
    public class StatsTodayHttpRequestsQuery : IRequest<Response<StatsTodayHttpRequestsResponseDto>>
    {
        public StatsTodayHttpRequestsQuery()
        {
        }

        public class StatsTodayHttpRequestsHandler : IRequestHandler<StatsTodayHttpRequestsQuery, Response<StatsTodayHttpRequestsResponseDto>>
        {
            private readonly ILogger<StatsTodayHttpRequestsHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsTodayHttpRequestsHandler(ILogger<StatsTodayHttpRequestsHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<StatsTodayHttpRequestsResponseDto>> Handle(StatsTodayHttpRequestsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsTodayHttpRequestsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}