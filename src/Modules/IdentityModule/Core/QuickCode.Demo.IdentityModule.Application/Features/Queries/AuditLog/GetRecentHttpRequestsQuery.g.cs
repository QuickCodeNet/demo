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
    public class GetRecentHttpRequestsQuery : IRequest<Response<List<GetRecentHttpRequestsResponseDto>>>
    {
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }

        public GetRecentHttpRequestsQuery(int? pageNumber, int? pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        public class GetRecentHttpRequestsHandler : IRequestHandler<GetRecentHttpRequestsQuery, Response<List<GetRecentHttpRequestsResponseDto>>>
        {
            private readonly ILogger<GetRecentHttpRequestsHandler> _logger;
            private readonly IAuditLogRepository _repository;
            public GetRecentHttpRequestsHandler(ILogger<GetRecentHttpRequestsHandler> logger, IAuditLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetRecentHttpRequestsResponseDto>>> Handle(GetRecentHttpRequestsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetRecentHttpRequestsAsync(request.pageNumber, request.pageSize);
                return returnValue.ToResponse();
            }
        }
    }
}