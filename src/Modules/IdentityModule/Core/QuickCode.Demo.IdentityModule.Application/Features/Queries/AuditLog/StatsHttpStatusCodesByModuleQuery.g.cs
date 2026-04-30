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
    public class StatsHttpStatusCodesByModuleQuery : IRequest<Response<List<StatsHttpStatusCodesByModuleResponseDto>>>
    {
        public StatsHttpStatusCodesByModuleQuery()
        {
        }

        public class StatsHttpStatusCodesByModuleHandler : IRequestHandler<StatsHttpStatusCodesByModuleQuery, Response<List<StatsHttpStatusCodesByModuleResponseDto>>>
        {
            private readonly ILogger<StatsHttpStatusCodesByModuleHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsHttpStatusCodesByModuleHandler(ILogger<StatsHttpStatusCodesByModuleHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsHttpStatusCodesByModuleResponseDto>>> Handle(StatsHttpStatusCodesByModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsHttpStatusCodesByModuleAsync();
                return returnValue.ToResponse();
            }
        }
    }
}