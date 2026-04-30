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
    public class StatsTotalHttpRequestsByModuleQuery : IRequest<Response<List<StatsTotalHttpRequestsByModuleResponseDto>>>
    {
        public StatsTotalHttpRequestsByModuleQuery()
        {
        }

        public class StatsTotalHttpRequestsByModuleHandler : IRequestHandler<StatsTotalHttpRequestsByModuleQuery, Response<List<StatsTotalHttpRequestsByModuleResponseDto>>>
        {
            private readonly ILogger<StatsTotalHttpRequestsByModuleHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsTotalHttpRequestsByModuleHandler(ILogger<StatsTotalHttpRequestsByModuleHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsTotalHttpRequestsByModuleResponseDto>>> Handle(StatsTotalHttpRequestsByModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsTotalHttpRequestsByModuleAsync();
                return returnValue.ToResponse();
            }
        }
    }
}