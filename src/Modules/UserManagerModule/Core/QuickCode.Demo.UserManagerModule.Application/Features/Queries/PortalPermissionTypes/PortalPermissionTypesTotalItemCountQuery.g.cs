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
    public class PortalPermissionTypesTotalItemCountQuery : IRequest<Response<int>>
    {
        public PortalPermissionTypesTotalItemCountQuery()
        {
        }

        public class PortalPermissionTypesTotalItemCountHandler : IRequestHandler<PortalPermissionTypesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<PortalPermissionTypesTotalItemCountHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesTotalItemCountHandler(ILogger<PortalPermissionTypesTotalItemCountHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(PortalPermissionTypesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}