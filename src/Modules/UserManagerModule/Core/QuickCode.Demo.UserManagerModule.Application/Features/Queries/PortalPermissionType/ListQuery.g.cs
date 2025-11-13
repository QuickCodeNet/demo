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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionType
{
    public class ListPortalPermissionTypeQuery : IRequest<Response<List<PortalPermissionTypeDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListPortalPermissionTypeQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListPortalPermissionTypeHandler : IRequestHandler<ListPortalPermissionTypeQuery, Response<List<PortalPermissionTypeDto>>>
        {
            private readonly ILogger<ListPortalPermissionTypeHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public ListPortalPermissionTypeHandler(ILogger<ListPortalPermissionTypeHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionTypeDto>>> Handle(ListPortalPermissionTypeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}