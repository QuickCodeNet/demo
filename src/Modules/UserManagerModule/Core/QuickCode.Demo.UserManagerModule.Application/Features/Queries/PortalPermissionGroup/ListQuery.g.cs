using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup
{
    public class ListPortalPermissionGroupQuery : IRequest<Response<List<PortalPermissionGroupDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListPortalPermissionGroupQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListPortalPermissionGroupHandler : IRequestHandler<ListPortalPermissionGroupQuery, Response<List<PortalPermissionGroupDto>>>
        {
            private readonly ILogger<ListPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public ListPortalPermissionGroupHandler(ILogger<ListPortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionGroupDto>>> Handle(ListPortalPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}