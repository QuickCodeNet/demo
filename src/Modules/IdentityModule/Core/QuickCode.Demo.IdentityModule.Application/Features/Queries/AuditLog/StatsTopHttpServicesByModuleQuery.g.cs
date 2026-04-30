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
    public class StatsTopHttpServicesByModuleQuery : IRequest<Response<List<StatsTopHttpServicesByModuleResponseDto>>>
    {
        public StatsTopHttpServicesByModuleQuery()
        {
        }

        public class StatsTopHttpServicesByModuleHandler : IRequestHandler<StatsTopHttpServicesByModuleQuery, Response<List<StatsTopHttpServicesByModuleResponseDto>>>
        {
            private readonly ILogger<StatsTopHttpServicesByModuleHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsTopHttpServicesByModuleHandler(ILogger<StatsTopHttpServicesByModuleHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsTopHttpServicesByModuleResponseDto>>> Handle(StatsTopHttpServicesByModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsTopHttpServicesByModuleAsync();
                return returnValue.ToResponse();
            }
        }
    }
}