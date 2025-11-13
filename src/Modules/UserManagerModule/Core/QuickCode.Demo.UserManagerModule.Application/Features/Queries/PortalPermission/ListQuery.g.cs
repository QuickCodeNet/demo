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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermission;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermission
{
    public class ListPortalPermissionQuery : IRequest<Response<List<PortalPermissionDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListPortalPermissionQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListPortalPermissionHandler : IRequestHandler<ListPortalPermissionQuery, Response<List<PortalPermissionDto>>>
        {
            private readonly ILogger<ListPortalPermissionHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public ListPortalPermissionHandler(ILogger<ListPortalPermissionHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionDto>>> Handle(ListPortalPermissionQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}