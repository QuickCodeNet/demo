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
    public class GetRecentHttpRequestsByModuleQuery : IRequest<Response<List<GetRecentHttpRequestsByModuleResponseDto>>>
    {
        public string? AuditLogRequestModule { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }

        public GetRecentHttpRequestsByModuleQuery(string? auditLogRequestModule, int? pageNumber, int? pageSize)
        {
            this.AuditLogRequestModule = auditLogRequestModule;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        public class GetRecentHttpRequestsByModuleHandler : IRequestHandler<GetRecentHttpRequestsByModuleQuery, Response<List<GetRecentHttpRequestsByModuleResponseDto>>>
        {
            private readonly ILogger<GetRecentHttpRequestsByModuleHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public GetRecentHttpRequestsByModuleHandler(ILogger<GetRecentHttpRequestsByModuleHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetRecentHttpRequestsByModuleResponseDto>>> Handle(GetRecentHttpRequestsByModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetRecentHttpRequestsByModuleAsync(request.AuditLogRequestModule, request.pageNumber, request.pageSize);
                return returnValue.ToResponse();
            }
        }
    }
}