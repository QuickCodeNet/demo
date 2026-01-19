using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.AuditLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.AuditLog
{
    public class DeleteAuditLogCommand : IRequest<Response<bool>>
    {
        public AuditLogDto request { get; set; }

        public DeleteAuditLogCommand(AuditLogDto request)
        {
            this.request = request;
        }

        public class DeleteAuditLogHandler : IRequestHandler<DeleteAuditLogCommand, Response<bool>>
        {
            private readonly ILogger<DeleteAuditLogHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public DeleteAuditLogHandler(ILogger<DeleteAuditLogHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteAuditLogCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}