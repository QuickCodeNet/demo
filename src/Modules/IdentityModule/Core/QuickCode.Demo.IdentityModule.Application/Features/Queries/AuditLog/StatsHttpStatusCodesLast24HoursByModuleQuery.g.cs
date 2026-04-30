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
    public class StatsHttpStatusCodesLast24HoursByModuleQuery : IRequest<Response<List<StatsHttpStatusCodesLast24HoursByModuleResponseDto>>>
    {
        public StatsHttpStatusCodesLast24HoursByModuleQuery()
        {
        }

        public class StatsHttpStatusCodesLast24HoursByModuleHandler : IRequestHandler<StatsHttpStatusCodesLast24HoursByModuleQuery, Response<List<StatsHttpStatusCodesLast24HoursByModuleResponseDto>>>
        {
            private readonly ILogger<StatsHttpStatusCodesLast24HoursByModuleHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsHttpStatusCodesLast24HoursByModuleHandler(ILogger<StatsHttpStatusCodesLast24HoursByModuleHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsHttpStatusCodesLast24HoursByModuleResponseDto>>> Handle(StatsHttpStatusCodesLast24HoursByModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsHttpStatusCodesLast24HoursByModuleAsync();
                return returnValue.ToResponse();
            }
        }
    }
}