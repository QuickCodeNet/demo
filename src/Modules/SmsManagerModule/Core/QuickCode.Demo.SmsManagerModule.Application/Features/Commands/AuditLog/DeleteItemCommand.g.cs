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
    public class DeleteItemAuditLogCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public DeleteItemAuditLogCommand(Guid id)
        {
            this.Id = id;
        }

        public class DeleteItemAuditLogHandler : IRequestHandler<DeleteItemAuditLogCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemAuditLogHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public DeleteItemAuditLogHandler(ILogger<DeleteItemAuditLogHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemAuditLogCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}