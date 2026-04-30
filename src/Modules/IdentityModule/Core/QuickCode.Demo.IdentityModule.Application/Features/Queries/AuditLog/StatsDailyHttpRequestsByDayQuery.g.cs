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
    public class StatsDailyHttpRequestsByDayQuery : IRequest<Response<List<StatsDailyHttpRequestsByDayResponseDto>>>
    {
        public StatsDailyHttpRequestsByDayQuery()
        {
        }

        public class StatsDailyHttpRequestsByDayHandler : IRequestHandler<StatsDailyHttpRequestsByDayQuery, Response<List<StatsDailyHttpRequestsByDayResponseDto>>>
        {
            private readonly ILogger<StatsDailyHttpRequestsByDayHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsDailyHttpRequestsByDayHandler(ILogger<StatsDailyHttpRequestsByDayHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsDailyHttpRequestsByDayResponseDto>>> Handle(StatsDailyHttpRequestsByDayQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsDailyHttpRequestsByDayAsync();
                return returnValue.ToResponse();
            }
        }
    }
}