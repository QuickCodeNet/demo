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
    public class StatsHttpStatusCodesQuery : IRequest<Response<List<StatsHttpStatusCodesResponseDto>>>
    {
        public StatsHttpStatusCodesQuery()
        {
        }

        public class StatsHttpStatusCodesHandler : IRequestHandler<StatsHttpStatusCodesQuery, Response<List<StatsHttpStatusCodesResponseDto>>>
        {
            private readonly ILogger<StatsHttpStatusCodesHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public StatsHttpStatusCodesHandler(ILogger<StatsHttpStatusCodesHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<StatsHttpStatusCodesResponseDto>>> Handle(StatsHttpStatusCodesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.StatsHttpStatusCodesAsync();
                return returnValue.ToResponse();
            }
        }
    }
}