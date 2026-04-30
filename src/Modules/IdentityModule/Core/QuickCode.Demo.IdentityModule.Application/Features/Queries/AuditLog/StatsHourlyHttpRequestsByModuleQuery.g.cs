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
    public class StatsHourlyHttpRequestsByModuleQuery : IRequest<Response<List<StatsHourlyHttpRequestsByModuleResponseDto>>>
    {
        public StatsHourlyHttpRequestsByModuleQuery()
        {
        }

        public class StatsHourlyHttpRequestsByModuleHandler : IRequestHandler<StatsHourlyHttpRequestsByModuleQuery, Response<List<StatsHourlyHttpRequestsByModuleResponseDto>>>
        {
            private readonly ILogger<StatsHourlyHttpRequestsByModuleHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsHourlyHttpRequestsByModuleHandler(ILogger<StatsHourlyHttpRequestsByModuleHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsHourlyHttpRequestsByModuleResponseDto>>> Handle(StatsHourlyHttpRequestsByModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsHourlyHttpRequestsByModuleAsync();
                return returnValue.ToResponse();
            }
        }
    }
}