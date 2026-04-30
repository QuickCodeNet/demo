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
    public class StatsDailyHttpRequestsByDayByModuleQuery : IRequest<Response<List<StatsDailyHttpRequestsByDayByModuleResponseDto>>>
    {
        public StatsDailyHttpRequestsByDayByModuleQuery()
        {
        }

        public class StatsDailyHttpRequestsByDayByModuleHandler : IRequestHandler<StatsDailyHttpRequestsByDayByModuleQuery, Response<List<StatsDailyHttpRequestsByDayByModuleResponseDto>>>
        {
            private readonly ILogger<StatsDailyHttpRequestsByDayByModuleHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsDailyHttpRequestsByDayByModuleHandler(ILogger<StatsDailyHttpRequestsByDayByModuleHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsDailyHttpRequestsByDayByModuleResponseDto>>> Handle(StatsDailyHttpRequestsByDayByModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsDailyHttpRequestsByDayByModuleAsync();
                return returnValue.ToResponse();
            }
        }
    }
}