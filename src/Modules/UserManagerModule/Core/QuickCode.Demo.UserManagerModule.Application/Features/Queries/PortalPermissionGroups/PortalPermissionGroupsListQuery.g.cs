using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalPermissionGroupsListQuery : IRequest<Response<List<PortalPermissionGroupsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PortalPermissionGroupsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class PortalPermissionGroupsListHandler : IRequestHandler<PortalPermissionGroupsListQuery, Response<List<PortalPermissionGroupsDto>>>
        {
            private readonly ILogger<PortalPermissionGroupsListHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsListHandler(ILogger<PortalPermissionGroupsListHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionGroupsDto>>> Handle(PortalPermissionGroupsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}