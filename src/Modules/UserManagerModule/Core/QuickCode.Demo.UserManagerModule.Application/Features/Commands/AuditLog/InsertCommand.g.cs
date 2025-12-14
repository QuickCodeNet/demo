using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AuditLog;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AuditLog
{
    public class InsertAuditLogCommand : IRequest<Response<AuditLogDto>>
    {
        public AuditLogDto request { get; set; }

        public InsertAuditLogCommand(AuditLogDto request)
        {
            this.request = request;
        }

        public class InsertAuditLogHandler : IRequestHandler<InsertAuditLogCommand, Response<AuditLogDto>>
        {
            private readonly ILogger<InsertAuditLogHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public InsertAuditLogHandler(ILogger<InsertAuditLogHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AuditLogDto>> Handle(InsertAuditLogCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}