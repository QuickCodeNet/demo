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
    public class PortalPermissionGroupsTotalItemCountQuery : IRequest<Response<int>>
    {
        public PortalPermissionGroupsTotalItemCountQuery()
        {
        }

        public class PortalPermissionGroupsTotalItemCountHandler : IRequestHandler<PortalPermissionGroupsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<PortalPermissionGroupsTotalItemCountHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsTotalItemCountHandler(ILogger<PortalPermissionGroupsTotalItemCountHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(PortalPermissionGroupsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}