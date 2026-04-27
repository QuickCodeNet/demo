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
    public class TotalCountAuditLogQuery : IRequest<Response<int>>
    {
        public TotalCountAuditLogQuery()
        {
        }

        public class TotalCountAuditLogHandler : IRequestHandler<TotalCountAuditLogQuery, Response<int>>
        {
            private readonly ILogger<TotalCountAuditLogHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public TotalCountAuditLogHandler(ILogger<TotalCountAuditLogHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountAuditLogQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}